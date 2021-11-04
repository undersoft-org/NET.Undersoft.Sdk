/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.Link.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (29.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Linking
{
    using System.Uniques;

    [Serializable]
    public class Link : IUnique
    {
        #region Fields

        private Ussn serialcode;

        #endregion

        #region Constructors

        public Link()
        {
        }
        public Link(IFigures origin, IFigures target)
        {
            LinkPair(origin, target);
        }
        public Link(IFigures origin, IFigures target, IRubric keyRubric) : this(origin, target)
        {
            LinkKeys(new MemberRubrics(new MemberRubric[] { (MemberRubric)keyRubric }));
        }
        public Link(IFigures origin, IFigures target, IRubrics keyRubrics) : this(origin, target)
        {
            LinkKeys(keyRubrics);
        }
        public Link(IFigures origin, IFigures target, string[] keyRubricNames) : this(origin, target)
        {
            LinkKeys(keyRubricNames);
        }

        #endregion

        #region Properties

        public IUnique Empty => Ussn.Empty;

        public string Name { get; set; }

        public LinkMember Origin { get; set; }

        public IRubrics OriginKeys
        {
            get
            {
                return Origin.KeyRubrics;
            }
            set
            {
                Origin.KeyRubrics.Renew(value);
                Origin.KeyRubrics.Update();
            }
        }

        public string OriginName
        {
            get { return Origin.Name; }
            set
            {
                Origin.Name = value;
            }
        }

        public IRubrics OriginRubrics
        {
            get
            {
                return Origin.Rubrics;
            }
            set
            {
                Origin.Rubrics = value;
            }
        }

        public Ussn SerialCode { get => serialcode; set => serialcode = value; }

        public LinkMember Target { get; set; }

        public IRubrics TargetKeys
        {
            get
            {
                return Target.KeyRubrics;
            }
            set
            {
                Target.KeyRubrics.Renew(value);
                Target.KeyRubrics.Update();
            }
        }

        public string TargetName
        {
            get { return Target.Name; }
            set
            {
                Target.Name = value;
            }
        }

        public IRubrics TargetRubrics
        {
            get
            {
                return Target.KeyRubrics;
            }
            set
            {
                Target.KeyRubrics = value;
            }
        }

        public ulong UniqueKey { get => serialcode.UniqueKey; set => serialcode.UniqueKey = value; }

        public ulong UniqueSeed { get => serialcode.UniqueSeed; set => serialcode.UniqueSeed = value; }

        #endregion

        #region Methods

        public int CompareTo(IUnique other)
        {
            return serialcode.CompareTo(other);
        }

        public bool Equals(IUnique other)
        {
            return serialcode.Equals(other);
        }

        public byte[] GetBytes()
        {
            return serialcode.GetBytes();
        }

        public byte[] GetUniqueBytes()
        {
            return serialcode.GetUniqueBytes();
        }

        public Link SetLink(IFigures origin, IFigures target, IRubrics keyRubrics)
        {
            LinkPair(origin, target);
            LinkKeys(keyRubrics);
            return this;
        }

        public Link SetLink(IFigures origin, IFigures target, string[] keyRubricNames)
        {
            LinkPair(origin, target);
            LinkKeys(keyRubricNames);
            return this;
        }

        public Link LinkPair(IFigures origin, IFigures target)
        {
            Name = origin.Type.FullName + "_&_" + target.Type.FullName;

            UniqueKey = Name.UniqueKey64();
            UniqueSeed = Name.UniqueKey32();

            Origin = new LinkMember(origin, this, LinkSite.Origin);
            Target = new LinkMember(target, this, LinkSite.Target);

            origin.Linker.TargetLinks.Put(this);
            target.Linker.OriginLinks.Put(this);

            return Linker.Map.Links.Put(this).Value;
        }

        public void LinkKeys(IRubrics keyRubrics)
        {
            foreach (IUnique rubric in keyRubrics)
{
                var originRubric = Origin.Rubrics[rubric];
                var targetRubric = Target.Rubrics[rubric];
                if (originRubric != null && targetRubric != null)
                {
                    OriginKeys.Add(originRubric);
                    TargetKeys.Add(targetRubric);
                }
                else
                    throw new IndexOutOfRangeException("Rubric not found");
                OriginKeys.Update();
                TargetKeys.Update();
            }
        }

        public void LinkKeys(string[] keyRubricNames)
        {
            foreach (var name in keyRubricNames)
            {
                var originRubric = Origin.Rubrics[name];
                var targetRubric = Target.Rubrics[name];
                if (originRubric != null && targetRubric != null)
                {
                    OriginKeys.Add(originRubric);
                    TargetKeys.Add(targetRubric);
                }
                else
                    throw new IndexOutOfRangeException("Rubric not found");
            }
            OriginKeys.Update();
            TargetKeys.Update();
        }

        #endregion
    }
}
