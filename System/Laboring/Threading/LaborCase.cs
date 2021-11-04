/*************************************************
   Copyright (c) 2021 Undersoft

   LaborCase.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

using System.Uniques;

namespace System.Laboring
{
    using System.Collections.Generic;
    using System.Linq;

    public class LaborCase : Case
    {
        public LaborCase(IEnumerable<IDeputy> methods, Case @case = null) 
            : base((@case == null) ? $"Case_{Unique.New}" : @case.Name, 
                  (@case == null) ? new LaborNotes(): @case.Notes)
        {
            Add($"Aspect_{Unique.New}", methods);            
            Open();
        }
        public LaborCase(Case @case) : base(@case.Name, @case.Notes)
        {
            Add(@case.AsValues());
        }
        public LaborCase() : base($"Case_{Unique.New}", new LaborNotes())
        {
        }

        public Aspect Aspect(IDeputy method, Aspect aspect)
        {
            if (aspect != null)
            {
                if(!TryGet(aspect.Name, out Aspect _aspect))
                {
                    Add(_aspect);
                    _aspect.Include(method);                   
                }
                return aspect;
            }
            return null;
        }
        public Aspect Aspect(IDeputy method, string name)
        {
            if(!TryGet(name, out Aspect aspect))
            {
                aspect = new Aspect(name);
                Add(aspect);
                aspect.Include(method);              
            }
            return aspect;
        }
        public Aspect Aspect(string name)
        {
            if(!TryGet(name, out Aspect aspect))
            {
                aspect = new Aspect(name);
                Add(aspect);
            }
            return aspect;
        }

        public void Open()
        {
            Setup();
        }
        public void Setup()
        {
            foreach (Aspect aspect in AsValues())
            {
                if (aspect.Laborator == null)
                {
                    aspect.Laborator = new Laborator(aspect);
                }
                if (!aspect.Laborator.Ready)
                {
                    aspect.Allocate();
                }
            }
        }

        public void Run(string laborName, params object[] input)
        {
            Labor[] labors = AsValues()
                .Where(m => m.ContainsKey(laborName))
                    .SelectMany(w => w.AsValues()).ToArray();

            foreach (Labor labor in labors)
                labor.Execute(input);
        }
        public void Run(IDictionary<string, object[]> laborsAndInputs)
        {
            foreach (KeyValuePair<string, object[]> worker in laborsAndInputs)
            {
                object input = worker.Value;
                string workerName = worker.Key;
                Labor[] workerLabors = AsValues()
                    .Where(m => m.ContainsKey(workerName))
                    .SelectMany(w => w.AsValues()).ToArray();

                foreach (Labor objc in workerLabors)
                    objc.Execute(input);
            }
        }
    }

    /// <summary>
    /// Defines the <see cref="InvalidLaborException" />.
    /// </summary>
    public class InvalidLaborException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLaborException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public InvalidLaborException(string message)
            : base(message)
        {
        }

        #endregion
    }
}
