using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lib_utilities.Utilities
{
    public class EncryptHelper
    {
        private static string key = "b14ca5898a4e4133bbce2ea2315a1916";
        private static Aes? aes = null;

        public static void Create()
        {
            if (aes != null)
                return;
            aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16];
        }

        public static string EncryptKey(string value, string subKey = "")
        {
            Create();
            ICryptoTransform encryptor = aes!.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(cs))
                    {
                        writer.Write(value);
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static string DecryptKey(string value, string subKey = "")
        {
            Create();
            byte[] buffer = Convert.FromBase64String(value);
            ICryptoTransform decryptor = aes!.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }

        public static bool IsDecrypted(string value)
        {
            try
            {
                DecryptKey(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}