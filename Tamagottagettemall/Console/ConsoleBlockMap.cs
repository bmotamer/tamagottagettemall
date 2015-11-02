using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagottagettemall
{

    /// <summary>
    /// Defines a unidimensional ConsoleBlock array behaving like a bidimensional one
    /// TODO: make a static function FromFile
    /// </summary>
    public class ConsoleBlockMap : Map<ConsoleBlock>
    {

        /// <summary>
        /// Creates a new console block map
        /// </summary>
        /// <param name="width">Map width</param>
        /// <param name="height">Map height</param>
        public ConsoleBlockMap(
            int width,
            int height
        ) : base(width, height)
        {
        }

        /// <summary>
        /// Creates a new console block map
        /// </summary>
        /// <param name="width">Map width</param>
        /// <param name="height">Map height</param>
        /// <param name="data">Array with 'width' * 'height' length</param>
        public ConsoleBlockMap(
            int width,
            int height,
            ConsoleBlock[] data
        )
            : base(width, height, data)
        {
        }

        /// <summary>
        /// Draws the entire map on the console
        /// WARNING: this function is very slow. You should consider using ConsoleScreen if done multiple times.
        /// </summary>
        public void Show()
        {
            // If the map width is the same as the console width
            if (_Width == Console.BufferWidth)
            {
                // Then just iterates through all the blocks
                for (int i = 0; i < _Data.Length; i++)
                    // And print them because they won't be out of place
                    _Data[i].Show();
            }
            // If it's not
            else
            {
                // Then it's necessary to keep track of when it's the end of the map
                int x = 0;

                // Iterates through all the blocks
                for (int i = 0; i < _Data.Length; i++)
                {
                    // Print each one of them
                    _Data[i].Show();

                    // And if it's the end of the line
                    if (++x == _Width)
                    {
                        // Go to the next one
                        Console.Write('\n');
                        x = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Merges a text with this map
        /// TODO: add "sourceRectangle" option
        /// </summary>
        /// <param name="offsetX">Horizontal offset</param>
        /// <param name="offsetY">Vertical offset</param>
        /// <param name="text">Incoming text</param>
        /// <param name="function">Function that merges the block and the characters</param>
        public void MergeText(
            int offsetX,
            int offsetY,
            string text,
            Func<
                int,          // Destination x-coordinate
                int,          // Destination y-coordinate
                int,          // Destination index
                ConsoleBlock, // Original block
                int,          // Line character index
                int,          // Line index
                int,          // Text index
                char,         // Line character
                ConsoleBlock  // Merging result block
            > function
        )
        {
            // If the vertical offset is too high
            if (offsetY >= _Height)
                // The text won't be seen, so avoid doing useless calculations
                return;

            // Get some more variables declared for later use
            int  i          = 0;
            int  x          = 0;
            int  y          = 0;
            int  destX;
            int  destY      = offsetY;
            int  destIndex;
            int  textLength = text.Length;
            char textChar;

            // Iterates through the given text
            for (; i < textLength; i++)
            {
                // Gets the current character
                textChar = text[i];

                // If it is a "break line" character
                if (textChar == '\n')
                {
                    // Goes to the next line
                    x = 0;
                    ++y;

                    // Calculates at what height it will start
                    destY = y + offsetY;

                    // If the next line is too far down
                    if (destY >= _Height)
                        // It won't be seen, so avoid doing useless calculations
                        break;
                }
                // If it's not a "break line"
                else
                {
                    // Calculates at what place in the horizontal it will start
                    destX = x + offsetX;
                    destIndex = destX + destY * _Width;
                    
                    
                    // If the place where it's supposed to be merged is inbounds
                    if ((destX >= 0) && (destX < _Width) && (destY >= 0) && (destY < _Height))
                        // So then merge the character with the block
                        _Data[destIndex] = function(destX, destY, destIndex, _Data[destIndex], x, y, i, textChar);

                    ++x;
                }
            }
        }

        /// <summary>
        /// Gets a copy of the current map
        /// </summary>
        public ConsoleBlockMap Clone
        {
            get { return new ConsoleBlockMap(_Width, _Height, _Data); }
        }

        /// <summary>
        /// Turns a string into a console block map
        /// </summary>
        /// <param name="input">Source string</param>
        /// <param name="function">Function that generates the blocks</param>
        /// <returns></returns>
        public static ConsoleBlockMap FromString(
            string input,
            Func<
                int,         // Line character index
                int,         // Line index
                int,         // Map index
                char,        // Line character
                bool,        // Is this character a padding character
                ConsoleBlock // Generated block
            > function
        )
        {
            // Saves the string length for later use
            int inputLength = input.Length;

            // It's necessary to check if the source string is not empty
            if (inputLength == 0)
                // Because if it is, it's not possible to create a map out of it
                throw new ArgumentException();

            // Get some more variables declared for later use
            char            inputChar;
            StringBuilder   stringBuilder = new StringBuilder();
            string          line;
            List<string>    lineList      = new List<string>();
            int             lineLength    = 0;
            int             x;
            int             y;
            int             mapWidth      = 0;
            int             mapHeight     = 0;
            ConsoleBlockMap output;

            // To avoid having the same exact code at two spots, an action was declared to make the code pretty
            Action endLine = () =>
            {
                // This function basically gets the completely built line
                line = stringBuilder.ToString();

                // And if it's size is bigger than the biggest one
                if (lineLength > mapWidth)
                    // Updates the map width
                    mapWidth = lineLength;
                
                // Increases the map height, so there's space for the next line
                ++mapHeight;

                // Saves the built line to the list
                lineList.Add(line);
                // Clears the buffer from the builder
                stringBuilder.Clear();
            };

            // Iterates through the text
            for (x = 0; x < inputLength; x++)
            {
                // Gets the current text character
                inputChar = input[x];

                // If it's a "break line" character
                if (inputChar == '\n')
                {
                    // Then just go to the next line
                    endLine();
                    // Resets the line length to 0
                    lineLength = 0;
                }
                // If it's not a "break line"
                else
                {
                    // Then build the line
                    stringBuilder.Append(inputChar);
                    // And increases the line length
                    ++lineLength;
                }
            }

            // It may happen that a line goes all the way to the end, so the action is called again to make sure nothing is missing
            endLine();

            // Now that the required map size is known, creates it
            output = new ConsoleBlockMap(mapWidth, mapHeight);

            // And start filling it
            for (y = 0; y < mapHeight; y++)
            {
                // Goes through each line and get their length
                line       = lineList[y];
                lineLength = line.Length;

                // Goes through each character of each line
                for (x = 0; x < mapWidth; x++)
                    // Not all line shave the same size, so if the current character index is in the line length limit
                    if (x < lineLength)
                        // Then just generate a block normally
                        output[x, y] = function(x, y, x + y * mapWidth, line[x], false);
                    // If it's beyond that limit
                    else
                        // Then just generate a padding block
                        output[x, y] = function(x, y, x + y * mapWidth, ' ', true);
            }

            // Returns the completely filled console block map
            return output;
        }

    }

}