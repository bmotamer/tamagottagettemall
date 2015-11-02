using System.Windows.Input;

namespace Tamagottagettemall
{

    /// <summary>
    /// Defines a key from the keyboard
    /// </summary>
    public class KeyboardKey : InputButton
    {

        /// <summary>
        /// Key from the keyboard
        /// </summary>
        public Key Key;

        /// <summary>
        /// Creates a keyboard key state manager
        /// </summary>
        /// <param name="game">Game manager</param>
        /// <param name="key">Key from the keyboard</param>
        /// <param name="isDown">Is the button being pressed</param>
        public KeyboardKey(Game game, Key key, bool isDown)
            : base(game, isDown)
        {
            Key = key;
        }

    }

}