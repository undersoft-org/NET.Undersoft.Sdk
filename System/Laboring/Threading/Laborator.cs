/*************************************************
   Copyright (c) 2021 Undersoft

   Laborator.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

namespace System.Laboring
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Series;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="Laborator" />.
    /// </summary>
    public class Laborator : ILaborator
    {
        #region Fields

        private static readonly int WAIT_WRITE_TIMEOUT = 5000;

        private readonly ManualResetEventSlim postAccess = new (true, 256);
        private readonly SemaphoreSlim postPass = new (1);
        private readonly object holder = new();

        private void acquirePostAccess()
        {
            do
            {
                if (!postAccess.Wait(WAIT_WRITE_TIMEOUT))
                    continue; //throw new TimeoutException("Wait write Timeout");
                postAccess.Reset();
            }
            while (!postPass.Wait(0));
        }
        private void releasePostAccess()
        {
            postPass.Release();
            postAccess.Set();
        }

        /// <summary>
        /// Defines the Notes.
        /// </summary>
        public LaborNotes Notes;
        /// <summary>
        /// Defines the Ready.
        /// </summary>
        public bool Ready;
        /// <summary>
        /// Defines the Scope.
        /// </summary>
        public Case Case;
        /// <summary>
        /// Defines the Aspect.
        /// </summary>
        public Aspect Aspect;
        /// <summary>
        /// Defines the laborers.
        /// </summary>
        private Thread[] laborers;
        /// <summary>
        /// Defines the LaborersCount.
        /// </summary>
        private int LaborersCount => Aspect.LaborersCount;
        /// <summary>
        /// Defines the LaborersQueue.
        /// </summary>
        private Board<Laborer> Elaborations = new();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Laborator"/> class.
        /// </summary>
        /// <param name="mission">The mission<see cref="Aspect"/>.</param>
        public Laborator(Aspect aspect)
        {
            Aspect = aspect;
            Case = Aspect.Case;
            Notes = Case.Notes;
            Ready = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The Close.
        /// </summary>
        /// <param name="SafeClose">The SafeClose<see cref="bool"/>.</param>
        public void Close(bool SafeClose)
        {
            // Enqueue one null item per worker to make each exit.
            foreach (Thread laborer in laborers)
            {
                Run(null);

                if (SafeClose && laborer.ThreadState == ThreadState.Running)
                    laborer.Join();
            }
            Ready = false;
        }

        /// <summary>
        /// The CreateLaborers.
        /// </summary>
        /// <param name="antcount">The antcount<see cref="int"/>.</param>
        public Aspect Allocate(int laborersCount = 0)
        {
            if (laborersCount > 0)
                Aspect.LaborersCount = laborersCount;

            laborers = new Thread[LaborersCount];
            // Create and start a separate thread for each Ant
            for (int i = 0; i < LaborersCount; i++)
            {
                laborers[i] = new Thread(Activate);
                laborers[i].Start();
            }

            Ready = true;
            return Aspect;
        }

        /// <summary>
        /// The Elaborate.
        /// </summary>
        /// <param name="worker">The worker<see cref="Laborer"/>.</param>
        public void Run(Labor work)
        {
            lock (holder)
            {
                if (work != null)
                {
                    Laborer Worker = CloneLaborer(work.Laborer);
                    Elaborations.Enqueue(Worker);
                    Monitor.Pulse(holder);

                }
                else
                {
                    Elaborations.Enqueue(DateTime.Now.Ticks, null);
                    Monitor.Pulse(holder);
                }
            }
        }

        /// <summary>
        /// The Reset.
        /// </summary>
        /// <param name="antcount">The antcount<see cref="int"/>.</param>
        public void Reset(int laborersCount = 0)
        {
            Close(true);
            Allocate(laborersCount);
        }

        /// <summary>
        /// The ActivateLaborer.
        /// </summary>
        private void Activate()
        {
            while (true)
            {
                Laborer laborer = null;
                object input = null;
                lock (holder)
                {
                    do
                    {
                        while (Elaborations.Count == 0)
                        {
                            Monitor.Wait(holder);
                        }
                    } while (!Elaborations.TryDequeue(out laborer));

                    if (laborer != null) input = laborer.Input;
                }

                if (laborer == null) return;

                object output = null;
                if (input != null)
                {
                    if (input is IList)
                        output = laborer.Operation.Execute((object[]) input);
                    else
                        output = laborer.Operation.Execute(input);
                }
                else
                {
                    output = laborer.Operation.Execute();
                }
                
                acquirePostAccess();
                
                Outpost(laborer, output);

                releasePostAccess();
            }
        }

        /// <summary>
        /// The CloneLaborer.
        /// </summary>
        /// <param name="laborer">The laborer<see cref="Laborer"/>.</param>
        /// <returns>The <see cref="Laborer"/>.</returns>
        private Laborer CloneLaborer(Laborer laborer)
        {
            Laborer _laborer = new Laborer(laborer.Name, laborer.Operation);
            _laborer.Input = laborer.Input;
            _laborer.Evokers = laborer.Evokers;
            _laborer.Labor = laborer.Labor;
            return _laborer;
        }

        /// <summary>
        /// The Outpost.
        /// </summary>
        /// <param name="worker">The worker<see cref="Laborer"/>.</param>
        /// <param name="output">The output<see cref="object"/>.</param>
        private void Outpost(Laborer worker, object output)
        {
            if (output != null)
            {
                worker.Output = output;

                if (worker.Evokers != null && worker.Evokers.Count > 0)
                {
                    int l = worker.Evokers.Count;
                    if (l > 0)
                    {
                        var notes = new Note[l];
                        for (int i = 0; i < worker.Evokers.Count; i++)
                        {
                            Note note = new Note(worker.Labor, worker.Evokers[i].Recipient, worker.Evokers[i], null, output);
                            note.SenderBox = worker.Labor.Box;
                            notes[i] = note;
                        }

                        Notes.Send(notes);
                    }
                }

            }
        }

        #endregion
    }
}
