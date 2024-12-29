using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecureNotes.Helpers.Cryptography;

public static class Cryptography
{
    public static void GenerateKeyAndIVFromRandomNumber(int randomSeed, out byte[] key, out byte[] iv, int keySize = 16, int blockSize = 16)
    {
        key = new byte[keySize];
        iv = new byte[blockSize];

        using (var rng = new RNGCryptoServiceProvider())
        {
            // Use the random seed to generate a deterministic random number sequence
            byte[] seedBytes = BitConverter.GetBytes(randomSeed);
            RNGCryptoServiceProvider rngProvider = new RNGCryptoServiceProvider(seedBytes);

            rngProvider.GetBytes(key); // Generate random key
            rngProvider.GetBytes(iv);  // Generate random IV
        }
    }

    public static string EncryptString(string plainText, byte[] key, byte[] iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
                cs.Write(inputBytes, 0, inputBytes.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public static string DecryptString(string encryptedText, byte[] key, byte[] iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                cs.FlushFinalBlock();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
