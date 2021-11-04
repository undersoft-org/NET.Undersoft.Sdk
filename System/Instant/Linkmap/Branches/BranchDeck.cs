/*************************************************
   Copyright (c) 2021 Undersoft

   LinkBranch.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

namespace System.Instant.Linking
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Series;
    using System.Uniques;

    [Serializable]
    public class BranchDeck : BoardBase<IFigure>, IUnique, ICard<IFigure>
    {
        #region Fields

        protected IFigure value;    
        private ICard<IFigure> extended;
        private ICard<IFigure> next;
        private IDeck<IFigure> deck;

        #endregion

        #region Constructors

        public BranchDeck(Link link, ICard<IFigure> value) : base(7)
        {
            Link = link;
            UniqueKey = link.UniqueKey;
            InnerAdd(value);
        }
        public BranchDeck(Link link, ICard<IFigure> value, ulong linkkey) : base(7)
        {
            Link = link;
            UniqueKey = linkkey;
            InnerAdd(value);
        }
        public BranchDeck(Link link, ICard<IFigure> value, int capacity = 8) : base(capacity)
        {
            Link = link;
            UniqueKey = link.UniqueKey;
            InnerAdd(value);
        }
        public BranchDeck(Link link, ICollection<ICard<IFigure>> collections, int capacity = 7) : base(capacity)
        {
            Link = link;
            UniqueKey = link.UniqueKey;
            foreach (var card in collections.Skip(1))
                InnerAdd(card);
        }
        public BranchDeck(Link link, IEnumerable<ICard<IFigure>> collections, int capacity = 7) : base(capacity)
        {
            Link = link;
            if (collections.Any())
            {
                var val = collections.First();
                UniqueKey = link.UniqueKey;
                InnerAdd(val);
            }
            foreach (var card in collections.Skip(1))
                InnerAdd(card);
        }
        public BranchDeck(Link link) : base(9)
        {
            Link = link;
            UniqueKey = link.UniqueKey;
        }

        #endregion

        #region Properties

        public new IUnique Empty => Ussn.Empty;

        public Link Link { get; set; }

        public Ussn SerialCode { get => serialcode; set => serialcode = value; }

        public virtual IFigure UniqueObject
        {
            get => value;
            set
            {
                this.value = value;
            }
        }

        public override ulong UniqueSeed
        {
            get
            {
                if (UniqueObject.UniqueSeed == 0)
                    UniqueObject.UniqueSeed = value.GetType().FullName.UniqueKey32();
                return UniqueObject.UniqueSeed;
            }
            set
            {
                UniqueObject.UniqueSeed = value;
            }
        }

        public ICard<IFigure> Extended { get; set; }
        
        public int Index { get; set; }
        
        public new ulong Key { get => serialcode.UniqueKey; set => serialcode.UniqueKey = value; }

        public new ICard<IFigure> Next { get => next; set => next = value; }

        public bool Removed { get; set; }
        
        public IFigure Value { get ; set; }

        public virtual IDeck<IFigure> Deck { get => deck; set => deck = value; }

        #endregion

        #region Methods

        public  override int CompareTo(IUnique other)
        {
            return serialcode.CompareTo(other);
        }

        public override ICard<IFigure> EmptyCard()
        {
            return new BranchCard();
        }

        public override ICard<IFigure>[] EmptyCardTable(int size)
        {
            return new ICard<IFigure>[size];
        }

        public override bool Equals(IUnique other)
        {
            return serialcode.Equals(other);
        }

        public override byte[] GetBytes()
        {
            return serialcode.GetBytes();
        }

        public override byte[] GetUniqueBytes()
        {
            return serialcode.GetUniqueBytes();
        }

        public override ICard<IFigure> NewCard(ICard<IFigure> value)
        {
            return value;
        }       

        public ICard<IFigure> NewCard(object key, ICard<IFigure> value)
        {
            value.Key = key.UniqueKey64();
            return value;
        }

        public ICard<IFigure> NewCard(ulong key, ICard<IFigure> value)
        {
            value.Key = key;
            return value;
        }

        protected override bool InnerAdd(ICard<IFigure> value)
        {
            return InnerAdd(value);
        }

        protected override ICard<IFigure> InnerPut(ICard<IFigure> value)
        {
            return base.InnerPut(value);
        }

        public ICard<IFigure>[] EmptyDeck(int size)
        {
            return new ICard<IFigure>[size];
        }

        public override ICard<IFigure> NewCard(ulong key, IFigure value)
        {
            return Deck.Put(key, value);
        }

        public override ICard<IFigure> NewCard(object key, IFigure value)
        {
            return Deck.Put(key, value);
        }

        public override ICard<IFigure> NewCard(IFigure card)
        {
            return Deck.Put(card);
        }      

        public int[] UniqueOrdinals()
        {
            throw new NotImplementedException();
        }

        public ulong CompactKey()
        {
            return Key;
        }

        public object[] UniqueValues()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object y)
        {
            return Key.Equals(y.UniqueKey64());
        }

        public override int GetHashCode()
        {
            return (int)Key;
        }

        public int CompareTo(ICard<IFigure> other)
        {
            return serialcode.CompareTo(other);
        }

        public int CompareTo(object other)
        {
            return serialcode.CompareTo(other);
        }

        public int CompareTo(ulong key)
        {
            return serialcode.CompareTo(key);
        }

        public bool Equals(ICard<IFigure> y)
        {
            return serialcode.Equals(y);
        }

        public bool Equals(ulong key)
        {
            return serialcode.Equals(key);
        }

        public Type GetUniqueType()
        {
            return value.GetType();
        }

        void ICard<IFigure>.Set(ICard<IFigure> card)
        {
            Deck.Set(card);
        }

        void ICard<IFigure>.Set(object key, IFigure value)
        {
            Deck.Set(key, value);
        }

        void ICard<IFigure>.Set(ulong key, IFigure value)
        {
            Deck.Set(key, value);
        }

        void ICard<IFigure>.Set(IFigure value)
        {
            Deck.Set(value);
        }

        #endregion
    }
}
