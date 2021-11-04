/******************************************
   Copyright (c) 2021 Undersoft

   System.Series.BaseCard.cs
    
    Card abstract class. 
    Reference type of common used 
    value type Bucket in Hashtables.
    Include properties: 
    Key - long abstract property to implement different
          type fields with hashkey like long, int etc.
    Value - Generic type property to store collection item.
    Next - for one site list implementation. 
    Extended - for one site list hash conflict items
    Removed - flag for removed items to skip before
              removed items limit exceed and rehash
              process executed
        
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 ****************************************/

namespace System.Series
{
    using System;
    using System.Runtime.InteropServices;
    using System.Uniques;

    [StructLayout(LayoutKind.Sequential)]
    public abstract class CardBase<V> : IEquatable<ICard<V>>, IEquatable<object>, IEquatable<ulong>, IComparable<object>,
                                    IComparable<ulong>, IComparable<ICard<V>>, ICard<V>
    {
        #region Fields

        protected V value;
        protected bool isUnique = false;
        private bool disposedValue = false;
        private ICard<V> extended;
        private ICard<V> next;
        private IDeck<V> deck;


        #endregion

        #region Constructors

        public CardBase()
        {
            isUnique = typeof(V).IsAssignableTo(typeof(IUnique));
        }
        public CardBase(ICard<V> value) : base()
        {
            Set(value);
        }
        public CardBase(object key, V value) : base()
        {
            Set(key, value);
        }
        public CardBase(ulong key, V value) : base()
        {
            Set(key, value);
        }
        public CardBase(V value) : base()
        {
            Set(value);
        }

        #endregion

        #region Properties

        public virtual     IDeck<V> Deck { get => deck; set => deck = value; }

        public virtual     IUnique Empty => throw new NotImplementedException();

        public virtual ICard<V> Extended { get => extended; set => extended = value; }

        public virtual         int Index { get; set; } = -1;

        public abstract        ulong Key { get; set; }

        public virtual     ICard<V> Next { get => next; set => next = value; }

        public virtual      bool Removed { get; set; }

        public virtual      bool IsUnique { get => isUnique; set => isUnique = value; }

        public virtual   ulong UniqueKey { get => Key; set => Key = value; }

        public virtual     V UniqueObject
        {
            get => value;
            set => this.value = value;
        }

        public virtual ulong   UniqueSeed
        {
            get
            {
                if (isUnique)
                {
                    var uniqueValue = (IUnique)UniqueObject;
                    if (uniqueValue.UniqueSeed == 0)
                        uniqueValue.UniqueSeed = typeof(V).UniqueKey32();
                    return uniqueValue.UniqueSeed;
                }
                return typeof(V).UniqueKey32();
            }
            set
            {
                if (isUnique)
                {
                    var uniqueValue = (IUnique)UniqueObject;
                    uniqueValue.UniqueSeed = value;
                }
            }
        }

        public virtual           V Value { get => value; set => this.value = value; }

        #endregion

        #region Methods

        public virtual ulong CompactKey()
        {
            return (isUnique) ? ((IUnique)UniqueObject).UniqueKey : Key;
        }

        public virtual int CompareTo(ICard<V> other)
        {
            return (int)(Key - other.Key);
        }

        public virtual int CompareTo(IUnique other)
        {
            return (int)(Key - other.UniqueKey);
        }

        public abstract int CompareTo(object other);

        public virtual int CompareTo(ulong key)
        {
            return (int)(Key - key);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public virtual bool Equals(ICard<V> y)
        {
            return this.Equals(y.Key);
        }

        public virtual bool Equals(IUnique other)
        {
            return Key == other.UniqueKey;
        }

        public override abstract bool Equals(object y);

        public virtual bool Equals(ulong key)
        {
            return Key == key;
        }

        public abstract byte[] GetBytes();

        public override abstract int GetHashCode();

        public abstract byte[] GetUniqueBytes();

        public virtual Type GetUniqueType()
        {
            return typeof(V);
        }

        public abstract void Set(ICard<V> card);

        public abstract void Set(object key, V value);

        public virtual void Set(ulong key, V value)
        {
            this.value = value;
            Key = key;
        }

        public abstract void Set(V value);

        public virtual int[] UniqueOrdinals()
        {
            return null;
        }

        public virtual object[] UniqueValues()
        {
            return new object[] { Key };
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Value = default(V);
                }

                disposedValue = true;
            }
        }

        #endregion
    }
}
