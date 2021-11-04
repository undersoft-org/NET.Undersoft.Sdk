/*************************************************
   Copyright (c) 2021 Undersoft

   System.Int64Extension.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (28.05.2021) 
   @licence MIT
 *************************************************/

namespace System
{
    /// <summary>
    /// Defines the <see cref="Int64Extension" />.
    /// </summary>
    public static class Int64Extension
    {
        #region Methods

        /// <summary>
        /// The IsEven.
        /// </summary>
        /// <param name="i">The i<see cref="long"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsEven(this long i)
        {
            return !((i & 1L) != 0);
        }

        /// <summary>
        /// The IsOdd.
        /// </summary>
        /// <param name="i">The i<see cref="long"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsOdd(this long i)
        {
            return ((i & 1) != 0);
        }

        /// <summary>
        /// The RemoveSign.
        /// </summary>
        /// <param name="i">The i<see cref="long"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        public static long RemoveSign(this long i)
        {
            return (long)(((ulong)i << 1) >> 1);
        }

        #endregion
    }
}
