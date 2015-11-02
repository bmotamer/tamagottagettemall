using System;

namespace Tamagottagettemall
{

    /// <summary>
    /// Where it all happens
    /// </summary>
    public class Program
    {

        /// <summary>
        /// Where it all begins
        /// </summary>
        /// <param name="args">Command line arguments</param>
        [STAThread]
        public static void Main(string[] args)
        {
            // Make a game instance of 32x16 blocks resolution and goes to the title scene
            using (ConsoleGame game = new ConsoleGame(32, 16, new SceneTitle()))
                // Starts the game loop
                game.Run();
        }
    }

}