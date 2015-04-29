#region Using Statements
using System;
using GameEngine;
using SecretOfThePast.Screens;

#endregion

namespace SecretOfThePast
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
            using (var game = new ScreenManager()){
                game.Screens.Add(new MainScreen(game));
                game.Screens.Add(new MenuScreen(game));
                game.Run();
            }
        }
    }
#endif
}
