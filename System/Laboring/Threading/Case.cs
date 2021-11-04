/*************************************************
   Copyright (c) 2021 Undersoft

   Case.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

namespace System.Laboring
{
    using System.Collections.Generic;
    using System.Instant;
    using System.Series;


    public class Case : Catalog<Aspect>
    {
        public Case(string name = null, LaborNotes notes = null)
        {
            Name = (name != null) ? name : "LaborCase";
            Notes = (Notes != null) ? notes : new LaborNotes();
            Methods = new LaborMethods();
        }

        public string Name { get; set; }

        public LaborMethods Methods { get; set; }

        public LaborNotes Notes { get; set; }

        public Aspect Get(string key)
        {
            Aspect result = null;
            TryGet(key, out result);
            return result;
        }
        public override void Add(Aspect aspect)
        {
            aspect.Case = this;
            aspect.Laborator = new Laborator(aspect);
            Put(aspect.Name, aspect);
        }
        public override void Add(IEnumerable<Aspect> aspects)
        {
            foreach (var aspect in aspects)
            {
                aspect.Case = this;
                aspect.Laborator = new Laborator(aspect);
                Put(aspect.Name, aspect);
            }          
        }
        public override bool Add(object key, Aspect value)
        {
            value.Case = this;
            value.Laborator = new Laborator(value);
            Put(key, value);
            return true;
        }
        public void Add(object key, IEnumerable<Labor> value)
        {
            Aspect msn = new Aspect(key.ToString(), value);
            msn.Case = this;
            msn.Laborator = new Laborator(msn);
            Put(key, msn);
        }
        public Aspect Add(object key, IEnumerable<IDeputy> value)
        {
            Aspect msn = new Aspect(key.ToString(), value);
            msn.Case = this;
            msn.Laborator = new Laborator(msn);
            Put(key, msn);
            return msn;
        }
        public void Add(object key, IDeputy value)
        {
            List<IDeputy> cml = new List<IDeputy>() { value };
            Aspect msn = new Aspect(key.ToString(), cml);
            msn.Case = this;
            msn.Laborator = new Laborator(msn);
            Put(key, msn);
        }

        public override Aspect this[object key]
        {
            get
            {                
                TryGet(key, out Aspect result);
                return result;
            }
            set
            {                
                value.Case = this;
                value.Laborator = new Laborator(value);
                Put(key, value);
            }
        }
    }
}
