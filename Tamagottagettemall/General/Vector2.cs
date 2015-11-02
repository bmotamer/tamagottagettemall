using System;

namespace Tamagottagettemall
{

    /// <summary>
    /// Defines a vector with two components
    /// </summary>
    public struct Vector2
    {

        /// <summary>
        /// Gets or sets the x-component of the vector
        /// </summary>
        public float X;

        /// <summary>
        /// Gets or sets the y-component of the vector.
        /// </summary>
        public float Y;

        /// <summary>
        /// Initializes a new instance of Vector2
        /// </summary>
        /// <param name="x">Initial value for the x-component of the vector</param>
        /// <param name="y">Initial value for the y-component of the vector</param>
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Calculates the length of the vector squared
        /// </summary>
        public float LengthSquared
        {
            get { return X * X + Y * Y; }
        }

        /// <summary>
        /// Calculates the length of the vector
        /// </summary>
        public float Length
        {
            get { return (float)Math.Sqrt(LengthSquared); }
        }

        /// <summary>
        /// Returns a Vector2 with both of its components set to one
        /// </summary>
        public static Vector2 One
        {
            get { return new Vector2(1.0f, 1.0f); }
        }

        /// <summary>
        /// Returns the unit vector for the x-axis
        /// </summary>
        public static Vector2 UnitX
        {
            get { return new Vector2(1.0f, 0.0f); }
        }

        /// <summary>
        /// Returns the unit vector for the y-axis
        /// </summary>
        public static Vector2 UnitY
        {
            get { return new Vector2(0.0f, 1.0f); }
        }

        /// <summary>
        /// Returns a Vector2 with all of its components set to zero
        /// </summary>
        public static Vector2 Zero
        {
            get { return new Vector2(0.0f, 0.0f); }
        }

        #region Sum Overloads

        /// <summary>
        /// Sums two vectors
        /// </summary>
        /// <param name="a">Source vector</param>
        /// <param name="b">Source vector</param>
        /// <returns>Sum</returns>
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// Sums the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates added by the given value</returns>
        public static Vector2 operator +(Vector2 vector, byte value)
        {
            return new Vector2(vector.X + value, vector.Y + value);
        }

        /// <summary>
        /// Sums the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates added by the given value</returns>
        public static Vector2 operator +(Vector2 vector, sbyte value)
        {
            return new Vector2(vector.X + value, vector.Y + value);
        }

        /// <summary>
        /// Sums the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates added by the given value</returns>
        public static Vector2 operator +(Vector2 vector, short value)
        {
            return new Vector2(vector.X + value, vector.Y + value);
        }

        /// <summary>
        /// Sums the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates added by the given value</returns>
        public static Vector2 operator +(Vector2 vector, ushort value)
        {
            return new Vector2(vector.X + value, vector.Y + value);
        }

        /// <summary>
        /// Sums the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates added by the given value</returns>
        public static Vector2 operator +(Vector2 vector, int value)
        {
            return new Vector2(vector.X + value, vector.Y + value);
        }

        /// <summary>
        /// Sums the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates added by the given value</returns>
        public static Vector2 operator +(Vector2 vector, uint value)
        {
            return new Vector2(vector.X + value, vector.Y + value);
        }

        /// <summary>
        /// Sums the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates added by the given value</returns>
        public static Vector2 operator +(Vector2 vector, long value)
        {
            return new Vector2(vector.X + value, vector.Y + value);
        }

        /// <summary>
        /// Sums the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates added by the given value</returns>
        public static Vector2 operator +(Vector2 vector, ulong value)
        {
            return new Vector2(vector.X + value, vector.Y + value);
        }

        /// <summary>
        /// Sums the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates added by the given value</returns>
        public static Vector2 operator +(Vector2 vector, float value)
        {
            return new Vector2(vector.X + value, vector.Y + value);
        }

        /// <summary>
        /// Sums the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates added by the given value</returns>
        public static Vector2 operator +(Vector2 vector, double value)
        {
            return new Vector2((float)(vector.X + value), (float)(vector.Y + value));
        }

        #endregion

        #region Subtraction Overloads

        /// <summary>
        /// Subtracts two vectors
        /// </summary>
        /// <param name="a">Source vector</param>
        /// <param name="b">Source vector</param>
        /// <returns>Subtraction</returns>
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        /// <summary>
        /// Subtracts the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates subtracted by the given value</returns>
        public static Vector2 operator -(Vector2 vector, byte value)
        {
            return new Vector2(vector.X - value, vector.Y - value);
        }

        /// <summary>
        /// Subtracts the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates subtracted by the given value</returns>
        public static Vector2 operator -(Vector2 vector, sbyte value)
        {
            return new Vector2(vector.X - value, vector.Y - value);
        }

        /// <summary>
        /// Subtracts the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates subtracted by the given value</returns>
        public static Vector2 operator -(Vector2 vector, short value)
        {
            return new Vector2(vector.X - value, vector.Y - value);
        }

        /// <summary>
        /// Subtracts the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates subtracted by the given value</returns>
        public static Vector2 operator -(Vector2 vector, ushort value)
        {
            return new Vector2(vector.X - value, vector.Y - value);
        }

        /// <summary>
        /// Subtracts the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates subtracted by the given value</returns>
        public static Vector2 operator -(Vector2 vector, int value)
        {
            return new Vector2(vector.X - value, vector.Y - value);
        }

        /// <summary>
        /// Subtracts the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates subtracted by the given value</returns>
        public static Vector2 operator -(Vector2 vector, uint value)
        {
            return new Vector2(vector.X - value, vector.Y - value);
        }

        /// <summary>
        /// Subtracts the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates subtracted by the given value</returns>
        public static Vector2 operator -(Vector2 vector, long value)
        {
            return new Vector2(vector.X - value, vector.Y - value);
        }

        /// <summary>
        /// Subtracts the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates subtracted by the given value</returns>
        public static Vector2 operator -(Vector2 vector, ulong value)
        {
            return new Vector2(vector.X - value, vector.Y - value);
        }

        /// <summary>
        /// Subtracts the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates subtracted by the given value</returns>
        public static Vector2 operator -(Vector2 vector, float value)
        {
            return new Vector2(vector.X - value, vector.Y - value);
        }

        /// <summary>
        /// Subtracts the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates subtracted by the given value</returns>
        public static Vector2 operator -(Vector2 vector, double value)
        {
            return new Vector2((float)(vector.X - value), (float)(vector.Y - value));
        }

        #endregion

        #region Multiplication Overloads

        /// <summary>
        /// Multiplies two vectors
        /// </summary>
        /// <param name="a">Source vector</param>
        /// <param name="b">Source vector</param>
        /// <returns>Multiplication</returns>
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }

        /// <summary>
        /// Multiplies the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates multiplied by the given value</returns>
        public static Vector2 operator *(Vector2 vector, byte value)
        {
            return new Vector2(vector.X * value, vector.Y * value);
        }

        /// <summary>
        /// Multiplies the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates multiplied by the given value</returns>
        public static Vector2 operator *(Vector2 vector, sbyte value)
        {
            return new Vector2(vector.X * value, vector.Y * value);
        }

        /// <summary>
        /// Multiplies the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates multiplied by the given value</returns>
        public static Vector2 operator *(Vector2 vector, short value)
        {
            return new Vector2(vector.X * value, vector.Y * value);
        }

        /// <summary>
        /// Multiplies the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates multiplied by the given value</returns>
        public static Vector2 operator *(Vector2 vector, ushort value)
        {
            return new Vector2(vector.X * value, vector.Y * value);
        }

        /// <summary>
        /// Multiplies the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates multiplied by the given value</returns>
        public static Vector2 operator *(Vector2 vector, int value)
        {
            return new Vector2(vector.X * value, vector.Y * value);
        }

        /// <summary>
        /// Multiplies the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates multiplied by the given value</returns>
        public static Vector2 operator *(Vector2 vector, uint value)
        {
            return new Vector2(vector.X * value, vector.Y * value);
        }

        /// <summary>
        /// Multiplies the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates multiplied by the given value</returns>
        public static Vector2 operator *(Vector2 vector, long value)
        {
            return new Vector2(vector.X * value, vector.Y * value);
        }

        /// <summary>
        /// Multiplies the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates multiplied by the given value</returns>
        public static Vector2 operator *(Vector2 vector, ulong value)
        {
            return new Vector2(vector.X * value, vector.Y * value);
        }

        /// <summary>
        /// Multiplies the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates multiplied by the given value</returns>
        public static Vector2 operator *(Vector2 vector, float value)
        {
            return new Vector2(vector.X * value, vector.Y * value);
        }

        /// <summary>
        /// Multiplies the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates multiplied by the given value</returns>
        public static Vector2 operator *(Vector2 vector, double value)
        {
            return new Vector2((float)(vector.X * value), (float)(vector.Y * value));
        }

        #endregion

        #region Division Overloads

        /// <summary>
        /// Divides two vectors
        /// </summary>
        /// <param name="a">Source vector</param>
        /// <param name="b">Source vector</param>
        /// <returns>Division</returns>
        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X / b.X, a.Y / b.Y);
        }

        /// <summary>
        /// Divides the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates divided by the given value</returns>
        public static Vector2 operator /(Vector2 vector, byte value)
        {
            return new Vector2(vector.X / value, vector.Y / value);
        }

        /// <summary>
        /// Divides the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates divided by the given value</returns>
        public static Vector2 operator /(Vector2 vector, sbyte value)
        {
            return new Vector2(vector.X / value, vector.Y / value);
        }

        /// <summary>
        /// Divides the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates divided by the given value</returns>
        public static Vector2 operator /(Vector2 vector, short value)
        {
            return new Vector2(vector.X / value, vector.Y / value);
        }

        /// <summary>
        /// Divides the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates divided by the given value</returns>
        public static Vector2 operator /(Vector2 vector, ushort value)
        {
            return new Vector2(vector.X / value, vector.Y / value);
        }

        /// <summary>
        /// Divides the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates divided by the given value</returns>
        public static Vector2 operator /(Vector2 vector, int value)
        {
            return new Vector2(vector.X / value, vector.Y / value);
        }

        /// <summary>
        /// Divides the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates divided by the given value</returns>
        public static Vector2 operator /(Vector2 vector, uint value)
        {
            return new Vector2(vector.X / value, vector.Y / value);
        }

        /// <summary>
        /// Divides the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates divided by the given value</returns>
        public static Vector2 operator /(Vector2 vector, long value)
        {
            return new Vector2(vector.X / value, vector.Y / value);
        }

        /// <summary>
        /// Divides the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates divided by the given value</returns>
        public static Vector2 operator /(Vector2 vector, ulong value)
        {
            return new Vector2(vector.X / value, vector.Y / value);
        }

        /// <summary>
        /// Divides the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates divided by the given value</returns>
        public static Vector2 operator /(Vector2 vector, float value)
        {
            return new Vector2(vector.X / value, vector.Y / value);
        }

        /// <summary>
        /// Divides the two coordinates of the vector by a value
        /// </summary>
        /// <param name="vector">Source vector</param>
        /// <param name="value">Value</param>
        /// <returns>Vector with the two coordinates divided by the given value</returns>
        public static Vector2 operator /(Vector2 vector, double value)
        {
            return new Vector2((float)(vector.X / value), (float)(vector.Y / value));
        }

        #endregion

        /// <summary>
        /// Calculates the distance between two vectors
        /// </summary>
        /// <param name="vectorA">Source vector</param>
        /// <param name="vectorB">Source vector</param>
        /// <returns>Distance between the two vectors</returns>
        public static float Distance(Vector2 vectorA, Vector2 vectorB)
        {
            // Calculates the distance of each coordinate
            float distanceX = vectorB.X - vectorA.X;
            float distanceY = vectorB.Y - vectorA.Y;

            // If the distance of the x-coordinates is 0
            if (distanceX == 0.0f)
                // Then avoid calculations, because the total distance will be the one from the y-coordinates
                return distanceY;

            // If the distance of the y-coordinates is 0
            if (distanceY == 0.0f)
                // Then avoid calculations, because the total distance will be the one from the x-coordinates
                return distanceX;

            // Uses the pythagoras theorem to calculate the hypotenuse
            return (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
        }

    }

}