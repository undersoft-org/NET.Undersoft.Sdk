/*************************************************
   Copyright (c) 2021 Undersoft

   NoteEvoker.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

namespace System.Laboring
{
    using System.Collections.Generic;
    using System.Extract;
    using System.Linq;
    using System.Series;
    using System.Uniques;

    /// <summary>
    /// Defines the <see cref="NoteEvoker" />.
    /// </summary>
    public class NoteEvoker : Board<Labor>, IUnique
    {
        #region Fields

        public IDeck<Labor> RelatedLabors = new Board<Labor>();
        public IDeck<string> RelatedLaborNames = new Board<string>();
        private Ussn SerialCode;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteEvoker"/> class.
        /// </summary>
        /// <param name="sender">The sender<see cref="Labor"/>.</param>
        /// <param name="recipient">The recipient<see cref="Labor"/>.</param>
        /// <param name="relayLabors">The relayLabors<see cref="List{Labor}"/>.</param>
        public NoteEvoker(Labor sender, Labor recipient, params Labor[] relayLabors)
        {
            Sender = sender;
            SenderName = sender.Laborer.Name;
            Recipient = recipient;
            RecipientName = recipient.Laborer.Name;
            SerialCode = new Ussn(SenderName.UniqueKey(RecipientName.UniqueKey()), RecipientName.UniqueKey());
            RelatedLabors.Add(relayLabors);
            RelatedLaborNames.Add(RelatedLabors.Select(rn => rn.Laborer.Name));
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteEvoker"/> class.
        /// </summary>
        /// <param name="sender">The sender<see cref="Labor"/>.</param>
        /// <param name="recipient">The recipient<see cref="Labor"/>.</param>
        /// <param name="relayNames">The relayNames<see cref="List{string}"/>.</param>
        public NoteEvoker(Labor sender, Labor recipient, params string[] relayNames)
        {
            Sender = sender;
            SenderName = sender.Name;
            Recipient = recipient;
            RecipientName = recipient.Name;
            SerialCode = new Ussn(SenderName.UniqueKey(RecipientName.UniqueKey()), RecipientName.UniqueKey());
            RelatedLaborNames.Add(relayNames);
            var namekeys = relayNames.ForEach(s => s.UniqueKey());
            RelatedLabors.Add(Sender.Case.AsValues()
                .Where(m => m.AsIdentifiers()
                .Any(k => namekeys.Contains(k.UniqueKey)))
                .SelectMany(os => os.AsValues()).ToList());
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteEvoker"/> class.
        /// </summary>
        /// <param name="sender">The sender<see cref="Labor"/>.</param>
        /// <param name="recipientName">The recipientName<see cref="string"/>.</param>
        /// <param name="relayLabors">The relayLabors<see cref="IList{Labor}"/>.</param>
        public NoteEvoker(Labor sender, string recipientName, params Labor[] relayLabors)
        {
            Sender = sender;
            SenderName = sender.Name;
            RecipientName = recipientName;
            SerialCode = new Ussn(SenderName.UniqueKey(RecipientName.UniqueKey()), RecipientName.UniqueKey());
            var rcpts = Sender.Case.AsValues()
                                        .Where(m => m.ContainsKey(recipientName))
                                            .SelectMany(os => os.AsValues()).ToArray();
            Recipient = rcpts.FirstOrDefault();
            RelatedLabors.Add(relayLabors);
            RelatedLaborNames.Add(RelatedLabors.Select(rn => rn.Laborer.Name));
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteEvoker"/> class.
        /// </summary>
        /// <param name="sender">The sender<see cref="Labor"/>.</param>
        /// <param name="recipientName">The recipientName<see cref="string"/>.</param>
        /// <param name="relayNames">The relayNames<see cref="IList{string}"/>.</param>
        public NoteEvoker(Labor sender, string recipientName,  params string[] relayNames)
        {
            Sender = sender;
            SenderName = sender.Laborer.Name;
            var rcpts = Sender.Case.AsValues()
                .Where(m => m.ContainsKey(recipientName))
                .SelectMany(os => os.AsValues()).ToArray();
            Recipient = rcpts.FirstOrDefault();
            RecipientName = recipientName;
            SerialCode = new Ussn(SenderName.UniqueKey(RecipientName.UniqueKey()), RecipientName.UniqueKey());
            RelatedLaborNames.Add(relayNames);
            var namekeys = relayNames.ForEach(s => s.UniqueKey());
            RelatedLabors.Add(Sender.Case.AsValues()
                .Where(m => m.AsIdentifiers()
                    .Any(k => namekeys.Contains(k.UniqueKey)))
                .SelectMany(os => os.AsValues()).ToList());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Empty.
        /// </summary>
        public IUnique Empty => new Usid();

        /// <summary>
        /// Gets or sets the EvokerName.
        /// </summary>
        public string EvokerName { get; set; }

        /// <summary>
        /// Gets or sets the EvokerType.
        /// </summary>
        public EvokerType EvokerType { get; set; }

        /// <summary>
        /// Gets or sets the Recipient.
        /// </summary>
        public Labor Recipient { get; set; }

        /// <summary>
        /// Gets or sets the RecipientName.
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// Gets or sets the Sender.
        /// </summary>
        public Labor Sender { get; set; }

        /// <summary>
        /// Gets or sets the SenderName.
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// Gets or sets the UniqueKey.
        /// </summary>
        public new ulong UniqueKey { get => SerialCode.UniqueKey; set => SerialCode.SetUniqueKey(value); }

        /// <summary>
        /// Gets or sets the UniqueSeed.
        /// </summary>
        public ulong UniqueSeed { get => SerialCode.UniqueSeed; set => SerialCode.SetUniqueSeed(value); }

        #endregion

        #region Methods

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
            return ($"{SenderName}.{RecipientName}").GetBytes();
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
