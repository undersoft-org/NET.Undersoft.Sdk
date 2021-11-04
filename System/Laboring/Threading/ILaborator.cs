namespace System.Laboring
{
    public interface ILaborator
    {
        /// <summary>
        /// The Close.
        /// </summary>
        /// <param name="SafeClose">The SafeClose<see cref="bool"/>.</param>
        void Close(bool SafeClose);

        /// <summary>
        /// The CreateLaborers.
        /// </summary>
        /// <param name="antcount">The antcount<see cref="int"/>.</param>
        Aspect Allocate(int antcount = 1);

        /// <summary>
        /// The Elaborate.
        /// </summary>
        /// <param name="worker">The worker<see cref="Laborer"/>.</param>
        void Run(Labor labor);

        /// <summary>
        /// The Reset.
        /// </summary>
        /// <param name="antcount">The antcount<see cref="int"/>.</param>
        void Reset(int antcount = 1);
    }
}