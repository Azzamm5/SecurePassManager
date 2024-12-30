using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SafeSecureManager_API.Services
{
    public class AES_EncryptionService
    {
        // Méthode de chiffrement 
        public string Encrypt(string plainText, out byte[] key, out byte[] iv)
        {
            // Utiliser la méthode générant une clé et un IV sécurisés
            (key, iv) = AES_Utils.GenerateKeyAndIV();

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                // Déclarer ms AVANT le bloc using
                MemoryStream ms = new MemoryStream();

                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        // Méthode de déchiffrement 
        public string Decrypt(string encryptedText, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedText)))
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
