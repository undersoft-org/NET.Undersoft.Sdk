/*************************************************
   Copyright (c) 2021 Undersoft

   NoteEvokers.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

namespace System.Laboring
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Series;


    public class NoteEvokers : Catalog<NoteEvoker>
    {
        public bool Contains(IEnumerable<Labor> objectives)
        {
            return this.AsValues().Any(t => t.RelatedLabors.Any(ro => objectives.All(o => ReferenceEquals(ro, o))));
        }
        public bool Contains(IEnumerable<string> relayNames)
        {
            return this.AsValues().Any(t => t.RelatedLaborNames.SequenceEqual(relayNames));
        }

        public NoteEvoker this[string relatedLaborName]
        {
            get
            {
                return this.AsValues().FirstOrDefault(c => c.RelatedLaborNames.Contains(relatedLaborName));
            }
        }
        public NoteEvoker this[Labor relatedLabor]
        {
            get
            {
                return this.AsValues().FirstOrDefault(c => c.RelatedLabors.Contains(relatedLabor));
            }
        }
    }
}
