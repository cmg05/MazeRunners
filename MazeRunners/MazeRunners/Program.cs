using System.Security.Cryptography.X509Certificates;

namespace MazeRunners
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Initial initial = new Initial();
            Application.Run(initial);

            if (initial.startGame) {
                Maze maze = new Maze();
                maze.StartGame();
                Application.Run(maze);
            }
        }
    }
}



