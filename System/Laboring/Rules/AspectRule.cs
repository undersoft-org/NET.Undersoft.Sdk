/*************************************************
   Copyright (c) 2021 Undersoft

   LaborCase.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

using System.Uniques;

namespace System.Laboring.Rules
{
    public class Aspect<TAspect> : Aspect where TAspect : class
    {
        public Aspect() : base(typeof(TAspect).FullName) { }

        public override Labor Perform<TEvent>() where TEvent : class
        {
            return base.Perform<TEvent>();
        }
        public override Labor Perform<TEvent>(Type[] arguments) where TEvent : class
        {
            return base.Perform<TEvent>(arguments);
        }
        public override Labor Perform<TEvent>(params object[] consrtuctorParams) where TEvent : class
        {
            return base.Perform<TEvent>(consrtuctorParams);
        }
        public override Labor Perform<TEvent>(Type[] arguments, params object[] consrtuctorParams) where TEvent : class
        {
            return base.Perform<TEvent>(arguments, consrtuctorParams);
        }
    }
}
