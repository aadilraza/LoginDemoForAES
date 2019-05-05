using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DemoServer.Models;
using olympic.Controllers;

namespace DemoServer.Controllers
{

    public class AESController : ApiController
    {
        [Route("api/AESAlgorithumEncrypt")]
        [HttpPost, HttpGet]
        public HttpResponseMessage AESAlgorithumEncrypt([FromBody] AESParameters param)
        {        
            string encrypted = Encrypt(param.PlainText, param.Key);
            //string decrypted = Decrypt(encrypted, param.Key);
            AESManaged obj = new AESManaged()
            {
                Encrypted = encrypted
               // Decrypted = decrypted 
            };          
            try
            {
                var json = new JavaScriptSerializer().Serialize(obj);
                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent(json)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return resp;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Route("api/AESAlgorithumDecrypt")]
        [HttpPost, HttpGet]
        public HttpResponseMessage AESAlgorithumDecrypt([FromBody] AESParameters param)
        {
            string decrypted = Decrypt(param.cipherText, param.Key);
            AESManaged obj = new AESManaged()
            {
                Decrypted = decrypted
            };
            try
            {
                var json = new JavaScriptSerializer().Serialize(obj);
                var resp = new HttpResponseMessage()
                {
                    Content = new StringContent(json)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return resp;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public partial class AESParameters
        {
            public string PlainText { get; set; }
            public string cipherText { get; set; }
            public string Key { get; set; }
        }
        private partial class AESManaged
        {
            public string Encrypted { get; set; }
            public string Decrypted { get; set; }
        }
        #region Settings

        private static int _iterations = 2;
        private static int _keySize = 256;

        private static string _hash = "SHA1";
        private static string _salt = "aselrias38490a32"; // Random
        private static string _vector = "8947az34awl34kjq"; // Random

        #endregion
        public static string Encrypt(string value, string password)
        {
            return Encrypt<AesManaged>(value, password);
        }
        public static string Encrypt<T>(string value, string password)
                where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(_vector);

            byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(_salt);
            byte[] valueBytes = ASCIIEncoding.ASCII.GetBytes(value);

            byte[] encrypted;
            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes =
                    new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                cipher.Mode = CipherMode.CBC;

                using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                {
                    using (MemoryStream to = new MemoryStream())
                    {
                        using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(valueBytes, 0, valueBytes.Length);
                            writer.FlushFinalBlock();
                            encrypted = to.ToArray();
                        }
                    }
                }
                cipher.Clear();
            }
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string value, string password)
        {
            return Decrypt<AesManaged>(value, password);
        }
        public static string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(_vector);
            byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(_salt);
            byte[] valueBytes = Convert.FromBase64String(value);

            byte[] decrypted;
            int decryptedByteCount = 0;

            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                cipher.Mode = CipherMode.CBC;

                try
                {
                    using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                    {
                        using (MemoryStream from = new MemoryStream(valueBytes))
                        {
                            using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                            {
                                decrypted = new byte[valueBytes.Length];
                                decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }

                cipher.Clear();
            }
            return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
        }

       
    }
}
