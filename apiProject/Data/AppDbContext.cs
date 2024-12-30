using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace apiProject.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        

        public DbSet<Account> Accounts {get; set;}
        
    }
}