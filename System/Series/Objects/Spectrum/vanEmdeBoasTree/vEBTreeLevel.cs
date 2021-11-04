/*************************************************
   Copyright (c) 2021 Undersoft

   System.Series.vEBTreeLevel.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: PhD Radoslaw Rudek, Dariusz Hanc
   @date: (30.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Series.Spectrum
{
    using System.Collections.Generic;

    public class vEBTreeLevel
    {
        #region Constructors

        public vEBTreeLevel()
        {
            Level = 0;
            BaseOffset = 0;
            Nodes = null;
        }

        #endregion

        #region Properties

        public int BaseOffset { get; set; }

        public byte Count { get; set; }

        public byte Level { get; set; }

        public IList<vEBTreeNode> Nodes { get; set; }

        #endregion
    }
}
