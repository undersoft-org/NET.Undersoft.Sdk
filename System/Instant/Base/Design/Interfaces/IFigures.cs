/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.IFigures.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant
{
    using System.Instant.Linking;
    using System.Instant.Treatments;
using System.Linq;
    using System.Series;

    public interface IFigures : IDeck<IFigure>, IFigure, ISerialFormatter
    {
        IInstant Instant { get; set; }

        bool Prime { get; set; }

        new IFigure this[int index] { get; set; }

        object this[int index, string propertyName] { get; set; }

        object this[int index, int fieldId] { get; set; }

        IRubrics Rubrics { get; set; }

        IRubrics KeyRubrics { get; set; }

        IFigure NewFigure();

        ISleeve NewSleeve();

        Type FigureType { get; set; }

        int FigureSize { get; set; }

        Type Type { get; set; }

        IQueryable<IFigure> View { get; set; }

        IFigure Summary { get; set; }

        FigureFilter Filter { get; set; }

        FigureSort Sort { get; set; }

        Func<IFigure, bool> QueryFormula { get; set; }

        Treatment Treatment { get; set; }

        Linker Linker { get; set; }

        IDeck<IComputation> Computations { get; set; }
    }
}
