using System;
using System.Collections.Generic;

namespace Tamagottagettemall
{

    /// <summary>
    /// Console screen manager
    /// </summary>
    public class ConsoleScreen
    {

        /// <summary>
        /// Current map state
        /// </summary>
        protected ConsoleBlockMap _Map;

        /// <summary>
        /// Last map state
        /// </summary>
        protected ConsoleBlockMap _LastMap;

        /// <summary>
        /// Creates a new console screen manager
        /// </summary>
        /// <param name="width">Screen width</param>
        /// <param name="height">Screen height</param>
        public ConsoleScreen(int width, int height)
        {
            // Increases the height to avoid annoying screen scrollings
            // TODO: figure out a way to avoid screen scrolling without having to do this
            ++height;

            // Sets the screen size
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            // Hides the ugly cursor
            Console.CursorVisible = false;

            // Restores the given height
            --height;

            // Creates a map for drawing
            _Map     = new ConsoleBlockMap(width, height);
            _LastMap = _Map.Clone;

            // Prints it entirely on the console
            Show(false);
        }

        /// <summary>
        /// Draws a map on the screen
        /// </summary>
        /// <param name="offsetX">Horizontal offset</param>
        /// <param name="offsetY">Vertical offset</param>
        /// <param name="map">Map to be drawn</param>
        /// <param name="function">Function that merges the blocks</param>
        public void DrawMap(
            int             offsetX,
            int             offsetY,
            ConsoleBlockMap map,
            Func<
                int,
                int,
                int,
                ConsoleBlock,
                int,
                int,
                int,
                ConsoleBlock,
                ConsoleBlock
            > function = null
        )
        {
            if (function == null)
                function = Utils.BlockJustReplaceIfNotEmpty;

            _Map.Merge(offsetX, offsetY, map, function);
        }

        /// <summary>
        /// Draws a text on the screen
        /// </summary>
        /// <param name="offsetX">Horizontal offset</param>
        /// <param name="offsetY">Vertical offset</param>
        /// <param name="text">Text to be drawn</param>
        /// <param name="function">Function that merges the text with the blocks</param>
        public void DrawText(
            int offsetX,
            int offsetY,
            string text,
            Func<
                int,
                int,
                int,
                ConsoleBlock,
                int,
                int,
                int,
                char,
                ConsoleBlock
            > function = null
        )
        {
            if (function == null)
                function = Utils.TextJustReplace;

            _Map.MergeText(offsetX, offsetY, text, function);
        }

        /// <summary>
        /// Clears the screen
        /// </summary>
        public void Clear()
        {
            _Map.Clear(ConsoleBlock.Empty);
        }

        /// <summary>
        /// Clears the screen with a given background
        /// </summary>
        /// <param name="background">Background</param>
        public void Clear(ConsoleBlockMap background)
        {
            _Map.CopyFrom(background);
        }

        /// <summary>
        /// Shows the current screen state
        /// TODO: make the draw functions return the differences, and add an enum so there can be different ways to show
        /// </summary>
        /// <param name="compareWithLast">Should it compare to the last screen to improve performance</param>
        public void Show(bool compareWithLast = true)
        {
            // If the the flag was left on
            if (compareWithLast)
            {
                // Gets the differences from the current and last map state
                List<Tuple<int, int>> differenceList = ConsoleBlockMap.Differences(_Map, _LastMap);

                int index;

                // Iterates through each difference
                foreach (Tuple<int, int> difference in differenceList)
                    for (index = difference.Item1; index < difference.Item2; index++)
                    {
                        // And updates only the different blocks from the screen
                        Console.SetCursorPosition(index % _Map.Width, index / _Map.Width);
                        _Map[index].Show();
                    }
            }
            // If it was NOT
            else
            {
                // Then get yourself ready
                Console.SetCursorPosition(0, 0);
                // For the lag
                _Map.Show();
            }

            // Updates the last map state
            _LastMap.CopyFrom(_Map);
        }

    }

}
