using System.Collections.Generic;

namespace System.Laboring
{
    public interface ILaborer
    {
        /// <summary>
        /// Gets or sets the Input.
        /// </summary>
        object Input { get; set; }

        /// <summary>
        /// Gets or sets the Output.
        /// </summary>
        object Output { get; set; }

        /// <summary>
        /// Gets or sets the EvokersIn.
        /// </summary>
        NoteEvokers Evokers { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the Operation.
        /// </summary>
        IDeputy Operation { get; set; }

        /// <summary>
        /// The AddEvoker.
        /// </summary>
        /// <param name="Recipient">The Recipient<see cref="Laborer.Labor"/>.</param>
        void ResultTo(Labor Recipient);

        /// <summary>
        /// The AddEvoker.
        /// </summary>
        /// <param name="Recipient">The Recipient<see cref="Laborer.Labor"/>.</param>
        /// <param name="RelationLabors">The RelationLabors<see cref="List{Labor}"/>.</param>
        void ResultTo(Labor Recipient, params Labor[] RelationLabors);

        /// <summary>
        /// The AddEvoker.
        /// </summary>
        /// <param name="RecipientName">The RecipientName<see cref="string"/>.</param>
        void ResultTo(string RecipientName);

        /// <summary>
        /// The AddEvoker.
        /// </summary>
        /// <param name="RecipientName">The RecipientName<see cref="string"/>.</param>
        /// <param name="RelationNames">The RelationNames<see cref="List{string}"/>.</param>
        void ResultTo(string RecipientName, params string[] RelationNames);
    }
}