/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.Figure.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant
{
    using System;
    using Collections.Generic;
    using Linq;
    using Reflection;
    using Series;

    public class Sleeve<T> : Sleeve
    {
        public Sleeve() : base(typeof(T))
        {
        }

        public Sleeve(string sleeveName) : base(typeof(T), sleeveName)
        {
        }
    }

    public class Sleeve : IInstant
    {
        #region Fields

        private IDeck<RubricBuilder> rubricBuilders;
        private InstantBuilder instantBuilder;
        private Type compiledType;

        #endregion

        #region Constructors

        public Sleeve(Type figureModelType) : this(figureModelType, null)
        {
        }

        public Sleeve(Type figureModelType, string figureTypeName)
        {
            BaseType = figureModelType;

            Name = figureTypeName == null
                 ? figureModelType.Name
                 : figureTypeName;

            instantBuilder = new InstantBuilder();
            rubricBuilders = instantBuilder.CreateBuilders(figureModelType);

            Rubrics = new MemberRubrics(rubricBuilders.Select(m => m.Member).ToArray());
            Rubrics.KeyRubrics = new MemberRubrics();
        }

        #endregion

        #region Properties

        public Type BaseType { get; set; }

        public string Name { get; set; }

       public IRubrics Rubrics { get; set; } 

        public int Size { get; set; }

        public Type Type { get; set; }

        #endregion

        #region Methods

        public ISleeve Combine(object obj = null)
        {
            var s = combine();
            if (obj == null)
                obj = BaseType.New();
            s.Devisor = obj;
            return s;
        }

        public object New()
        {
            return Combine();
        }

        public ISleeve combine()
        {
            if(Type == null)
            {
                try
                {
                    combineSleeve(new SleeveCompiler(this, rubricBuilders));
                    Rubrics.Update();
                }
                catch(Exception ex)
                {
                    
                }
            }
            return newSleeve();
        }

        private void combineSleeve(SleeveCompiler compiler)
        {
            var fcdt = compiler;
            
            compiledType = fcdt.CompileSleeveType(Name);

            Rubrics.KeyRubrics.Add(fcdt.Identities.Values.Select(v => Rubrics[v.Name]).ToArray());
            
            Type = compiledType.New().GetType();

            Rubrics.Select(
                (f, y) => new object[]
                {
                    f.FieldId = y,
                    f.RubricId = y
                }).ToArray();
        }

        private ISleeve newSleeve()
        {
            ISleeve s;
            if (Type == null)
            {
                s = Combine();
            }
            s = (ISleeve)(Type.New());
            s.Rubrics = Rubrics;
            return s;
        }

        #endregion
    }
}
