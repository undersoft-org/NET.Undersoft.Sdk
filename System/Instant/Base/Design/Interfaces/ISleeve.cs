/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.IFigure.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 *************************************************/

using System.Series;

namespace System.Instant
{
    public interface ISleeve : IFigure
    {
        IRubrics Rubrics { get; set; }

        object Devisor { get; set; }
    }
}
