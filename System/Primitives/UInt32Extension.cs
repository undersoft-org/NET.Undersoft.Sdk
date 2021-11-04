/*************************************************
   Copyright (c) 2021 Undersoft

   System.UInt32Extension.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (28.05.2021) 
   @licence MIT
 *************************************************/

namespace System
{
    /// <summary>
    /// Defines the <see cref="UInt32Extension" />.
    /// </summary>
    public static class UInt32Extension
    {
        #region Methods      

        /// <summary>
        /// The IsEven.
        /// </summary>
        /// <param name="i">The i<see cref="uint"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsEven(this uint i)
        {
            return !((i & 1) != 0);
        }

        /// <summary>
        /// The IsOdd.
        /// </summary>
        /// <param name="i">The i<see cref="uint"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsOdd(this uint i)
        {
            return ((i & 1) != 0);
        }

        #endregion
    }
}
