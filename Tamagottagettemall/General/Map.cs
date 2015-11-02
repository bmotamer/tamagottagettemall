using System;
using System.Collections.Generic;

namespace Tamagottagettemall
{

    /// <summary>
    /// Defines a unidimensional array behaving like a bidimensional one
    /// TODO: add a resize function
    /// </summary>
    /// <typeparam name="T">Type of data hold by each map position</typeparam>
    public class Map<T>
    {

        /// <summary>
        /// Map width
        /// </summary>
        protected int _Width;

        /// <summary>
        /// Map height
        /// </summary>
        protected int _Height;

        /// <summary>
        /// Array that holds all the each map position's data
        /// </summary>
        protected T[] _Data;

        /// <summary>
        /// Creates a new map
        /// </summary>
        /// <param name="width">Map width</param>
        /// <param name="height">Map height</param>
        public Map(
            int width,
            int height
        )
        {
            // If the given map dimensions are less than 0
            if ((width < 0) || (height < 0))
                // Interrupts the map creation
                throw new ArgumentOutOfRangeException();

            // Otherwise save the dimensions given and allocate memory for each position
            _Width  = width;
            _Height = height;
            _Data   = new T[width * height];
        }

        /// <summary>
        /// Creates a new map
        /// </summary>
        /// <param name="width">Map width</param>
        /// <param name="height">Map height</param>
        /// <param name="data">Array with 'width' * 'height' length</param>
        public Map(
            int width,
            int height,
            T[] data
        )
        {
            // If the given data is null
            if (data == null)
                // Interrupts the map creation
                throw new ArgumentNullException();

            // Saves the data length
            int dataLength = data.Length;

            // If it's not equals to the area of the rectangle formed by the dimensions given
            if (dataLength != (width * height))
                // Interrupts the map creation
                throw new ArgumentException();

            // Otherwise save the dimensions given and allocate memory for each position
            _Width  = width;
            _Height = height;
            _Data   = new T[dataLength];

            // Otherwise, just iterates through the data given
            for (int i = 0; i < dataLength; i++)
                // And copy everything
                _Data[i] = data[i];
        }

        /// <summary>
        /// Gets the map width
        /// </summary>
        public int Width { get { return _Width; } }

        /// <summary>
        /// Gets the map height
        /// </summary>
        public int Height { get { return _Height; } }

        /// <summary>
        /// Gets the map area
        /// </summary>
        public int Area { get { return _Data.Length; } }

        /// <summary>
        /// Gets or sets the value of the given map data index
        /// </summary>
        /// <param name="i">Index</param>
        /// <returns>Value</returns>
        public T this[int i]
        {
            get { return _Data[i]; }
            set { _Data[i] = value; }
        }

        /// <summary>
        /// Gets or sets the value of the given map position
        /// </summary>
        /// <param name="x">X-coordinate</param>
        /// <param name="y">Y-coordinate</param>
        /// <returns>Value</returns>
        public T this[int x, int y]
        {
            get { return _Data[x + y * _Width]; }
            set { _Data[x + y * _Width] = value; }
        }

        /// <summary>
        /// Clears the map entirely
        /// </summary>
        /// <param name="clear">Value that will be put in each map position</param>
        public void Clear(T clear = default(T))
        {
            // Iterates through the entire map
            for (int i = 0; i < _Data.Length; i++)
                // Sets the 'clear' value for each position
                _Data[i] = clear;
        }

        /// <summary>
        /// Copies data from another map
        /// </summary>
        /// <param name="map">Map to be copied</param>
        public void CopyFrom(Map<T> map)
        {
            // If the given map is null
            if (map == null)
                // Interrupts the execution of the function because there's nothing to copy from
                throw new ArgumentNullException();

            // Gets what's the smallest dimensions from the two maps, so no IndexOutOfRangeException will happen
            int width  = Math.Min(_Width, map.Width);
            int height = Math.Min(_Height, map.Height);
            int x;
            int y;

            // Iterates through the delimited area of the given map
            for (y = 0; y < height; y++)
                for (x = 0; x < width; x++)
                    // Copies the data from that area of the other map to this one
                    _Data[x + y * _Width] = map[x, y];
        }

        /// <summary>
        /// Merges another map with this one
        /// TODO: add "sourceRectangle" option
        /// </summary>
        /// <param name="offsetX">Horizontal offset</param>
        /// <param name="offsetY">Vertical offset</param>
        /// <param name="map">Incoming map</param>
        /// <param name="function">Function that merges the blocks</param>
        public void Merge(
            int    offsetX,
            int    offsetY,
            Map<T> map,
            Func<
                int, // Destination x-coordinate
                int, // Destination y-coordinate
                int, // Destination index
                T,   // Original block
                int, // Incoming map's x-coordinate
                int, // Incoming map's y-coordinate
                int, // Incoming map's index
                T,   // Incoming block
                T    // Merging result block
            > function
        )
        {
            // If any of the parameters are null
            if (
                (map      == null) ||
                (function == null)
            )
                // Interrupts the map merging because it's not possible to procceed with the operation
                throw new ArgumentNullException();

            // Saves the incoming map's dimensions for later use
            int mapWidth  = map.Width;
            int mapHeight = map.Height;

            // If any of the offsets is too low or too high
            if (
                (offsetX <= -mapWidth ) ||
                (offsetX >=  _Width   ) ||
                (offsetY <= -mapHeight) ||
                (offsetY >=  _Height  )
            )
                // Don't do anything, because the incoming map will completely out of bounds
                return;

            // To avoid processing out of bounds data, calculates what are the first inbounds coordinates
            int mapStartX = offsetX < 0 ? -offsetX : 0;
            int mapStartY = offsetY < 0 ? -offsetY : 0;
            int mapEndX   = mapWidth;
            int mapEndY   = mapHeight;

            // And then calculates what are the last inbounds coordinates
            {
                // Calculates how much the incoming map is exceeding the boundaries of the current one
                int mapExcessX = offsetX + mapWidth - _Width;
                int mapExcessY = offsetY + mapHeight - _Height;

                // And then for each dimension, subtract the excess
                if (mapExcessX > 0)
                    mapEndX -= mapExcessX;

                if (mapExcessY > 0)
                    mapEndY -= mapExcessY;
            }

            // Get some more variables declared for later use, first the incoming map's ones
            int mapX;
            int mapY;
            int mapIndex;
            T   mapBlock;

            // Then the current map's
            int x;
            int y;
            int index;

            // Iterates through the incoming map's data
            for (mapY = mapStartY; mapY < mapEndY; mapY++)
                for (mapX = mapStartX; mapX < mapEndX; mapX++)
                {
                    // Calculates everything that is required for the merging function
                    mapIndex = mapX + mapY * mapWidth;
                    mapBlock = map[mapIndex];
                    x        = mapX + offsetX;
                    y        = mapY + offsetY;
                    index    = x + y * _Width;

                    // Then merges the blocks
                    _Data[index] = function(
                        x,
                        y,
                        index,
                        _Data[index],
                        mapX,
                        mapY,
                        mapIndex,
                        mapBlock
                    );
                }
        }

        /// <summary>
        /// Returns the differences between two same-sized maps
        /// </summary>
        /// <param name="mapA">Map A</param>
        /// <param name="mapB">Map B</param>
        /// <returns>List of {startIndex, endIndex} tuples</returns>
        public static List<Tuple<int, int>> Differences(Map<T> mapA, Map<T> mapB)
        {
            // The maps must have the same size
            if ((mapA.Width != mapB.Width) || (mapA.Height != mapB.Height))
                // Otherwise we can't compare them, so we interrupt the comparison
                throw new ArgumentException();

            // Get some more variables declared for later use
            int mapIndex = 0;
            int mapArea  = mapA.Area;
            T   mapABlock;
            T   mapBBlock;

            // Creates an empty list of differences
            List<Tuple<int, int>> differences          = new List<Tuple<int, int>>();
            bool                  differenceStarted    = false;
            int                   differenceStartIndex = 0;

            // To avoid having the same exact code at two spots, an action was declared to make the code pretty
            Action tryToAddToDifferenceList = () =>
            {
                // If no difference was spotted yet,
                if (!differenceStarted)
                    // Then there's nothing to add to the list
                    return;

                // Otherwise, make a tuple with the inclusive start index and exclusive end index and add it to the list
                differences.Add(new Tuple<int, int>(differenceStartIndex, mapIndex));
                // As the difference was added to the list, turn the flag off so it can look for new ones
                differenceStarted = false;
            };

            // Iterates through the maps
            for (mapIndex = 0; mapIndex < mapArea; mapIndex++)
            {
                mapABlock = mapA[mapIndex];
                mapBBlock = mapB[mapIndex];

                // If the data from the current index is the same on both maps
                if (mapABlock.Equals(mapBBlock))
                    // Maybe it means a difference was spotted and at this index it ended, so try to add it to the list
                    tryToAddToDifferenceList();
                // If the data from the current index is different on each map
                else if (!differenceStarted)
                {
                    // Saves the starting difference index
                    differenceStartIndex = mapIndex;
                    // Saves that a difference was spotted
                    differenceStarted    = true;
                }
            }

            // It may happen that a difference goes all the way to the end, so the action is called again to make sure nothing is missing
            tryToAddToDifferenceList();

            // Returns the filled list
            return differences;
        }

    }

}