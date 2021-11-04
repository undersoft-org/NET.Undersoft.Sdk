/*************************************************
   Copyright (c) 2021 Undersoft

   System.CryptoHash.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (28.05.2021) 
   @licence MIT
 *************************************************/

namespace System
{
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="CryptoHash" />.
    /// </summary>
    public static class CryptoHash
    {
        private static readonly KeyedHashAlgorithm ALGORITHM = new HMACSHA512();

        #region Methods

        /// <summary>
        /// The Encrypt.
        /// </summary>
        /// <param name="pass">The pass<see cref="string"/>.</param>
        /// <param name="format">The format<see cref="int"/>.</param>
        /// <param name="salt">The salt<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string Encrypt(string pass, string salt)
        {

            byte[] bIn = Encoding.Unicode.GetBytes(pass);
            byte[] bSalt = Convert.FromBase64String(salt);
            byte[] bRet = null;

            KeyedHashAlgorithm kha = ALGORITHM;

            if(kha.Key.Length == bSalt.Length)
            {
                kha.Key = bSalt;
            }
            else if(kha.Key.Length < bSalt.Length)
            {
                byte[] bKey = new byte[kha.Key.Length];
                Buffer.BlockCopy(bSalt, 0, bKey, 0, bKey.Length);
                kha.Key = bKey;
            }
            else
            {
                byte[] bKey = new byte[kha.Key.Length];
                for(int iter = 0; iter < bKey.Length;)
                {
                    int len = Math.Min(bSalt.Length, bKey.Length - iter);
                    Buffer.BlockCopy(bSalt, 0, bKey, iter, len);
                    iter += len;
                }

                kha.Key = bKey;
            }

            bRet = kha.ComputeHash(bIn);

            return Convert.ToBase64String(bRet);
        }

        /// <summary>
        /// The Salt.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public static string Salt()
        {
            return Convert.ToBase64String(DateTime.Now.ToLongDateString().ToBytes(CharEncoding.ASCII));
        }

        /// <summary>
        /// The Verify.
        /// </summary>
        /// <param name="hashedPassword">The hashedPassword<see cref="string"/>.</param>
        /// <param name="hashedSalt">The hashedSalt<see cref="string"/>.</param>
        /// <param name="providedPassword">The providedPassword<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool Verify(string hashedPassword, string hashedSalt, string providedPassword)
        {
            string salt = hashedSalt;
            if (String.Equals(Encrypt(providedPassword, salt), hashedPassword, StringComparison.CurrentCultureIgnoreCase))
                return true;
            else
                return false;
        }

        #endregion
    }
}
