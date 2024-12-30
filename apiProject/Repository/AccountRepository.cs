using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiProject.Interfaces;
using apiProject.Data;
using apiProject.Models;
using Microsoft.EntityFrameworkCore;
using apiProject.DTOs;
using apiProject.Exceptions;
using apiProject.Helpers;



namespace apiProject.Repository
{
    public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _SecurePass;

    public AccountRepository(AppDbContext secure)
    {
        _SecurePass = secure;
    }

        

        // Retourne tous les comptes enregistrés
        public async Task<List<Account>> GetAll()
        {
            var allAccounts = await _SecurePass.Accounts.ToListAsync();
            if (!allAccounts.Any())
            {
                throw new AccountNotFoundException("!!! Aucun Disponible !!!");
            }

            return allAccounts;
        }

    // Retourne un compte par son ID
    public async Task<Account?> GetById(int id)
    {
        var account = await _SecurePass.Accounts.FindAsync(id); 
        if(account == null){
            throw new AccountNotFoundException($"Le Compte Avec l'id {id} Est introuvable !!!!");
        }
        return account;  
    }

    // Ajoute un nouveau compte
    public async Task<Account> Add(AccountDto accountDto)
    {
        var account = new Account {
            Service = accountDto.Service,
            Username = accountDto.Username,
            Password = accountDto.Password,            
        };
        await _SecurePass.Accounts.AddAsync(account);
        await _SecurePass.SaveChangesAsync();
        return account;
    }

    // Supprime un compte par son ID
    public async Task<Account?> Delete(int id)
    {
        var accountToRemove = await _SecurePass.Accounts.FindAsync(id); // Utilisation de FindAsync pour plus de simplicité

        if (accountToRemove == null)
        {
            throw new AccountNotFoundException($"Le Compte Avec l'id {id} Est introuvable !!!!");
        }

        _SecurePass.Accounts.Remove(accountToRemove);
        await _SecurePass.SaveChangesAsync();
        return accountToRemove;
    }

    // Met à jour un compte existant
    public async Task<Account?> Update(int id, AccountUpdateDto accountDto)
    {
        var accountExists = await _SecurePass.Accounts.FindAsync(id);

        if (accountExists == null)
        {
            throw new AccountNotFoundException($"Supression Impossible \nLe Compte Avec l'id {id} Est introuvable !!!!");
        }

        // Mise à jour des propriétés
        accountExists.Service = accountDto.Service;
        accountExists.Username = accountDto.Username;
        accountExists.Password = accountDto.Password;
        accountExists.DateAdded = accountDto.DateAdded;

        await _SecurePass.SaveChangesAsync();
        return accountExists;
    }

    // Vérifie si le compte existe
    public async Task<bool> AccountExists(int id)
    {
        return await _SecurePass.Accounts.AnyAsync(account => account.Id == id);
    }

        public async Task<List<Account>> GetAccountByServiceAndName(QueryObject query)
        {
            var accountsQuery = _SecurePass.Accounts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Username))
            {
                accountsQuery = accountsQuery.Where(a => a.Username.Contains(query.Username));
            }

            if (!string.IsNullOrWhiteSpace(query.Service))
            {
                accountsQuery = accountsQuery.Where(a => a.Service.Contains(query.Service));
            }

            // Exécution de la requête et vérification des résultats
            var accountsList = await accountsQuery.ToListAsync();


            if (accountsList.Count == 0)
            {
                throw new AccountNotFoundException("Aucun compte trouvé avec les critères spécifiés.");
            }

            var paginatedAccounts = accountsQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize);
                
            return  await paginatedAccounts.ToListAsync();
        }
    }
}