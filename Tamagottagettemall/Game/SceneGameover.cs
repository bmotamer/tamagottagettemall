namespace Tamagottagettemall
{

    /// <summary>
    /// GameOver scene
    /// </summary>
    public class SceneGameover : ISceneBase
    {

        /// <summary>
        /// Starts the scene elements
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Start(Game game)
        {
        }

        /// <summary>
        /// Handles the logical part of the scene
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Update(Game game)
        {
            // If any key was pressed
            if (game.KeyboardManager.AnyTriggered != null)
                // Goes back to the title scene
                game.SceneManager.NextScene = new SceneTitle();
        }

        /// <summary>
        /// Handles the drawing part of the scene
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Draw(Game game)
        {
            // Game is a generic class, so make sure to convert it to ConsoleGame
            ConsoleGame consoleGame = (ConsoleGame)game;
            // ConsoleGame has a screen to draw whereas Game doesn't
            ConsoleScreen consoleScreen = consoleGame.Screen;

            // Clears the whole screen
            consoleScreen.Clear();
            // Draw the gameover text
            consoleScreen.DrawText(3, 7, "    Your pet left you!\nTake better care next time!");
            // When we're done drawing everything we needed, updates the screen
            consoleScreen.Show();
        }

        /// <summary>
        /// Disposes of all scene elements
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Terminate(Game game)
        {
        }

    }

}