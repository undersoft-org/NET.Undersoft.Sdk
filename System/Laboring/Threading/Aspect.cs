/*************************************************
   Copyright (c) 2021 Undersoft

   Aspect.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

namespace System.Laboring
{
    using Collections.Generic;
    using Series;
    using Linq;

    public class Aspect : Catalog<Labor>, ILaborator
    {
        public Case Case { get; set; }

        public Aspect(string Name)
        {
            this.Name = Name;
            LaborersCount = 1;
        }
        public Aspect(string Name, IEnumerable<Labor> LaborList) : this(Name)
        {
            foreach (Labor labor in LaborList)
            {
                labor.Case = Case;
                labor.Aspect = this;
                Put(labor);
            }
        }
        public Aspect(string Name, IEnumerable<IDeputy> MethodList) 
            : this(Name, MethodList.Select(m => new Labor(m))) { }

        public int LaborersCount { get; set; }

        public Laborator Laborator { get; set; }

        public string Name { get; set; }

        public override Labor Get(object key)
        {           
            TryGet(key, out Labor result);
            return result;
        }

        public Labor Include(Labor labor)
        {
            labor.Case = Case;
            labor.Aspect = this;
            Put(labor);
            return labor;
        }
        public Labor Include(IDeputy deputy)
        {            
            Labor labor = new Labor(deputy);
            labor.Case = Case;
            labor.Aspect = this;
            Put(labor);
            return labor;
        }
        
        public Aspect Include(IEnumerable<Labor> labors)
        {
            foreach(Labor labor in labors)
            {
                labor.Case = Case;
                labor.Aspect = this;
                Put(labor);
            }
            return this;
        }
        public Aspect Include(IEnumerable<IDeputy> methods)
        {
            foreach (IDeputy method in methods)
            {
                Labor labor = new Labor(method);
                labor.Case = Case;
                labor.Aspect = this;
                Put(labor);
            }
            return this;
        }

        public virtual Aspect Include<T>() where T : class
        {
            var deputy = new Deputy<T>();
            Include(Case.Methods.SureGet(deputy, k => deputy).Value);
            return this;
        }
        public virtual Aspect Include<T>(Type[] arguments) where T : class
        {
            var deputy = new Deputy<T>(arguments);
            Include(Case.Methods.SureGet(deputy, k => deputy).Value);
            return this;
        }
        public virtual Aspect Include<T>(params object[] consrtuctorParams) where T : class
        {
            var deputy = new Deputy<T>(consrtuctorParams);
            Include(Case.Methods.SureGet(deputy, k => deputy).Value);
            return this;
        }
        public virtual Aspect Include<T>(Type[] arguments, params object[] consrtuctorParams) where T : class
        {
            var deputy = new Deputy<T>(arguments, consrtuctorParams);
            Include(Case.Methods.SureGet(deputy, k => deputy).Value);
            return this;
        }

        public virtual Labor Perform<T>() where T : class
        {
            var deputy = new Deputy<T>();
            if(!TryGet(deputy.Name, out Labor labor))
               return Include(Case.Methods.SureGet(deputy, k => deputy).Value);
            return labor;
        }
        public virtual Labor Perform<T>(Type[] arguments) where T : class
        {
            var deputy = new Deputy<T>(arguments);
            if(!TryGet(deputy.Name, out Labor labor))
                return Include(Case.Methods.SureGet(deputy, k => deputy).Value);
            return labor;
        }
        public virtual Labor Perform<T>(params object[] consrtuctorParams) where T : class
        {
            var deputy = new Deputy<T>(consrtuctorParams);
            if(!TryGet(deputy.Name, out Labor labor))
                return Include(Case.Methods.SureGet(deputy, k => deputy).Value);
            return labor;
        }
        public virtual Labor Perform<T>(Type[] arguments, params object[] consrtuctorParams) where T : class
        {
            var deputy = new Deputy<T>(arguments, consrtuctorParams);
            if(!TryGet(deputy.Name, out Labor labor))
                return Include(Case.Methods.SureGet(deputy, k => deputy).Value);
            return labor;
        }

        public override Labor this[object key]
        {
            get => base[key];
            set
            {
                value.Case = Case;
                value.Aspect = this;
                base[key] = value;
            }
        }

        public void Close(bool SafeClose)
        {
            Laborator.Close(SafeClose);
        }

        public Aspect Allocate(int laborersCount = 1)
        {
            Laborator.Allocate(laborersCount);
            return this;
        }

        public void Run(Labor labor)
        {
            Laborator.Run(labor);
        }

        public void Reset(int laborersCount = 1)
        {
            Laborator.Reset(laborersCount);
        }
    }
}
