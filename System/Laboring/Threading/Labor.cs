/*************************************************
   Copyright (c) 2021 Undersoft

   Labor.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

using System.Collections.Generic;

namespace System.Laboring
{
    using System.Instant;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Uniques;

    public class Labor : Task<object>, IDeputy, ILaborer
    {
        public IUnique Empty => Ussn.Empty;

        public Labor(IDeputy operation) : base(() => operation.Execute())
        {
            Name = operation.Name;
            Laborer = new Laborer(operation.Name, operation);
            Laborer.Labor = this;
            Box = new NoteBox(Laborer.Name);
            Box.Labor = this;
            
            SerialCode = new Ussn(Name.UniqueKey(), Unique.New);
        }
        public Labor(Laborer laborer) : base(() => laborer.Operation.Execute())
        {
            Name = laborer.Name;
            Laborer = laborer;
            Laborer.Labor = this;
            Box = new NoteBox(Laborer.Name);
            Box.Labor = this;

            SerialCode = new Ussn(Name.UniqueKey(), Unique.New);
        }

        public string Name { get; set; }

        public string QualifiedName { get; set; }

        public Laborer Laborer { get; set; }

        public Aspect Aspect { get; set; }

        public Case Case { get; set; }

        public NoteBox Box { get; set; }

        public object[] ParameterValues
        {
            get => Laborer.Operation.ParameterValues;
            set => Laborer.Operation.ParameterValues = value;
        }
        public object this[int fieldId] { get => ParameterValues[fieldId]; set => ParameterValues[fieldId] = value; }
        public object this[string propertyName]
        {
            get
            {
                for (int i = 0; i < Parameters.Length; i++)
                    if (Parameters[i].Name == propertyName)
                        return ParameterValues[i];
                return null;
            }
            set
            {
                for (int i = 0; i < Parameters.Length; i++)
                    if (Parameters[i].Name == propertyName)
                        ParameterValues[i] = value;
            }
        }

        public MethodInfo Info
        {
            get => Laborer.Operation.Info;
            set => Laborer.Operation.Info = value;
        }

        public ParameterInfo[] Parameters
        {
            get => Laborer.Operation.Parameters;
            set => Laborer.Operation.Parameters = value;
        }
        public object[] ValueArray { get => ParameterValues; set => ParameterValues = value; }

        public Ussn SerialCode
        {
            get;
            set;
        }

        public ulong UniqueKey { get => SerialCode.UniqueKey; set => SerialCode.SetUniqueKey(value); }

        public ulong UniqueSeed { get => SerialCode.UniqueSeed; set => SerialCode.SetUniqueSeed(value); }

        public DeputyRoutine Routine => Operation.Routine;

        public void Run(params object[] input)
        {
            Laborer.Input = input;
            Aspect.Run(this);
        }

        public object Execute(params object[] parameters)
        {
            this.Run(parameters);
            return null;
        }

        public byte[] GetBytes()
        {
            return Laborer.Operation.GetBytes();
        }
        public byte[] GetUniqueBytes()
        {
            return SerialCode.GetUniqueBytes();
        }

        public bool Equals(IUnique other)
        {
            return SerialCode.Equals(other);
        }
        public int CompareTo(IUnique other)
        {
            return SerialCode.CompareTo(other);
        }

        public Task<object> ExecuteAsync(params object[] parameters)
        {
            return Task.Run(() => { return Execute(parameters); });
        }

        public Task<T> ExecuteAsync<T>(params object[] parameters)
        {
            return Task.Run(() => { Execute(parameters); return default(T); });
        }

        public object Input
        {
            get => Laborer.Input;
            set => Laborer.Input = value;
        }

        public object Output
        {
            get => Laborer.Output;
            set => Laborer.Output = value;
        }

        public NoteEvokers Evokers
        {
            get => Laborer.Evokers;
            set => Laborer.Evokers = value;
        }

        public string LaborerName
        {
            get => Laborer.Name;
            set => Laborer.Name = value;
        }

        public IDeputy Operation
        {
            get => Laborer.Operation;
            set => Laborer.Operation = value;
        }

        public void ResultTo(Labor Recipient)
        {
            ulong recipientKey = Recipient.Name.UniqueKey();

            var relationLabors = Aspect.AsValues()
                .Where(l => l.Laborer.Evokers
                        .ContainsKey(l.Name.UniqueKey(recipientKey)))
                .ToArray();

            var evokers = relationLabors.Select(l => l.Evokers.Get(l.Name.UniqueKey(recipientKey))).ToArray();
            foreach(var noteEvoker in evokers)
            {
                noteEvoker.RelatedLabors.Put(this);
                noteEvoker.RelatedLaborNames.Put(Name);
            }

            Laborer.ResultTo(Recipient, relationLabors.Concat(new[] { this }).ToArray());
        }
        public void ResultTo(Labor Recipient, params Labor[] RelationLabors)
        {
            Laborer.ResultTo(Recipient, RelationLabors);
        }
        public void ResultTo(string RecipientName)
        {
            var recipient = this.Case.AsValues()
                .Where(m => m.ContainsKey(RecipientName))
                .SelectMany(os => os.AsValues()).FirstOrDefault();

            ulong recipientKey = RecipientName.UniqueKey();

            var relationLabors = Aspect.AsValues()
                .Where(l => l.Laborer.Evokers
                    .ContainsKey(l.Name.UniqueKey(recipientKey))).ToArray();

            var evokers = relationLabors.Select(l => l.Evokers.Get(l.Name.UniqueKey(recipientKey))).ToArray();
            foreach(var noteEvoker in evokers)
            {
                noteEvoker.RelatedLabors.Put(this);
                noteEvoker.RelatedLaborNames.Put(Name);
            }

            Laborer.ResultTo(recipient, relationLabors.Concat(new[] { this }).ToArray());
        }
        public void ResultTo(string RecipientName, params string[] RelationNames)
        {
            Laborer.ResultTo(RecipientName, RelationNames);
        }

        public virtual Aspect Include<T>() where T : class
        {
            return Aspect.Include<T>();
        }
        public virtual Aspect Include<T>(Type[] arguments) where T : class
        {
            return Aspect.Include<T>(arguments);
        }
        public virtual Aspect Include<T>(params object[] consrtuctorParams) where T : class
        {
            return Aspect.Include<T>(consrtuctorParams);
        }
        public virtual Aspect Include<T>(Type[] arguments, params object[] consrtuctorParams) where T : class
        {
            return Aspect.Include<T>(arguments, consrtuctorParams);
        }

        public virtual Labor Perform<T>() where T : class
        {
            return Aspect.Perform<T>();
        }
        public virtual Labor Perform<T>(Type[] arguments) where T : class
        {
            return Aspect.Perform<T>(arguments);
        }
        public virtual Labor Perform<T>(params object[] consrtuctorParams) where T : class
        {
            return Aspect.Perform<T>(consrtuctorParams);
        }
        public virtual Labor Perform<T>(Type[] arguments, params object[] consrtuctorParams) where T : class
        {
            return Aspect.Perform<T>(arguments, consrtuctorParams);
        }

        public Aspect Allocate(int laborsCount = 1)
        {
            return Aspect.Allocate(laborsCount);
        }
    }
}
