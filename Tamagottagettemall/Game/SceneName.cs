using System.Windows.Input;

namespace Tamagottagettemall
{

    /// <summary>
    /// This is the scene where you name your pet
    /// </summary>
    public class SceneName : ISceneBase
    {

        /// <summary>
        /// Pet's name
        /// </summary>
        protected string _Name;

        /// <summary>
        /// Starts the scene elements
        /// </summary>
        /// <param name="game"></param>
        public void Start(Game game)
        {
            // Sets the initial pet name to nothing
            _Name = string.Empty;
        }

        /// <summary>
        /// Handles the logical part of the scene
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Update(Game game)
        {
            // Gets if any key was 'just pressed'
            KeyboardKey keyboardKey = game.KeyboardManager.AnyTriggered;

            // If not key was 'just pressed'
            if (keyboardKey == null)
                // Does nothing
                return;

            // If Esc was pressed
            if (keyboardKey.Key == Key.Escape)
                // Goes to the title screen
                game.SceneManager.NextScene = new SceneTitle();
            // If Enter was pressed
            else if (keyboardKey.Key == Key.Enter)
            {
                // Checks if the name length is over than 0
                int nameLength = _Name.Length;
                // If it's over than 0 it means the pet was named something
                if (nameLength > 0)
                    // So it moves to the actual game scene
                    game.SceneManager.NextScene = new SceneGame();
            }
            // If Backspace was pressed
            else if (keyboardKey.Key == Key.Back)
            {
                // Checks for the length again
                int nameLength = _Name.Length;
                // And if it's over than 0, it means that the pet was named something
                if (nameLength > 0)
                    // So removes the last character from it
                    _Name = _Name.Substring(0, nameLength - 1);
            }
            // If any other key was pressed other than those three
            else
            {
                // Gets the key name
                string keyString = keyboardKey.Key.ToString();
                // If the key name is only one character
                if (keyString.Length == 1)
                {
                    // Gets this character
                    char keyChar = keyString[0];
                    // And if it's a letter
                    if (char.IsLetter(keyChar))
                    {
                        // Checks for the length once again
                        int nameLength = _Name.Length;

                        // And if the name length is not larger than 8
                        if (nameLength < 8)
                            // Add this letter
                            _Name += keyChar;
                    }
                }
            }
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

            // Draws "Please enter your pet's name"
            consoleScreen.DrawText(8, 6, "Please enter your\n   pet's name:");
            // Centers and draw the the current pet's name
            consoleScreen.DrawText((32 - _Name.Length) / 2, 8, _Name);

            // When we're done drawing everything we needed, updates the screen
            consoleScreen.Show();
        }

        /// <summary>
        /// Disposes of all scene elements
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Terminate(Game game)
        {
            // If the next scene is the game scene
            if (game.SceneManager.NextScene is SceneGame)
            {
                // That means that our pet was correctly named, so we create its instance
                GameGlobals.Pet  = new Pet(_Name);
                // And resets the amount of days that have passed to 0
                GameGlobals.Days = 0;
            }
        }

    }

}