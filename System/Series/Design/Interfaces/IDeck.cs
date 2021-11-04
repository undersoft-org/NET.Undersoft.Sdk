/*************************************************
   Copyright (c) 2021 Undersoft

   System.Series.IDeck.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (01.06.2021) 
   @licence MIT
 *************************************************/

namespace System.Series
{
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public interface IDeck<V> : IEnumerable<V>, ICollection<V>, IList<V>, IProducerConsumerCollection<V>, IDisposable, IUnique
    {
        new V this[int index] { get; set; }
        V this[object key] { get; set; }

        ICard<V> First { get; }
        ICard<V> Last { get; }

        ICard<V> Next(ICard<V> card);

        new int Count { get; }

        bool ContainsKey(ulong key);
        bool ContainsKey(object key);
        bool ContainsKey(IUnique key);

        bool Contains(ICard<V> item);
        bool Contains(IUnique<V> item);

        V Get(object key);
        V Get(ulong key);
        V Get(IUnique key);
        V Get(IUnique<V> key);

        bool TryGet(object key, out ICard<V> output);
        bool TryGet(object key, out V output);
        bool TryGet(ulong key, out V output);
        bool TryGet(IUnique key, out ICard<V> output);
        bool TryGet(IUnique<V> key, out ICard<V> output);

        ICard<V> GetCard(object key);
        ICard<V> GetCard(ulong key);
        ICard<V> GetCard(IUnique key);
        ICard<V> GetCard(IUnique<V> key);

        ICard<V> Set(object key, V value);
        ICard<V> Set(ulong key, V value);
        ICard<V> Set(IUnique key, V value);
        ICard<V> Set(IUnique<V> key, V value);
        ICard<V> Set(V value);
        ICard<V> Set(IUnique<V> value);
        ICard<V> Set(ICard<V> value);
        int Set(IEnumerable<V> values);
        int Set(IList<V> values);
        int Set(IEnumerable<ICard<V>> values);
        int Set(IEnumerable<IUnique<V>> values);

        ICard<V> SureGet(object key, Func<ulong, V> sureaction);
        ICard<V> SureGet(ulong key, Func<ulong, V> sureaction);
        ICard<V> SureGet(IUnique key, Func<ulong, V> sureaction);
        ICard<V> SureGet(IUnique<V> key, Func<ulong, V> sureaction);

        ICard<V> AddNew();
        ICard<V> AddNew(ulong key);
        ICard<V> AddNew(object key);

        bool Add(object key, V value);
        bool Add(ulong key, V value);
        void Add(ICard<V> card);
        void Add(IList<ICard<V>> cardList);
        void Add(IEnumerable<ICard<V>> cards);
        void Add(IList<V> cards);
        void Add(IEnumerable<V> cards);
        void Add(IUnique<V> cards);
        void Add(IList<IUnique<V>> cards);
        void Add(IEnumerable<IUnique<V>> cards);

        bool Enqueue(object key, V value);
        void Enqueue(ICard<V> card);
        bool Enqueue(V card);

        V Dequeue();
        bool TryDequeue(out ICard<V> item);
        bool TryDequeue(out V item);
        new bool TryTake(out V item);

        bool TryPick(int skip, out V output);
        bool TryPick(int skip, out ICard<V> output);

        ICard<V> Put(object key, V value);
        ICard<V> Put(ulong key, V value);
        ICard<V> Put(ICard<V> card);
        void Put(IList<ICard<V>> cardList);
        void Put(IEnumerable<ICard<V>> cards);
        void Put(IList<V> cards);
        void Put(IEnumerable<V> cards);
        ICard<V> Put(V value);
        ICard<V> Put(IUnique<V> cards);
        void Put(IList<IUnique<V>> cards);
        void Put(IEnumerable<IUnique<V>> cards);

        V Remove(object key);
        bool Remove(ICard<V> item);
        bool Remove(IUnique<V> item);
        bool TryRemove(object key);

        void Renew(IEnumerable<V> cards);
        void Renew(IList<V> cards);
        void Renew(IList<ICard<V>> cards);
        void Renew(IEnumerable<ICard<V>> cards);

        new V[] ToArray();

        IEnumerable<ICard<V>> AsCards();

        IEnumerable<V> AsValues();

        IEnumerable<IUnique<V>> AsIdentifiers();

        new void CopyTo(Array array, int arrayIndex);

        new bool IsSynchronized { get; set; }
        new object SyncRoot { get; set; }

        ICard<V> NewCard(V value);
        ICard<V> NewCard(object key, V value);
        ICard<V> NewCard(ulong key, V value);
        ICard<V> NewCard(ICard<V> card);

        void CopyTo(ICard<V>[] array, int destIndex);
        void CopyTo(IUnique<V>[] array, int arrayIndex);
        //void CopyTo(V[] array, int index);

        new void Clear();

        void Resize(int size);

        void Flush();
    }
}
