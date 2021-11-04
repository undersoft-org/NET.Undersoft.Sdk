/*********************************************************************************       
    Copyright (c) 2020 Undersoft

    System.Series.BaseSpectrum
    
    @authors Darius Hanc & PhD Radek Rudek 
    @project Vagas.Sdk                                    
    @version 0.8.D (Feb 7, 2020)                                           
    @licence MIT
 **********************************************************************************/

namespace System.Series
{
    public abstract class SpectrumBase
    {
        #region Properties

        public abstract int IndexMax { get; }

        public abstract int IndexMin { get; }

        public abstract int Size { get; }

        #endregion

        #region Methods

        public abstract void Add(int baseOffset, int offsetFactor, int indexOffset, int x);

        public abstract void Add(int x);

        public abstract bool Contains(int baseOffset, int offsetFactor, int indexOffset, int x);

        public abstract bool Contains(int x);

        public abstract void FirstAdd(int baseOffset, int offsetFactor, int indexOffset, int x);

        public abstract void FirstAdd(int x);

        public abstract int Next(int baseOffset, int offsetFactor, int indexOffset, int x);

        public abstract int Next(int x);

        public abstract int Previous(int baseOffset, int offsetFactor, int indexOffset, int x);

        public abstract int Previous(int x);

        public abstract bool Remove(int baseOffset, int offsetFactor, int indexOffset, int x);

        public abstract bool Remove(int x);

        #endregion
    }
}
