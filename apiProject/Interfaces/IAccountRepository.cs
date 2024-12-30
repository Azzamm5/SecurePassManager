using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiProject.Data;
using apiProject.Models;
using apiProject.DTOs;
using apiProject.Helpers;


namespace apiProject.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAll();
        Task<Account?> GetById(int id);
        Task<List<Account>> GetAccountByServiceAndName(QueryObject query);
        Task<Account> Add(AccountDto accountDto);
        Task<Account?> Update(int id,AccountUpdateDto Ac);
        Task<Account?> Delete(int id);

        Task<bool> AccountExists(int id);
        
    }
}