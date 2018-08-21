using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AESEncriptation
{
    class Program
    {
        static void Main(string[] args)
        {
            Protection protection = new Protection();
            try
            {
                using (Aes aes = Aes.Create())
                {
                    Console.WriteLine("Enter text to encrypt: ");
                    string text =  Console.ReadLine();

                    byte[] encrypted = protection.EncryptDataAes(text, aes.Key, aes.IV);
                    string eText = String.Empty;

                    foreach (var b in encrypted)
                    {
                        eText += b.ToString() + ", ";
                    }
                    Console.WriteLine(Environment.NewLine +$"Encrypted text: {eText}");

                    string decrypted = protection.DecryptDataAes(encrypted, aes.Key, aes.IV);

                    Console.WriteLine(Environment.NewLine + $"Decrypted text: {decrypted}");
                }
                Console.WriteLine(Environment.NewLine + $"Predd any key to continue ....");

                Console.ReadKey();

            }
            catch (Exception e)
            {
                Console.WriteLine(Environment.NewLine +$"Error: {e.Message}");
            }
        }

        public class Protection
        {
            /// <summary>
            /// Encrypt data with AES 
            /// </summary>
            /// <param name="data"></param>
            /// <param name="desKey"></param>
            /// <param name="desIV"></param>
            /// <returns></returns>
            public byte[] EncryptDataAes(string data, byte[] desKey, byte[] desIV)
            {
                //Check values
                if (data == null || data.Length <= 0) throw new ArgumentNullException("data");
                if (desKey == null || desKey.Length <= 0) throw new ArgumentNullException("desKey");
                if (desIV == null || desIV.Length <= 0) throw new ArgumentNullException("desIV");

                try
                {
                    //Create and AES object 
                    using (var aes = Aes.Create())
                    {
                        aes.Key = desKey;
                        aes.IV = desIV;
                        byte[] eData;
                        using (var ms = new MemoryStream())
                        {
                            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                            {
                                using (var sw = new StreamWriter(cs))
                                {
                                    // Write all data to the stream.
                                    sw.Write(data);
                                }
                                eData = ms.ToArray();
                            }
                        }
                        // Return the encryted from memory 
                        return eData;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error" + ex.InnerException);
                    throw;
                }

            }
            /// <summary>
            /// Decrypt data with AES 
            /// </summary>
            /// <param name="data"></param>
            /// <param name="desKey"></param>
            /// <param name="desIV"></param>
            /// <returns></returns>
            public string DecryptDataAes(byte[] data, byte[] desKey, byte[] desIV)
            {
                //Check values
                if (data == null || data.Length <= 0) throw new ArgumentNullException("data");
                if (desKey == null || desKey.Length <= 0) throw new ArgumentNullException("desKey");
                if (desIV == null || desIV.Length <= 0) throw new ArgumentNullException("desIV");

                //Create and AES object 
                using (var aes = Aes.Create())
                {
                    aes.Key = desKey;
                    aes.IV = desIV;
                    string dData;
                    using (var ms = new MemoryStream(data))
                    {
                        using (var cs = new CryptoStream(ms, aes.CreateDecryptor(aes.Key, aes.IV), CryptoStreamMode.Read))
                        {
                            using (var sr = new StreamReader(cs))
                            {
                                //Read decrypted bytes 
                               dData = sr.ReadToEnd();
                            }
                            //dData = ms.ToArray();
                        }
                    }

                    // Return the decryted from memory 
                    return dData;
                }
            }
        }

    }
}
