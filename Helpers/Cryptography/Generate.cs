
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecureNotes.Helpers.Cryptography;

public static class Generate
{
    public static (byte[] Key, byte[] IV) GenerateKeyAndIv()
    {
        // Create an instance of AES
        using Aes aesAlg = Aes.Create();
        // Generate a random key and IV
        aesAlg.GenerateKey();  // Generates a random 256-bit key
        aesAlg.GenerateIV();   // Generates a random 128-bit IV

        return (aesAlg.Key, aesAlg.IV);  // Return the key and IV as byte arrays
    }
}
