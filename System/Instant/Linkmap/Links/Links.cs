/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.Links.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (29.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Linking
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Series;
    using System.Uniques;

    #region Enums

    public enum LinkSite
    {
        None,
        Origin,
        Target
    }

    #endregion

    public class Links : CatalogBase<Link>, IUnique
    {
        private new Ussn serialcode;

        public Links()
        {
        }
        public Links(IList<Link> links)
        {
            Add(links);
        }

        public Link this[string linkName]
        {
            get
            {
                return base[linkName];
            }
            set
            {
                base[linkName] = value;
            }
        }
        public new Link this[int linkid]
        {
            get
            {
                return base[linkid];
            }
            set
            {
                base[linkid] = value;
            }
        }

        public Link TargetLink(string TargetName)
        {
            return AsValues().Where(o => o.TargetName.Equals(TargetName)).FirstOrDefault();
        }
        public Link OriginLink(string OriginName)
        {
            return AsValues().Where(o => o.OriginName.Equals(OriginName)).FirstOrDefault();
        }

        public LinkMember TargetMember(string TargetName)
        {
            Link link = TargetLink(TargetName);
            if (link != null)
                return link.Target;
            return null;
        }
        public LinkMember OriginMember(string OriginName)
        {
            Link link = OriginLink(OriginName);
            if (link != null)
                return link.Origin;
            return null;
        }

        public override ICard<Link> EmptyCard()
        {
            return new Card<Link>();
        }

        public override ICard<Link> NewCard(ulong key, Link value)
        {
            return new Card<Link>(key, value);
        }
        public override ICard<Link> NewCard(object key, Link value)
        {
            return new Card<Link>(key, value);
        }
        public override ICard<Link> NewCard(ICard<Link> value)
        {
            return new Card<Link>(value);
        }
        public override ICard<Link> NewCard(Link value)
        {
            return new Card<Link>(value);
        }

        public override ICard<Link>[] EmptyCardTable(int size)
        {
            return new Card<Link>[size];
        }

        public override ICard<Link>[] EmptyDeck(int size)
        {
            return new Card<Link>[size];
        }

        protected override bool InnerAdd(Link value)
        {
            return InnerAdd(NewCard(value));
        }

        protected override ICard<Link> InnerPut(Link value)
        {
            return InnerPut(NewCard(value));
        }

        public Ussn SerialCode
        {
            get => serialcode;
            set => serialcode = value;
        }

        public new IUnique Empty => Ussn.Empty;

        public override ulong UniqueKey
        {
            get => serialcode.UniqueKey;
            set => serialcode.UniqueKey = value;
        }

        public override ulong UniqueSeed
        {
            get => serialcode.UniqueSeed;
            set => serialcode.UniqueSeed = value;
        }

        public override int CompareTo(IUnique other)
        {
            return serialcode.CompareTo(other);
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
    }
}
