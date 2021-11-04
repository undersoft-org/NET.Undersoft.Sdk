/*************************************************
   Copyright (c) 2021 Undersoft

   System.UInt64Extension.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (28.05.2021) 
   @licence MIT
 *************************************************/

namespace System
{
    /// <summary>
    /// Defines the <see cref="UInt64Extension" />.
    /// </summary>
    public static class UInt64Extension
    {
        #region Methods

        /// <summary>
        /// The IsEven.
        /// </summary>
        /// <param name="i">The i<see cref="ulong"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsEven(this ulong i)
        {
            return !((i & 1UL) != 0);
        }

        /// <summary>
        /// The IsOdd.
        /// </summary>
        /// <param name="i">The i<see cref="ulong"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsOdd(this ulong i)
        {
            return ((i & 1UL) != 0);
        }

        #endregion
    }
}
