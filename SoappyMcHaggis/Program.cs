using SoappyMcHaggis;
using System;

namespace SoappMcHaggis
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (FabulousAdventure game = new FabulousAdventure())
            {
                game.Run();
            }
        }
    }
}

