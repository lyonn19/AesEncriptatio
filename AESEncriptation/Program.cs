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

                    //byte[]
                    Console.WriteLine("Fingerprint Encryptation ....");
                    var data =
                    Convert.FromBase64String(
                   "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");

                    
                    using (var a = new FileStream("encriptadoOrig.wsq", FileMode.Create))
                    {
                        a.Write(data, 0, data.Length);
                    }

                    var result = protection.EncryptDataAes2(data, aes.Key, aes.IV);


                    using (var a = new FileStream("encriptado.wsq", FileMode.Create))
                    {
                        a.Write(result, 0, result.Length);
                    }

                    var dresult = protection.DecryptDataAes2(result, aes.Key, aes.IV);

                    using (var a = new FileStream("desencriptado.wsq", FileMode.Create))
                    {
                        //string converted = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                        a.Write(dresult, 0, dresult.Length);
                    }

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

            public byte[] EncryptDataAes2(byte[] data, byte[] desKey, byte[] desIV)
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
                                //using (var sw = new StreamWriter(cs))
                                //{
                                // Write all data to the stream.
                                //    sw.Write(data);
                                //}
                                cs.Write(data, 0, data.Length);
                            }
                            eData = ms.ToArray();
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

            public byte[] DecryptDataAes2(byte[] data, byte[] desKey, byte[] desIV)
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
                    byte[] dData;
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, aes.CreateDecryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                        {
                            //using (var sr = new StreamReader(cs))
                            //{
                            //Read decrypted bytes 
                            //    sr.ReadToEnd();
                            //}
                            cs.Write(data, 0, data.Length);
                        }

                        dData = ms.ToArray();
                    }

                    // Return the decryted from memory 
                    return dData;
                }
            }
        }

    }
}
