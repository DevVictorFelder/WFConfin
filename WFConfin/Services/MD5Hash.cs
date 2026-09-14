using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Security.Cryptography;
using System.Text;

namespace WFConfin.Services
{
    public class MD5Hash
    {
        public static string CalcHash(string valor)
        {
            try
            {
                MD5 md5 = MD5.Create();
                Byte[] inputBytes = Encoding.ASCII.GetBytes(valor);
                Byte[] hash = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("2x"));
                }

                return sb.ToString();
            }
            catch (Exception e)
            {

            }

            return null;
        }
    }
}
