/*************************************************
   Copyright (c) 2021 Undersoft

   System.Series.MassCatalog.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 *************************************************/

namespace System.Series
{
    using System.Collections.Generic;

    public class MassCatalog<V> : MassCatalogBase<V> where V : IUnique
    {
        #region Constructors

        public MassCatalog(IEnumerable<IUnique<V>> collection, int capacity = 17) : base(collection, capacity)
        {
        }
        public MassCatalog(IEnumerable<V> collection, int capacity = 17) : base(collection, capacity)
        {
        }
        public MassCatalog(IList<IUnique<V>> collection, int capacity = 17) : base(collection, capacity)
        {
        }
        public MassCatalog(IList<V> collection, int capacity = 17) : base(collection, capacity)
        {
        }
        public MassCatalog(int capacity = 17) : base(capacity)
        {
        }

        #endregion

        #region Methods

        public override ICard<V> EmptyCard()
        {
            return new MassCard<V>();
        }

        public override ICard<V>[] EmptyCardTable(int size)
        {
            return new MassCard<V>[size];
        }

        public override ICard<V>[] EmptyDeck(int size)
        {
            return new MassCard<V>[size];
        }

        public override ICard<V> NewCard(ICard<V> card)
        {
            return new MassCard<V>(card);
        }

        public override ICard<V> NewCard(object key, V value)
        {
            return new MassCard<V>(key, value);
        }

        public override ICard<V> NewCard(ulong key, V value)
        {
            return new MassCard<V>(key, value);
        }

        public override ICard<V> NewCard(V value)
        {
            return new MassCard<V>(value);
        }

        #endregion
    }
}
