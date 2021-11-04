/*************************************************
   Copyright (c) 2021 Undersoft

   System.Series.Catalog.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 *************************************************/

namespace System.Series
{
using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Catalog<V> : CatalogBase<V>
    {
        #region Constructors

        public Catalog(IEnumerable<IUnique<V>> collection, int capacity = 17) : base(collection, capacity)
        {
        }
        public Catalog(IEnumerable<V> collection, int capacity = 17) : base(collection, capacity)
        {
        }
        public Catalog(IList<IUnique<V>> collection, int capacity = 17) : base(collection, capacity)
        {
        }
        public Catalog(IList<V> collection, int capacity = 17) : base(collection, capacity)
        {
        }
        public Catalog(int capacity = 17) : base(capacity)
        {
        }

        #endregion

        #region Methods

        public override ICard<V> EmptyCard()
        {
            return new Card<V>();
        }

        public override ICard<V>[] EmptyCardTable(int size)
        {
            return new Card<V>[size];
        }

        public override ICard<V>[] EmptyDeck(int size)
        {
            return new Card<V>[size];
        }

        public override ICard<V> NewCard(ICard<V> card)
        {
            return new Card<V>(card);
        }

        public override ICard<V> NewCard(object key, V value)
        {
            return new Card<V>(key, value);
        }

        public override ICard<V> NewCard(ulong key, V value)
        {
            return new Card<V>(key, value);
        }

        public override ICard<V> NewCard(V value)
        {
            return new Card<V>(value);
        }

        #endregion
    }
}
