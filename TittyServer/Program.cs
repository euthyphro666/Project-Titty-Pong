using System;

namespace TittyServer
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
        static void Main(string[] args)
        {
            var startArg = "";
            if (args.Length != 0)
                startArg = args[0];
            
            using (var game = new ServerGameManager(startArg))
                game.Run();
        }
    }
#endif
}
