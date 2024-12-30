using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiProject.DTOs;
using apiProject.Models;

namespace apiProject.Mappers
{
    public static class AccountMapper
    {
        public static AccountDto ToAccountDto(this Account AccountModel)
        {
            return new AccountDto{
                Service = AccountModel.Service,
                Username = AccountModel.Username,
                Password = AccountModel.Password,
            };
        }
    }
}