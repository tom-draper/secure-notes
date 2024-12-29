
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecureNotes.Helpers.Cryptography;

public static class Generate
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
}
