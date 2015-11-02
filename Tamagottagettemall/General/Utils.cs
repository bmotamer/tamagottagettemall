using System;
using System.IO;
using System.Text;

namespace Tamagottagettemall
{

    /// <summary>
    /// Useful handy random functions
    /// </summary>
    public static class Utils
    {


        /// <summary>
        /// Function to be used with the ConsoleBlockMap / ConsoleScreen class
        /// If the source block isn't transparent, put it on the destination one
        /// </summary>
        /// <param name="dx">Destination map X</param>
        /// <param name="dy">Destination map Y</param>
        /// <param name="di">Destination map index</param>
        /// <param name="dblock">Destination map block</param>
        /// <param name="x">Source map X</param>
        /// <param name="y">Source map Y</param>
        /// <param name="i">Source map index</param>
        /// <param name="block">Source map block</param>
        /// <returns>Merged block</returns>
        public static ConsoleBlock BlockJustReplaceIfNotEmpty(
            int          dx,
            int          dy,
            int          di,
            ConsoleBlock dblock,
            int          x,
            int          y,
            int          i,
            ConsoleBlock block
        )
        {
            return char.IsWhiteSpace(block.Character) ? dblock : block;
        }

        /// <summary>
        /// Function to be used with the ConsoleBlockMap / ConsoleScreen class
        /// Replaces the destination block, no questions asked
        /// </summary>
        /// <param name="dx">Destination map X</param>
        /// <param name="dy">Destination map Y</param>
        /// <param name="di">Destination map index</param>
        /// <param name="dblock">Destination map block</param>
        /// <param name="x">Source map X</param>
        /// <param name="y">Source map Y</param>
        /// <param name="i">Source map index</param>
        /// <param name="block">Source map block</param>
        /// <returns>Merged block</returns>
        public static ConsoleBlock BlockJustReplace(
            int          dx,
            int          dy,
            int          di,
            ConsoleBlock dblock,
            int          x,
            int          y,
            int          i,
            ConsoleBlock block
        )
        {
            return block;
        }

        /// <summary>
        /// Function to be used with the ConsoleBlockMap / ConsoleScreen class
        /// If the source block isn't transparent, put it on the destination one
        /// </summary>
        /// <param name="dx">Destination map X</param>
        /// <param name="dy">Destination map Y</param>
        /// <param name="di">Destination map index</param>
        /// <param name="dblock">Destination map block</param>
        /// <param name="x">Source line character index</param>
        /// <param name="y">Source line index</param>
        /// <param name="i">Source index</param>
        /// <param name="chr">Source line character</param>
        /// <returns>Merged block</returns>
        public static ConsoleBlock TextJustReplace(
            int          dx,
            int          dy,
            int          di,
            ConsoleBlock dblock,
            int          x,
            int          y,
            int          i,
            char         chr
        )
        {
            dblock.Character = chr;
            return dblock;
        }

        /// <summary>
        /// Returns a black and white block
        /// </summary>
        /// <param name="x">Source line character index</param>
        /// <param name="y">Source line index</param>
        /// <param name="i">Source index</param>
        /// <param name="chr">Source line character</param>
        /// <param name="outOfBounds">Check ConsoleBlock.FromString for more information</param>
        /// <returns>Merged block</returns>
        public static ConsoleBlock TextBlackAndWhite(
            int  x,
            int  y,
            int  i,
            char chr,
            bool outOfBounds
        )
        {
            return new ConsoleBlock(ConsoleColor.Black, ConsoleColor.White, chr);
        }

        /// <summary>
        /// Reflects a string given an offset
        /// </summary>
        /// <param name="input">Source string</param>
        /// <param name="outputLength">Output size</param>
        /// <param name="startIndex">Offset</param>
        /// <returns>Reflected string</returns>
        public static string StringRoulette(string input, int outputLength, int startIndex)
        {
            // If the input is null
            if (input == null)
                // We can do nothing about it
                throw new ArgumentNullException();

            // Otherwise, gets its length
            int inputLength = input.Length;

            // If the length is 0
            if (inputLength == 0)
                // We cannot reflect it
                throw new ArgumentException();

            // Makes the start index a reasonable number
            startIndex %= inputLength;
            // And positive
            while (startIndex < 0)
                startIndex += inputLength;

            // Creates a string builder
            StringBuilder output = new StringBuilder();

            // Rolls and repeat through the source string and appends every character until it reaches the output size
            for (int i = 0; i < outputLength; i++)
                output.Append(input[(i + startIndex) % inputLength]);

            // Returns the string builder's output
            return output.ToString();
        }

        /// <summary>
        /// Reads everything from a file
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>All the text from the file</returns>
        public static string StringReadFile(string path)
        {
            string text;

            // Opens the file
            using (StreamReader streamReader = new StreamReader(path))
                // Reads it all
                text = streamReader.ReadToEnd();

            // Returns what was in it
            return text;
        }

        /// <summary>
        /// Pads left and right
        /// </summary>
        /// <param name="input">Source string</param>
        /// <param name="outputLength">Output size</param>
        /// <returns>Centered string</returns>
        public static string StringPadBoth(string input, int outputLength)
        {
            // Gets the input length
            int inputLength = input.Length;
            // If it's larger than the output
            if (inputLength >= outputLength)
                // Then does nothing
                return input;
            // Calculates the left padding
            int leftPadding = (outputLength - inputLength) / 2 + inputLength;
            // Then pads left and right
            return input.PadLeft(leftPadding).PadRight(outputLength);
        }

        /// <summary>
        /// Keeps a float value between two values
        /// </summary>
        /// <param name="num">Source value</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>Clamped float</returns>
        public static float FloatClamp(float num, float min, float max)
        {
            return num < min ? min : num > max ? max : num;
        }

        /// <summary>
        /// Gets the index of the largest value from a given array
        /// </summary>
        /// <param name="arguments">Float values</param>
        /// <returns>Index</returns>
        public static int FloatMaxIndex(params float[] arguments)
        {
            // First we assume that the max is the first one
            float max = arguments[0];
            int maxIndex = 0;

            float currentArgument;

            // Then we loop through all the given values
            for (int i = 1; i < arguments.Length; i++)
            {
                currentArgument = arguments[i];
                // And if there's any value bigger than the current max value
                if (currentArgument > max)
                {
                    // Then we replace it and saves the new index
                    max = currentArgument;
                    maxIndex = i;
                }
            }

            // Returns the index that's got the largest value
            return maxIndex;
        }

    }

}
