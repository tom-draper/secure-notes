using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecureNotes.Helpers.Cryptography;

public static class Cryptography
{
    // Static fields to hold the generated key and IV
    private static readonly byte[] key;
    private static readonly byte[] iv;

    static Cryptography()
    {
        // Generate key and IV at runtime in the static constructor
        (key, iv) = Generate.GenerateKeyAndIv();
    }

    public static string EncryptString(string plainText)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
        byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
        cs.Write(inputBytes, 0, inputBytes.Length);
        cs.FlushFinalBlock();

        return Convert.ToBase64String(ms.ToArray());  // Return encrypted data as Base64 string
    }

    public static string DecryptString(string encryptedText)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
        cs.Write(encryptedBytes, 0, encryptedBytes.Length);
        cs.FlushFinalBlock();

        return Encoding.UTF8.GetString(ms.ToArray());  // Return decrypted data as string
    }

    public static string GenerateHash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}
