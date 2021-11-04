/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.Linkmap.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (29.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Linking
{
    using Linq;
    using Series;
    using Uniques;

    #region Interfaces

    public interface ILinker
    {
        #region Properties

        Links OriginLinks { get; }

        Links TargetLinks { get; }

        #endregion

        #region Methods

        void Clear();

        IFigure GetOrigin(IFigure target, string OriginName);

        IDeck<IFigure> GetOrigins(IFigures target, string OriginName);

        IFigure GetTarget(IFigure origin, string TargetName);

        IDeck<IFigure> GetTargets(IFigures origin, string TargetName);

        #endregion
    }

    #endregion

    [Serializable]
    public class Linker : ILinker
    {
        #region Fields

        private static NodeCatalog map = new NodeCatalog(new Links(), PRIMES_ARRAY.Get(9));
        private Links originLinks;
        private Links targetLinks;

        #endregion

        #region Constructors

        public Linker()
        {
            originLinks = new Links();
            targetLinks = new Links();
        }

        #endregion

        #region Properties

        public static NodeCatalog Map { get => map; }

        public IFigures Figures { get; set; }

        public Links OriginLinks { get => originLinks; }

        public Links TargetLinks { get => targetLinks; }

        #endregion

        #region Methods

        public void Clear()
        {
            Map.Flush();
        }

        public IFigure GetOrigin(IFigure figure, string OriginName)
        {
            return map[OriginKey(figure, OriginName)];
        }

        public Link GetOriginLink(string OriginName)
        {
            return OriginLinks[OriginName + "_" + Figures.Instant.Name];
        }

        public LinkMember GetOriginMember(string OriginName)
        {
            Link link = GetOriginLink(OriginName);
            if (link != null)
                return link.Origin;
            return null;
        }

        public IDeck<IFigure> GetOrigins(IFigures figures, string OriginName)
        {
            var originMember = GetOriginMember(OriginName);
            return new Album<IFigure>(figures.Select(f => map[originMember.LinkKey(f)]), 255);
        }

        public IFigure GetTarget(IFigure figure, string TargetName)
        {
            return map[TargetKey(figure, TargetName)];
        }

        public Link GetTargetLink(string TargetName)
        {
            return TargetLinks[Figures.Instant.Name + "_&_" + TargetName];
        }

        public LinkMember GetTargetMember(string TargetName)
        {
            Link link = GetTargetLink(TargetName);
            if (link != null)
                return link.Target;
            return null;
        }

        public IDeck<IFigure> GetTargets(IFigures figures, string TargetName)
        {
            var targetMember = GetTargetMember(TargetName);
            return new Album<IFigure>(figures.Select(f => map[targetMember.LinkKey(f)]).ToArray(), 255);
        }

        public ulong OriginKey(IFigure figure, string OriginName)
        {
            return GetOriginMember(OriginName).LinkKey(figure);
        }

        public ulong TargetKey(IFigure figure, string TargetName)
        {
            return GetTargetMember(TargetName).LinkKey(figure);
        }

        #endregion
    } 

    public static class LinkerExtension
    {
        #region Methods

        public static Link GetOriginLink(this IFigures figures, string OriginName)
        {
            return Linker.Map.Links[OriginName + "_" + figures.Instant.Name];
        }

        public static Link GetTargetLink(this IFigures figures, string TargetName)
        {
            return Linker.Map.Links[figures.Instant.Name + "_" + TargetName];
        }

        #endregion
    }
}
