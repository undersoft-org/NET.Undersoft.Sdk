/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.LinkMember.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (29.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Linking
{
    using System.Extract;
    using System.Linq;
    using System.Uniques;

    [Serializable]
    public class LinkMember : IUnique
    {
        #region Fields

        public int BranchesCount = 0;
        private Ussc serialcode;

        #endregion

        #region Constructors

        public LinkMember()
        {
            KeyRubrics = new MemberRubrics();
        }
        public LinkMember(IFigures figures, Link link, LinkSite site) : this()
        {
            string[] names = link.Name.Split("_&_");
            LinkMember member;
            Site = site;
            Link = link;

            int siteId = 1;

            if (site == LinkSite.Origin)
            {
                siteId = 0;
                member = Link.Origin;
            }
            else
                member = Link.Target;

            Name = names[siteId];
            UniqueKey = names[siteId].UniqueKey64(link.UniqueKey);
            UniqueSeed = link.UniqueKey;
            Rubrics = figures.Rubrics;
            Figures = figures;
            member.Figures = figures;
        }
        public LinkMember(Link link, LinkSite site) : this()
        {
            string[] names = link.Name.Split("_&_");
            Site = site;
            Link = link;
            LinkMember member;
            int siteId = 1;

            if (site == LinkSite.Origin)
            {
                siteId = 0;
                member = Link.Origin;
            }
            else
                member = Link.Target;

            Name = names[siteId];
            UniqueKey = names[siteId].UniqueKey64(link.UniqueKey);
            UniqueSeed = link.UniqueKey;
            Rubrics = member.Figures.Rubrics;
            Figures =member.Figures;
        }

        #endregion

        #region Properties

        public IUnique Empty => Ussc.Empty;

        public IFigures Figures { get; set; }

        public IRubrics KeyRubrics { get; set; }

        public Link Link { get; set; }

        public string Name { get; set; }

        public IRubrics Rubrics { get; set; }

        public Ussc SerialCode { get => serialcode; set => serialcode = value; }

        public LinkSite Site { get; set; }

        public ulong UniqueKey { get => serialcode.UniqueKey; set => serialcode.UniqueKey = value; }

        public ulong UniqueSeed { get => serialcode.UniqueSeed; set => serialcode.UniqueSeed = value; }

        #endregion

        #region Methods

        public int CompareTo(IUnique other)
        {
            return SerialCode.CompareTo(other);
        }

        public bool Equals(IUnique other)
        {
            return SerialCode.Equals(other);
        }

        public byte[] GetBytes()
        {
            return serialcode.GetBytes();
        }

        public byte[] GetUniqueBytes()
        {
            return serialcode.GetUniqueBytes();
        }

        public unsafe ulong LinkKey(IFigure figure)
        {
            byte[] b = KeyRubrics.Ordinals
                .SelectMany(x => figure[x].GetBytes()).ToArray();

            int l = b.Length;
            fixed(byte* pb = b)
            {
                return Hasher64.ComputeKey(pb, l);
            }
        }

        #endregion
    }
}
