using System;

namespace Tamagottagettemall
{

    /// <summary>
    /// Defines a console block
    /// </summary>
    public struct ConsoleBlock
    {

        /// <summary>
        /// Solid background color
        /// </summary>
        public ConsoleColor BackgroundColor;
        
        /// <summary>
        /// Font color
        /// </summary>
        public ConsoleColor ForegroundColor;

        /// <summary>
        /// Single character
        /// </summary>
        public char Character;

        /// <summary>
        /// Creates a new block
        /// </summary>
        /// <param name="backgroundColor">Solid background color</param>
        /// <param name="foregroundColor">Font color</param>
        /// <param name="character">Single character</param>
        public ConsoleBlock(
            ConsoleColor backgroundColor,
            ConsoleColor foregroundColor,
            char         character
        )
        {
            // Saves the settings given
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
            Character       = character;
        }

        /// <summary>
        /// Draws the block on the console
        /// </summary>
        public void Show()
        {
            // Saves the previous color settings
            ConsoleColor previousBackgroundColor = Console.BackgroundColor;
            ConsoleColor previousForegroundColor = Console.ForegroundColor;

            // Applies the new ones
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;

            // Draws the single character
            Console.Write(Character);

            // Restores the previous color settings
            Console.BackgroundColor = previousBackgroundColor;
            Console.ForegroundColor = previousForegroundColor;
        }

        /// <summary>
        /// Returns if the current object is equal to another
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>If they're the same</returns>
        public override bool Equals(object obj)
        {
            // If the given object to compare to is null or isn't the same type of this one
            if ((obj == null) || !(obj is ConsoleBlock))
                // Then uses the original equals function
                return base.Equals(obj);

            // If the object is the same type as this, then convert it
            ConsoleBlock other = (ConsoleBlock)obj;

            // And compare if they have the same settings
            return (
                (BackgroundColor == other.BackgroundColor) &&
                (ForegroundColor == other.ForegroundColor) &&
                (Character       == other.Character      )
            );
        }

        /// <summary>
        /// Returns the current object hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return (
                           base.GetHashCode() +
                BackgroundColor.GetHashCode() +
                ForegroundColor.GetHashCode() +
                      Character.GetHashCode()
            );
        }

        /// <summary>
        /// Returns an empty block
        /// </summary>
        public static ConsoleBlock Empty
        {
            get { return new ConsoleBlock(ConsoleColor.Black, ConsoleColor.White, ' '); }
        }

    }

}