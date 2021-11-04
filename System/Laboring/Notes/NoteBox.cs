/*************************************************
   Copyright (c) 2021 Undersoft

   NoteBox.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

namespace System.Laboring
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Series;

    /// <summary>
    /// Defines the <see cref="NoteBox" />.
    /// </summary>
    public class NoteBox : Board<NoteTopic>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteBox"/> class.
        /// </summary>
        /// <param name="Recipient">The Recipient<see cref="string"/>.</param>
        public NoteBox(string Recipient)
        {
            RecipientName = Recipient;
            Evokers = new NoteEvokers();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Evokers.
        /// </summary>
        public NoteEvokers Evokers { get; set; }

        /// <summary>
        /// Gets or sets the Labor.
        /// </summary>
        public Labor Labor { get; set; }

        /// <summary>
        /// Gets or sets the RecipientName.
        /// </summary>
        public string RecipientName { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The Notify.
        /// </summary>
        /// <param name="value">The value<see cref="IList{Note}"/>.</param>
        public void Notify(params Note[] notes)
        {
            if (notes != null && notes.Any())
            {
                foreach (Note note in notes)
                {
                    NoteTopic queue = null;
                    if (note.SenderName != null)
                    {
                        if (!ContainsKey(note.SenderName))
                        {
                            queue = new NoteTopic(note.SenderName, this);
                            if (Add(note.SenderName, queue))
                            {
                                if (note.EvokerOut != null)
                                    Evokers.Add(note.EvokerOut);
                                queue.Notify(note);
                            }
                        }
                        else if (TryGet(note.SenderName, out queue))
                        {
                            if (notes != null && notes.Length > 0)
                            {
                                if (note.EvokerOut != null)
                                    Evokers.Add(note.EvokerOut);
                                queue.Notify(note);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The Notify.
        /// </summary>
        /// <param name="note">The note<see cref="Note"/>.</param>
        public void Notify(Note note)
        {
            if (note.SenderName != null)
            {
                NoteTopic queue = null;
                if (!ContainsKey(note.SenderName))
                {
                    queue = new NoteTopic(note.SenderName, this);
                    if (Add(note.SenderName, queue))
                    {
                        if (note.EvokerOut != null)
                            Evokers.Add(note.EvokerOut);
                        queue.Notify(note);
                    }
                }
                else if (TryGet(note.SenderName, out queue))
                {
                    if (note.EvokerOut != null)
                        Evokers.Add(note.EvokerOut);
                    queue.Notify(note);
                }
            }
        }

        /// <summary>
        /// The Notify.
        /// </summary>
        /// <param name="key">The key<see cref="string"/>.</param>
        /// <param name="value">The value<see cref="List{Note}"/>.</param>
        public void Notify(string key, params Note[] notes)
        {
            NoteTopic queue = null;
            if (!ContainsKey(key))
            {
                queue = new NoteTopic(key, this);
                if (Add(key, queue) && notes != null && notes.Length > 0)
                {
                    foreach (Note note in notes)
                    {
                        if (note.EvokerOut != null)
                            Evokers.Add(note.EvokerOut);
                        note.SenderName = key;
                        queue.Notify(note);
                    }
                }
            }
            else if (TryGet(key, out queue))
            {
                if (notes != null && notes.Length > 0)
                {
                    foreach (Note note in notes)
                    {
                        if (note.EvokerOut != null)
                            Evokers.Add(note.EvokerOut);
                        note.SenderName = key;
                        queue.Notify(note);
                    }
                }
            }
        }

        /// <summary>
        /// The Notify.
        /// </summary>
        /// <param name="key">The key<see cref="string"/>.</param>
        /// <param name="value">The value<see cref="Note"/>.</param>
        public void Notify(string key, Note value)
        {
            value.SenderName = key;
            NoteTopic queue = null;
            if (!ContainsKey(key))
            {
                queue = new NoteTopic(key, this);
                if (Add(key, queue))
                {
                    if (value.EvokerOut != null)
                        Evokers.Add(value.EvokerOut);
                    queue.Notify(value);
                }
            }
            else if (TryGet(key, out queue))
            {
                if (value.EvokerOut != null)
                    Evokers.Add(value.EvokerOut);
                queue.Notify(value);
            }
        }

        /// <summary>
        /// The Notify.
        /// </summary>
        /// <param name="key">The key<see cref="string"/>.</param>
        /// <param name="ioqueues">The ioqueues<see cref="object"/>.</param>
        public void Notify(string key, object ioqueues)
        {
            NoteTopic queue = null;
            if (!ContainsKey(key))
            {
                queue = new NoteTopic(key, this);
                if (Add(key, queue) && ioqueues != null)
                {
                    queue.Notify(ioqueues);
                }
            }
            else if (TryGet(key, out queue))
            {
                if (ioqueues != null)
                {
                    queue.Notify(ioqueues);
                }
            }
        }

        /// <summary>
        /// The GetNote.
        /// </summary>
        /// <param name="key">The key<see cref="string"/>.</param>
        /// <returns>The <see cref="Note"/>.</returns>
        public Note TakeNote(string key)
        {
            NoteTopic _ioqueue = null;
            if (TryGet(key, out _ioqueue))
                return _ioqueue.Dequeue();
            return null;
        }

        /// <summary>
        /// The TakeOut.
        /// </summary>
        /// <param name="keys">The keys<see cref="List{string}"/>.</param>
        /// <returns>The <see cref="List{Note}"/>.</returns>
        public IList<Note> TakeNotes(IList<string> keys)
        {
            return AsCards().Where(q => keys.Contains(q.Value.SenderName)).Select(v => v.Value.Notes).ToList();
        }

        /// <summary>
        /// The GetParams.
        /// </summary>
        /// <param name="key">The key<see cref="string"/>.</param>
        /// <returns>The <see cref="object[]"/>.</returns>
        public object[] GetParams(string key)
        {
            NoteTopic _ioqueue = null;
            Note temp = null;
            if (TryGet(key, out _ioqueue))
                if (_ioqueue.TryDequeue(out temp))
                    return temp.Parameters;
            return null;
        }

        /// <summary>
        /// The MeetsRequirements.
        /// </summary>
        /// <param name="keys">The keys<see cref="List{string}"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool MeetsRequirements(IList<string> keys)
        {
            return this.AsCards().Where(q => keys.Contains(q.Value.SenderName)).All(v => v.Value.Count > 0);
        }

        /// <summary>
        /// The QualifyToEvoke.
        /// </summary>
        public void QualifyToEvoke()
        {
            List<NoteEvoker> toEvoke = new List<NoteEvoker>();
            foreach (NoteEvoker relay in Evokers.AsValues())
            {
                if (relay.RelatedLaborNames.All(r => ContainsKey(r)))
                    if (relay.RelatedLaborNames.All(r => this[r].AsValues().Any()))
                    {
                        toEvoke.Add(relay);
                    }
            }

            if (toEvoke.Any())
            {
                foreach (NoteEvoker evoke in toEvoke)
                {
                    if (MeetsRequirements(evoke.RelatedLaborNames))
                    {
                        IList<Note> notes = TakeNotes(evoke.RelatedLaborNames);

                        if (notes.All(a => a != null))
                        {
                            object[] parameters = new object[0];
                            object begin = Labor.Laborer.Input;
                            if (begin != null)
                                parameters = parameters.Concat((object[])begin).ToArray();
                            foreach (Note note in notes)
                            {
                                if (note.Parameters.GetType().IsArray)
                                    parameters = parameters.Concat(note.Parameters.SelectMany(a => (object[])a).ToArray()).ToArray();
                                else
                                    parameters = parameters.Concat(note.Parameters).ToArray();
                            }

                            Labor.Execute(parameters);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
