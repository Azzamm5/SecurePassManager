using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SafeSecureManager_API.Services
{
    public class AES_Utils
    {
        // Génère une clé AES de 256 bits (32 bytes) et un IV de 128 bits (16 bytes)
        public static (byte[] Key, byte[] IV) GenerateKeyAndIV()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] key = new byte[32]; // 256 bits pour AES
                byte[] iv = new byte[16];  // 128 bits pour AES

                rng.GetBytes(key); // Remplir la clé avec des valeurs aléatoires
                rng.GetBytes(iv);  // Remplir le IV avec des valeurs aléatoires

                return (key, iv);
            }
        }
    }
}