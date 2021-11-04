/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.LinkCard.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (29.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant
{
    using Extract;
    using Linking;
    using Linq;
    using Series;
    using Uniques;


    public class BranchCard : CardBase<IFigure>, IFigure, IEquatable<IFigure>, IComparable<IFigure>
    {

        public object this[int fieldId]
        {
            get => ((IFigure)this.value)[fieldId];
            set => ((IFigure)this.value)[fieldId] = value;
        }
        public object this[string propertyName]
        {
            get => ((IFigure)this.value)[propertyName];
            set => ((IFigure)this.value)[propertyName] = value;
        }

        public void Set(object key, ICard<IFigure> value)
        {            
            value.UniqueKey = key.UniqueKey();
            Deck.Set(value);
        }
        public override void Set(ICard<IFigure> value)
        {
            value.Key = Key;
            Deck.Set(value);
        }

        public override bool Equals(ulong key)
        {
            return Key == key;
        }
        public override bool Equals(object y)
        {
            return Key.Equals(y.UniqueKey());
        }
        public override bool Equals(ICard<IFigure> other)
        {
            return Key == other.UniqueKey;
        }
        public bool Equals(IFigure other)
        {
            return Key == other.UniqueKey;
        }

        public override int GetHashCode()
        {
            return Value.GetUniqueBytes().BitAggregate64to32().ToInt32();
        }

        public override int CompareTo(object other)
        {
            return (int)(Key - other.UniqueKey64());
        }
        public override int CompareTo(ulong key)
        {
            return (int)(Key - key);
        }
        public override int CompareTo(ICard<IFigure> other)
        {
            return (int)(Key - other.UniqueKey);
        }
        public int CompareTo(IFigure other)
        {
            return (int)(Key - other.UniqueKey);
        }

        public override byte[] GetBytes()
        {
            return value.GetBytes();
        }

        public override byte[] GetUniqueBytes()
        {
            return value.GetUniqueBytes();
        }

        public override int[] UniqueOrdinals()
        {
            return Member.KeyRubrics.Ordinals;
        }
        public override object[] UniqueValues()
        {
            return Member.KeyRubrics.Ordinals.Select(x => value[x]).ToArray();
        }
        public override ulong CompactKey()
        {
            IRubrics r = Member.KeyRubrics;
            IFigure f = value;
            return r.Ordinals.SelectMany(x => f[x].GetBytes()).ToArray().UniqueKey64();//(r.BinarySizes, r.BinarySize, UniqueSeed);
        }

        public override void Set(object key, IFigure value)
        {
            Key = key.UniqueKey();
            Value = value;
            Deck.Set((ICard<IFigure>)this);
        }

        public override void Set(IFigure value)
        {          
            Value = value;
        }

        public override ulong Key
        {
            get => value.UniqueKey;
            set => this.value.UniqueKey = value;
        }

        public override ulong UniqueKey
        {
            get => value.UniqueKey;
            set => this.value.UniqueKey = value;
        }

        public override ulong UniqueSeed
        {
            get => Member.UniqueKey;
            set => Member.UniqueKey = value;
        }

        public object[] ValueArray
        {
            get
            {
                return ((IFigure)this.value).ValueArray;
            }
            set
            {
                ((IFigure)this.value).ValueArray = value;
            }
        }

        public Ussn SerialCode
        {
            get => value.SerialCode;
            set => this.Value.SerialCode = value;
        }

        public LinkMember Member { get; set; }
    }
}
