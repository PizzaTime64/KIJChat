using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace ECDH
{
    class Program
    {
        public static byte[] publicKey;


        public static void GenerateKey()
        {
            ECDiffieHellmanCng keygen = new ECDiffieHellmanCng();
            keygen.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            keygen.HashAlgorithm = CngAlgorithm.Sha256;
            publicKey = keygen.PublicKey.ToByteArray();

            /*
            Console.WriteLine("panjang key : " + publicKey.Length);
            Console.WriteLine("Public Key : ");
            for (int i=0; i<publicKey.Length;i++)
            {
                Console.Write(publicKey[i]);
            }
            */
        }

        public static byte[] CalculateKey(byte[] publicKey)
        {
            ECDiffieHellmanCng Calculate = new ECDiffieHellmanCng();
            Calculate.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            Calculate.HashAlgorithm = CngAlgorithm.Sha256;
            CngKey k = CngKey.Import(publicKey, CngKeyBlobFormat.EccPublicBlob);
            byte[] key = Calculate.DeriveKeyMaterial(CngKey.Import(publicKey, CngKeyBlobFormat.EccPublicBlob));
            return key;
        }




        static void Main(string[] args)
        {
            GenerateKey();
            byte[] kunci = CalculateKey(publicKey);
            for (int i = 0; i < kunci.Length; i++)
            {
                Console.Write(kunci[i]);
            }
            Console.ReadLine();
        }
    }
}
