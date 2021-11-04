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
    public class Rule<TRule> : LaborCase where TRule : class
    {
        public Aspect<TAspect> Aspect<TAspect>() where TAspect : class
        {
            if(!TryGet(typeof(TAspect).FullName, out Aspect aspect))
            {
                aspect = new Aspect<TAspect>();
                Add(aspect);
            }
            return aspect as Aspect<TAspect>;
        }
    }
}
