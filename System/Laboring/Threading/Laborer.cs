/*************************************************
   Copyright (c) 2021 Undersoft

   Laborer.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

namespace System.Laboring
{
    using Collections.Generic;
    using Series;
    using Uniques;

    /// <summary>
    /// Defines the <see cref="Laborer" />.
    /// </summary>
    public class Laborer : IUnique, ILaborer
    {
        #region Fields

        private readonly Board<object> input = new();
        private readonly Board<object> output = new();
        private Ussn SerialCode;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Laborer"/> class.
        /// </summary>
        private Laborer()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Laborer"/> class.
        /// </summary>
        /// <param name="Name">The Name<see cref="string"/>.</param>
        /// <param name="Method">The Method<see cref="IDeputy"/>.</param>
        public Laborer(string Name, IDeputy Method) : this()
        {
            Operation = Method;
            this.Name = Name;
            ulong serie = Unique.New;
            SerialCode = new Ussn((Operation.UniqueKey).UniqueKey(serie), serie);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Empty.
        /// </summary>
        public IUnique Empty => new Ussc();

        /// <summary>
        /// Gets or sets the EvokersIn.
        /// </summary>
        public NoteEvokers Evokers { get; set; } = new();

        /// <summary>
        /// Gets or sets the Input.
        /// </summary>
        public object Input
        {

            get
            {
                object entry;
                input.TryDequeue(out entry);
                return entry;

            }
            set
            {
                input.Enqueue(value);
            }
        }

        /// <summary>
        /// Gets or sets the Labor.
        /// </summary>
        public Labor Labor { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Output.
        /// </summary>
        public object Output
        {
            get
            {
                if (output.TryPick(0, out object _result))
                    return _result;
                return null;
            }
            set
            {
                output.Enqueue(value);
            }
        }

        /// <summary>
        /// Gets or sets the UniqueKey.
        /// </summary>
        public ulong UniqueKey { get => SerialCode.UniqueKey; set => SerialCode.UniqueKey = value; }

        /// <summary>
        /// Gets or sets the UniqueSeed.
        /// </summary>
        public ulong UniqueSeed { get => SerialCode.UniqueSeed; set => SerialCode.UniqueSeed = value; }

        /// <summary>
        /// Gets or sets the Work.
        /// </summary>
        public IDeputy Operation { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The AddEvoker.
        /// </summary>
        /// <param name="Recipient">The Recipient<see cref="Labor"/>.</param>
        public void ResultTo(Labor recipient)
        {
            Evokers.Add(new NoteEvoker(Labor, recipient,  Labor));
        }

        /// <summary>
        /// The AddEvoker.
        /// </summary>
        /// <param name="Recipient">The Recipient<see cref="Labor"/>.</param>
        /// <param name="RelationLabors">The RelationLabors<see cref="List{Labor}"/>.</param>
        public void ResultTo(Labor Recipient, params Labor[] RelationLabors)
        {
            Evokers.Add(new NoteEvoker(Labor, Recipient, RelationLabors));
        }

        /// <summary>
        /// The AddEvoker.
        /// </summary>
        /// <param name="RecipientName">The RecipientName<see cref="string"/>.</param>
        public void ResultTo(string RecipientName)
        {
            Evokers.Add(new NoteEvoker(Labor, RecipientName, Name ));
        }

        /// <summary>
        /// The AddEvoker.
        /// </summary>
        /// <param name="RecipientName">The RecipientName<see cref="string"/>.</param>
        /// <param name="RelationNames">The RelationNames<see cref="List{string}"/>.</param>
        public void ResultTo(string RecipientName, params string[] RelationNames)
        {
            Evokers.Add(new NoteEvoker(Labor, RecipientName, RelationNames));
        }

        /// <summary>
        /// The CompareTo.
        /// </summary>
        /// <param name="other">The other<see cref="IUnique"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int CompareTo(IUnique other)
        {
            return SerialCode.CompareTo(other);
        }

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="other">The other<see cref="IUnique"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Equals(IUnique other)
        {
            return SerialCode.Equals(other);
        }

        /// <summary>
        /// The GetBytes.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] GetBytes()
        {
            return SerialCode.GetBytes();
        }

        /// <summary>
        /// The GetUniqueBytes.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] GetUniqueBytes()
        {
            return SerialCode.GetUniqueBytes();
        }

        #endregion
    }
}
