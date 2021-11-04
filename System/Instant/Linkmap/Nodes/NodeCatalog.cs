/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.LinkNodes.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (29.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Linking
{
    using System.Series;

    [Serializable]
    public class NodeCatalog : CatalogBase<IFigure>
    {
        #region Constructors

        public NodeCatalog() : base()
        {
            Links = Linker.Map.Links;
        }
        public NodeCatalog(int capacity) : base(capacity)
        {
            Links = Linker.Map.Links;
        }
        public NodeCatalog(Links links, int capacity) : base(capacity)
        {
            Links = links;
        }

        #endregion

        #region Properties

        public Links Links { get; set; }

        public Links Context { get; set; }

        #endregion

        #region Methods

        public override ICard<IFigure> EmptyCard()
        {
            return new NodeCard();
        }

        public override ICard<IFigure>[] EmptyCardTable(int size)
        {
            return new BranchDeck[size];
        }

        public override ICard<IFigure>[] EmptyDeck(int size)
        {
            return new BranchDeck[size];
        }

        public override ICard<IFigure> NewCard(IFigure card)
        {
            return new NodeCard(card);
        }

        public override ICard<IFigure> NewCard(ICard<IFigure> card)
        {
            return card;
        }

        public override ICard<IFigure> NewCard(object key, IFigure value)
        {
            return new NodeCard(key, value);
        }

        public override ICard<IFigure> NewCard(ulong key, IFigure value)
        {
            return new NodeCard(key, value);
        }

        #endregion
    }
}
