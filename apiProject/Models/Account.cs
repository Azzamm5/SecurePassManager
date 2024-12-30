    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace apiProject.Models
    {
        public class Account
        {
            public int Id { get; set; } 
            public string Service { get; set; } = string.Empty;
            public string Username { get; set; }  = string.Empty;
            public string Password { get; set; } = string.Empty;

            public DateTime DateAdded {get; set;} = DateTime.UtcNow;  // Valeur par défaut
        }
    }