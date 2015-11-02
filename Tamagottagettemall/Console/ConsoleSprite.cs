using System;

namespace Tamagottagettemall
{

    /// <summary>
    /// Movable console block map
    /// </summary>
    public class ConsoleSprite
    {

        /// <summary>
        /// Position on the console screen
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Origin of the block map
        /// </summary>
        public Vector2 Origin;

        /// <summary>
        /// Map to be drawn
        /// </summary>
        public ConsoleBlockMap Map;

        /// <summary>
        /// Visibility flag
        /// </summary>
        public bool Visible;

        /// <summary>
        /// Creates a new sprite
        /// </summary>
        public ConsoleSprite()
        {
            // Visibility is set true as default
            Visible = true;
        }

        /// <summary>
        /// Draws the sprite on the screen
        /// </summary>
        /// <param name="screen">Console screen</param>
        public void Draw(ConsoleScreen screen)
        {
            // If it's visible and it's holding a map
            if (Visible && (Map != null))
                // Then draws it on the screen
                screen.DrawMap(
                    (int)Math.Round(Position.X - Origin.X),
                    (int)Math.Round(Position.Y - Origin.Y),
                    Map
                );
        }

    }
}