/*************************************************
   Copyright (c) 2021 Undersoft

   LaborNotes.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

namespace System.Laboring
{
    using System.Linq;
    using System.Series;

    #region Enums

    public enum EvokerType
    {
        /// <summary>
        /// Defines the Always.
        /// </summary>
        Always,
        /// <summary>
        /// Defines the Single.
        /// </summary>
        Single,
        /// <summary>
        /// Defines the Schedule.
        /// </summary>
        Schedule,
        /// <summary>
        /// Defines the Nome.
        /// </summary>
        Nome
    }

    #endregion

    public class LaborNotes : Catalog<NoteBox>
    {
        #region Properties

        private Case Case { get; set; }

        #endregion

        #region Methods

        public void Send(Note parameters)
        {
            if (parameters.RecipientName != null && parameters.SenderName != null)
            {
                if (ContainsKey(parameters.RecipientName))
                {
                    NoteBox iobox = Get(parameters.RecipientName);
                    if (iobox != null)
                        iobox.Notify(parameters);
                }
                else if (parameters.Recipient != null)
                {
                    Labor objv = parameters.Recipient;
                    NoteBox iobox = new NoteBox(objv.Laborer.Name);
                    iobox.Labor = objv;
                    iobox.Notify(parameters);
                    SetOutbox(iobox);
                }
                else if (Case != null)
                {
                    var objvl = Case.AsValues()
                        .Where(m => m.ContainsKey(parameters.RecipientName))
                        .SelectMany(os => os.AsValues());

                    if (objvl.Any())
                    {
                        Labor objv = objvl.FirstOrDefault();
                        NoteBox iobox = new NoteBox(objv.Laborer.Name);
                        iobox.Labor = objv;
                        iobox.Notify(parameters);
                        SetOutbox(iobox);
                    }
                }
            }
        }

        public void Send(params Note[] parametersList)
        {
            foreach (Note parameters in parametersList)
            {
                Send(parameters);
            }
        }

        public void SetOutbox(NoteBox value)
        {
            if (value != null)
            {
                if (value.Labor != null)
                {
                    Labor objv = value.Labor;
                    Put(value.RecipientName, value);
                }
                else
                {
                    var objvl = Case.AsValues()
                        .Where(m => m.ContainsKey(value.RecipientName))
                        .SelectMany(os => os.AsValues());

                    if (objvl.Any())
                    {
                        Labor objv = objvl.FirstOrDefault();
                        value.Labor = objv;
                        Put(value.RecipientName, value);
                    }
                }
            }
        }

        public void CreateOutbox(string key, NoteBox noteBox)
        {
            if (noteBox != null)
            {
                if (noteBox.Labor != null)
                {
                    Labor objv = noteBox.Labor;
                    Put(noteBox.RecipientName, noteBox);
                }
                else
                {
                    var objvl = Case.AsValues()
                        .Where(m => m.ContainsKey(key))
                        .SelectMany(os => os.AsValues());

                    if (objvl.Any())
                    {
                        Labor objv = objvl.FirstOrDefault();
                        noteBox.Labor = objv;
                        Put(key, noteBox);
                    }
                }
            }
            else
            {
                var objvl = Case.AsValues()
                    .Where(m => m.ContainsKey(key))
                    .SelectMany(os => os.AsValues());

                if (objvl.Any())
                {
                    Labor objv = objvl.FirstOrDefault();
                    NoteBox iobox = new NoteBox(objv.Laborer.Name);
                    iobox.Labor = objv;
                    Put(key, iobox);
                }
            }
        }

        #endregion
    }
}
