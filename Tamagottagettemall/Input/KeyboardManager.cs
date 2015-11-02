using System;
using System.Windows.Input;

namespace Tamagottagettemall
{

    /// <summary>
    /// Keyboard manager
    /// </summary>
    public class KeyboardManager
    {

        /// <summary>
        /// Collection of keyboard key state managers
        /// </summary>
        protected KeyboardKey[]  _Keys;

        /// <summary>
        /// Creates a keyboard manager
        /// </summary>
        /// <param name="game">Game manager</param>
        public KeyboardManager(Game game)
        {
            // Gets all the keys from the enumeration and how many there are of them
            Key[] keys       = (Key[])Enum.GetValues(typeof(Key));
            int   keysLength = keys.Length;

            // Creates a collection of keyboard key managers to manage every key
            // NOTE: it's keyLength - 1 because it doesn't allow you to manage the NULL key, so we can skip it
            _Keys = new KeyboardKey[keysLength - 1];

            Key key;

            // Iterates through all the keys
            for (int i = 1; i < keysLength; i++)
            {
                // And sets up every keyboard key manager
                key          = keys[i];
                _Keys[i - 1] = new KeyboardKey(game, key, Keyboard.IsKeyDown(key));
                
            }
        }

        /// <summary>
        /// Updates all the keyboard key state managers
        /// </summary>
        /// <param name="game">Game manager</param>
        public void Update(Game game)
        {
            // Iterates through all the key managers
            foreach (KeyboardKey keyboardKey in _Keys)
                // And update their states
                keyboardKey.Update(
                    game,
                    Keyboard.IsKeyDown(keyboardKey.Key)
                );
        }

        /// <summary>
        /// Gets a keyboard key state manager from the collection
        /// </summary>
        /// <param name="key">Identifier</param>
        /// <returns>The corresponding keyboard key state manager</returns>
        public KeyboardKey this[Key key]
        {
            get
            {
                // Iterates through all the keys
                foreach (KeyboardKey keyboardKey in _Keys)
                    // And if it found what you're looking for
                    if (keyboardKey.Key == key)
                        // Returns it
                        return keyboardKey;

                // Otherwise returns nothing
                return null;
            }
        }

        /// <summary>
        /// Gets if any key has the 'just pressed' state
        /// </summary>
        public KeyboardKey AnyTriggered
        {
            get
            {
                // Iterates through all the keys
                foreach (KeyboardKey keyboardKey in _Keys)
                    // If any is 'just pressed'
                    if (keyboardKey.State == InputButtonState.Triggered)
                        // Returns it
                        return keyboardKey;

                // Otherwise returns nothing
                return null;
            }
        }

    }

}