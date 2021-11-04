/*************************************************
   Copyright (c) 2021 Undersoft

   System.Series.ICard.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 *************************************************/

namespace System.Series
{
    #region Interfaces

    public interface ICard<V> : IUnique<V>, IDisposable
    {
        #region Properties

        ICard<V> Extended { get; set; }

        int Index { get; set; }

        ulong Key { get; set; }

        ICard<V> Next { get; set; }

        bool Removed { get; set; }

        V Value { get; set; }

        IDeck<V> Deck { get; set; }

        #endregion

        #region Methods

        int CompareTo(ICard<V> other);

        int CompareTo(object other);

        int CompareTo(ulong key);

        bool Equals(ICard<V> y);

        bool Equals(object y);

        bool Equals(ulong key);

        int GetHashCode();

        Type GetUniqueType();

        void Set(ICard<V> card);

        void Set(object key, V value);

        void Set(ulong key, V value);

        void Set(V value);

        #endregion
    }

    #endregion
}
