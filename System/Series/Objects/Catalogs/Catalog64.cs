/*************************************************
   Copyright (c) 2021 Undersoft

   System.Series.Catalog64.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 *************************************************/

namespace System.Series
{
    using System.Collections.Generic;
    using System.Uniques;

    public class Catalog64<V> : CatalogBase<V>
    {
        #region Constructors

        public Catalog64(IEnumerable<IUnique<V>> collection, int capacity = 17) : this(capacity)
        {
            foreach(var c in collection)
                this.Add(c);
        }
        public Catalog64(IEnumerable<V> collection, int capacity = 17) : this(capacity)
        {
            foreach(var c in collection)
                this.Add(c);
        }
        public Catalog64(IList<IUnique<V>> collection, int capacity = 17) : this(capacity > collection.Count ? capacity : collection.Count)
        {
            foreach(var c in collection)
                this.Add(c);
        }
        public Catalog64(IList<V> collection, int capacity = 17) : this(capacity > collection.Count ? capacity : collection.Count)
        {
            foreach(var c in collection)
                this.Add(c);
        }
        public Catalog64(int capacity = 17) : base(capacity, HashBits.bit64)
        {
        }

        #endregion

        #region Methods

        public override ICard<V> EmptyCard()
        {
            return new Card64<V>();
        }

        public override ICard<V>[] EmptyCardTable(int size)
        {
            return new Card64<V>[size];
        }

        public override ICard<V>[] EmptyDeck(int size)
        {
            return new Card64<V>[size];
        }

        public override ICard<V> NewCard(ICard<V> card)
        {
            return new Card64<V>(card);
        }

        public override ICard<V> NewCard(object key, V value)
        {
            return new Card64<V>(key, value);
        }

        public override ICard<V> NewCard(ulong key, V value)
        {
            return new Card64<V>(key, value);
        }

        public override ICard<V> NewCard(V value)
        {
            return new Card64<V>(value);
        }

        #endregion
    }
}
