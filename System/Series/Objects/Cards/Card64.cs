/*************************************************
   Copyright (c) 2021 Undersoft

   System.Series.Card64.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 *************************************************/

namespace System.Series
{
    using System.Runtime.InteropServices;
    using System.Uniques;

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class Card64<V> : CardBase<V>
    {
        #region Fields

        private ulong _key;

        #endregion

        #region Constructors

        public Card64()
        {
        }
        public Card64(ICard<V> value) : base(value)
        {
        }
        public Card64(object key, V value) : base(key, value)
        {
        }
        public Card64(ulong key, V value) : base(key, value)
        {
        }
        public Card64(V value) : base(value)
        {
        }

        #endregion

        #region Properties

        public override ulong Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        #endregion

        #region Methods

        public override int CompareTo(ICard<V> other)
        {
            return (int)(Key - other.Key);
        }

        public override int CompareTo(object other)
        {
            return (int)(Key - other.UniqueKey64());
        }

        public override int CompareTo(ulong key)
        {
            return (int)(Key - key);
        }

        public override bool Equals(object y)
        {
            return Key.Equals(y.UniqueKey64());
        }

        public override bool Equals(ulong key)
        {
            return Key == key;
        }

        public override byte[] GetBytes()
        {
            return GetUniqueBytes();
        }

        public override int GetHashCode()
        {
            return (int)Key;
        }

        public unsafe override byte[] GetUniqueBytes()
        {
            byte[] b = new byte[8];
            fixed(byte* s = b)
                *(ulong*)s = _key;
            return b;
        }

        public override void Set(ICard<V> card)
        {
            this.value = card.Value;
            _key = card.Key;
        }

        public override void Set(object key, V value)
        {
            this.value = value;
            _key = key.UniqueKey64();
        }

        public override void Set(V value)
        {
            this.value = value;
            _key = value.UniqueKey64();
        }

        #endregion
    }
}
