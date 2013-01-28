// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cell.cs" company="Thomas James">
//   Copyright Thomas James 2012
// </copyright>
// <summary>
//   The cell.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameOfLife
{
    using System.Drawing;

    /// <summary>
    /// The cell represents a "live" cell in the game. A cell is only aware of it's own position. 
    /// The cell is immutable once constructed. 
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// The position of the cell
        /// </summary>
        private readonly Point value;

        /// <summary>
        /// Initialises a new instance of the <see cref="Cell"/> class. 
        /// </summary>
        public Cell()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Cell"/> class. 
        /// </summary>
        /// <param name="x">
        /// The x position.
        /// </param>
        /// <param name="y">
        /// The y position.
        /// </param>
        public Cell(int x, int y)
        {
            this.value = new Point(x, y);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Cell"/> class. 
        /// </summary>
        /// <param name="value">
        /// The cell position.
        /// </param>
        public Cell(Point value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets the x position of the cell
        /// </summary>
        public int X
        {
            get
            {
                return this.value.X;
            }
        }

        /// <summary>
        /// Gets the y position of the cell
        /// </summary>
        public int Y
        {
            get
            {
                return this.value.Y;
            }
        }

        /// <summary>
        /// Provides an implicit conversion from Cell to Point
        /// </summary>
        /// <param name="cell">
        /// The cell.
        /// </param>
        /// <returns>
        /// The position of the cell
        /// </returns>
        public static implicit operator Point(Cell cell)
        {
            return new Point(cell.X, cell.Y);
        }

        public override bool Equals(object obj)
        {
            var cell = obj as Cell;
            return !ReferenceEquals(null, cell) && this.value.Equals(cell.value);
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}