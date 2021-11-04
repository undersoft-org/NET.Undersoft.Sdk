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

    public class MassCache<V> : MassCatalogBase<V> where V : IUnique
    {
        private TimeSpan duration = TimeSpan.FromMinutes(30);
        private Deputy callback;

        #region Constructors

        public MassCache(IEnumerable<IUnique<V>> collection, TimeSpan? lifeTime = null, Deputy callback = null, int capacity = 17) : base(collection, capacity)
        {
            if (lifeTime != null) duration = lifeTime.Value;
            if (callback != null) this.callback = callback;
        }
        public MassCache(IEnumerable<V> collection, TimeSpan? lifeTime = null, Deputy callback = null, int capacity = 17) : base(collection, capacity)
        {
            if (lifeTime != null) duration = lifeTime.Value;
            if (callback != null) this.callback = callback;
        }
        public MassCache(IList<IUnique<V>> collection, TimeSpan? lifeTime = null, Deputy callback = null, int capacity = 17) : base(collection, capacity)
        {
            if (lifeTime != null) duration = lifeTime.Value;
            if (callback != null) this.callback = callback;
        }
        public MassCache(IList<V> collection, TimeSpan? lifeTime = null, Deputy callback = null, int capacity = 17) : base(collection, capacity)
        {
            if (lifeTime != null) duration = lifeTime.Value;
            if (callback != null) this.callback = callback;
        }
        public MassCache(TimeSpan? lifeTime = null, Deputy callback = null, int capacity = 17) : base(capacity)
        {
            if (lifeTime != null) duration = lifeTime.Value;
            if (callback != null) this.callback = callback;
        }

        #endregion

        #region Methods

        public override ICard<V> EmptyCard()
        {
            return new CacheCard<V>() { Expiration = DateTime.Now + duration };
        }

        public override ICard<V>[] EmptyCardTable(int size)
        {
            return new CacheCard<V>[size];
        }

        public override ICard<V>[] EmptyDeck(int size)
        {
            return new CacheCard<V>[size];
        }

        public override ICard<V> NewCard(ICard<V> card)
        {
            return new CacheCard<V>(card, duration);
        }

        public override ICard<V> NewCard(object key, V value)
        {
            return new CacheCard<V>(key, value, duration);
        }

        public override ICard<V> NewCard(ulong key, V value)
        {
            return new CacheCard<V>(key, value, duration);
        }

        public override ICard<V> NewCard(V value)
        {
            return new CacheCard<V>(value, duration);
        }

        #endregion
    }
}
