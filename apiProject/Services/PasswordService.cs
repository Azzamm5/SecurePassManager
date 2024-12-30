using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiProject.Data;
using apiProject.Models;
using apiProject.DTOs;
using apiProject.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SafeSecureManager_API.Services
{
    public class PasswordService
    {
        private readonly AppDbContext _context;

        // ====== Constructeur ======
        public PasswordService(AppDbContext context)
        {
            _context = context; 
        }

        // ====== Création de Compte ======
        public async Task<Account> CreateAccount(Account account)
        {
            _context.Add(account);
            await _context.SaveChangesAsync();  // Sauvegarde asynchrone
            return account;
        }

        // ====== Récupérer Tous les Comptes ======
        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }

        // ====== Récupérer les Comptes par Service ======
        public async Task<IEnumerable<Account>> GetAccountByName(string service)
        {
            // Récupère tous les comptes ayant le même service (Netflix, etc.)
            return await _context.Accounts
                                .Where(a => a.Service == service)
                                .ToListAsync();
        }

        // ====== Récupérer un Compte par ID ======
        public async Task<Account?> GetAccount(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        // ====== Mise à jour d'un Compte ======
        // public async Task<Account?> UpdateAccount(int id, UpdateAccountDTO updateAccount)
        // {
        //     // Recherche du compte
        //     var accountModel = await _context.Accounts.FirstOrDefaultAsync(i => i.Id == id);

        //     if (accountModel == null)
        //     {
        //         return null;  // Si aucun compte n'est trouvé, on retourne null
        //     }

        //     // Mapper les nouvelles valeurs
        //     updateAccount.MapToAccount(accountModel);

        //     // Sauvegarde asynchrone
        //     await _context.SaveChangesAsync();

        //     return accountModel;
        // }

        // ====== Suppression de Compte ======
        public async Task<bool> DeleteAccount(int id)
        {
            // Recherche du compte
            var accountDel = await _context.Accounts.FirstOrDefaultAsync(i => i.Id == id);
            
            if (accountDel == null)
            {
                return false;  // Retourne false si le compte n'existe pas
            }

            // Suppression et sauvegarde
            _context.Accounts.Remove(accountDel);
            await _context.SaveChangesAsync();

            return true;  // Retourne true si suppression réussie
        }
    }
}