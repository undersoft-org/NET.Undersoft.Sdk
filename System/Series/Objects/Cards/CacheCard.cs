/*************************************************
   Copyright (c) 2021 Undersoft

   System.Series.MassCard.cs
   
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
    using System.Extract;

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class CacheCard<V> : CardBase<V>
    {
        #region Fields

        private ulong _key;

        #endregion

        #region Constructors

        public CacheCard() : base()
        {          
            Expiration = DateTime.Now + TimeSpan.FromMinutes(30);
        }
        public CacheCard(ICard<V> value, TimeSpan? lifeTime = null) : base(value)
        {
            lifeTime ??= TimeSpan.FromMinutes(30);
            Expiration = DateTime.Now + lifeTime.Value;
        }
        public CacheCard(object key, V value, TimeSpan? lifeTime = null) : base(key, value)
        {
            lifeTime ??= TimeSpan.FromMinutes(30);
            Expiration = DateTime.Now + lifeTime.Value;
        }
        public CacheCard(ulong key, V value, TimeSpan? lifeTime = null) : base(key, value)
        {
            lifeTime ??= TimeSpan.FromMinutes(30);
            Expiration = DateTime.Now + lifeTime.Value;
        }
        public CacheCard(V value, TimeSpan? lifeTime = null) : base(value)
        {
            lifeTime ??= TimeSpan.FromMinutes(30);
            Expiration = DateTime.Now + lifeTime.Value;
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

        public DateTime Expiration { get; set; }

        #endregion

        #region Methods

        public override int CompareTo(ICard<V> other)
        {
            return (int)(Key - other.Key);
        }

        public override int CompareTo(object other)
        {
            return (int)(Key - other.UniqueKey64(UniqueSeed));
        }

        public override int CompareTo(ulong key)
        {
            return (int)(Key - key);
        }

        public override bool Equals(object y)
        {
            return Key.Equals(y.UniqueKey64(UniqueSeed));
        }

        public override bool Equals(ulong key)
        {
            return Key == key;
        }

        public override byte[] GetBytes()
        {
            return this.value.GetBytes();
        }

        public override int GetHashCode()
        {
            return (int)Key.UniqueKey32();
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
            value = card.Value;
            _key = card.Key;
        }

        public override void Set(object key, V value)
        {
            this.value = value;
            _key = key.UniqueKey64(UniqueSeed);
        }

        public override void Set(V value)
        {
            this.value = value;
            if(this.value is IUnique<V>)
            _key = ((IUnique<V>)value).CompactKey();
        }

        #endregion
    }
}
