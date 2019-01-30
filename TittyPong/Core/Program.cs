using System;

namespace TittyPong.Core
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var master = new Master())
            {
                master.IsFixedTimeStep = true;
                master.Run();

            }
        }
    }
#endif
}
