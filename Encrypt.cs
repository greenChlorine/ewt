using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ewt360
{
    internal class Encrypt

    {
        public static string AES_Encrypt(string text)
        {
            byte[] keyArr = Encoding.UTF8.GetBytes("20171109124536982017110912453698");//创建一系列bytes数组
            byte[] textArr = Encoding.UTF8.GetBytes(text);
            byte[] ivArr = Encoding.UTF8.GetBytes("2017110912453698");
            AesCryptoServiceProvider aesCSP = new AesCryptoServiceProvider()
            {
                Key = keyArr,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                IV = ivArr
            };
            ICryptoTransform cryptoTransform = aesCSP.CreateEncryptor();
            byte[] resultArr = cryptoTransform.TransformFinalBlock(textArr, 0, textArr.Length);//加密后的byte数组，结果bytes数组

            //hex编码
            string hexStr = "";
            foreach (byte item in resultArr)
            {
                hexStr += item.ToString("X2");
            }
            return hexStr;
        }

        public static string HMACSHA1_Encrypt(string text, string key)
        {
            Encoding encode = Encoding.UTF8;
            byte[] byteData = encode.GetBytes(text);
            byte[] byteKey = encode.GetBytes(key);
            HMACSHA1 hmac = new HMACSHA1(byteKey);
            CryptoStream cs = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write);
            cs.Write(byteData, 0, byteData.Length);
            cs.Close();

            string hexStr = "";//转为16进制字符串
            for (int i = 0; i < hmac.Hash.Length; i++)
            {
                hexStr += hmac.Hash[i].ToString("x2");
            }

            return hexStr;
        }

        public static string md5_32(string text)
        {
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(text));

            string hex = "";
            for (int i = 0; i < result.Length; i++)
            {
                hex += result[i].ToString("x2");
            }
            return hex;

        }
    }
}
