using System;

namespace Tamagottagettemall
{

    /// <summary>
    /// Scene where it shows your pet's status
    /// </summary>
    public class SceneReport : ISceneBase
    {

        /// <summary>
        /// Starts all scene elements
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Start(Game game)
        {
        }

        /// <summary>
        /// Function to draw the bars on the screen
        /// </summary>
        /// <param name="screen">Console screen</param>
        /// <param name="offsetX">Horizontal offset</param>
        /// <param name="offsetY">Vertical offset</param>
        /// <param name="value">A value from -1 to 1</param>
        /// <param name="highIsBad">Is a high value bad?</param>
        public void MakeBar(ConsoleScreen screen, int offsetX, int offsetY, float value, bool highIsBad = false)
        {
            // Transforms the value from -1 to 1 TO 0 to 1
            double percentage = 0.5 * (100 * value + 100);
            
            // Creates a right-sized string for the percentage
            string outputString = new string(
                '█',
                // There are only 28 blocks to draw, so that's why there's a 0.28 there
                (int)(Math.Round(percentage * 0.28))
            );

            // Calculates the right color for the bar
            ConsoleColor color;

            // If a high value is bad
            if (highIsBad)
            {
                // If it's less than 50%
                if (percentage <= 50)
                    // Makes the bar green
                    color = ConsoleColor.Green;
                // If it's less than 75%
                else if (percentage <= 75)
                    // Makes the bar yellow
                    color = ConsoleColor.Yellow;
                // If it's more than that
                else
                    // Makes the bar red
                    color = ConsoleColor.Red;
            }
            // If a high value is good
            else
            {
                // Does the opposite
                if (percentage < 25)
                    color = ConsoleColor.Red;
                else if (percentage < 50)
                    color = ConsoleColor.Yellow;
                else
                    color = ConsoleColor.Green;
            }

            // Draws the bar on the screen with the appropriate color
            screen.DrawText(offsetX, offsetY, outputString, (x, y, i, block, a, b, c, chr) =>
            {
                return new ConsoleBlock(ConsoleColor.Black, color, chr);
            });
        }

        /// <summary>
        /// Handles the logical part of the scene
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Update(Game game)
        {
            // Gets if any key was 'just pressed'
            KeyboardKey keyboardKey = game.KeyboardManager.AnyTriggered;

            // If no key was 'just pressed'
            if (keyboardKey == null)
                // Does nothing
                return;

            // Increases the amount of days that have passed
            GameGlobals.Days++;
            // Then goes back to the game scene
            game.SceneManager.NextScene = new SceneGame();
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

            // Draws STATUS on the top of the screen
            consoleScreen.DrawText(2, 2, "STATUS");

            // Then draws each status and their bars on the bottom of each
            consoleScreen.DrawText(2, 4, "Hunger");
            MakeBar(consoleScreen, 2, 5, GameGlobals.Pet.Hunger, true);

            consoleScreen.DrawText(2, 7, "Energy");
            MakeBar(consoleScreen, 2, 8, GameGlobals.Pet.Energy, false);

            consoleScreen.DrawText(2, 10, "Fat");
            MakeBar(consoleScreen, 2, 11, GameGlobals.Pet.Fat, true);

            consoleScreen.DrawText(2, 13, "Happiness");
            MakeBar(consoleScreen, 2, 14, GameGlobals.Pet.Happiness, false);

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