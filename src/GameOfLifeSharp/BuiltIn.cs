// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuiltIn.cs" company="Thomas James">
//   Copyright Thomas James 2012
// </copyright>
// <summary>
//   The game state.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameOfLife
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Built in initial game states
    /// </summary>
    public class BuiltIn
    {
        /// <summary>
        /// Provides the built in game states as a dictionary of name &amp; initial game state
        /// </summary>
        /// <returns>
        /// Dictionary of name &amp; initial game state
        /// </returns>
        public static IDictionary<string, IEnumerable<Cell>> AsWorlds()
        {
            return new Dictionary<string, IEnumerable<Cell>>()
                       {
                           { "Acorn", Acorn() }, 
                           { "Blinker", Blinker() }, 
                           { "Diehard", Diehard() }, 
                           { "GliderGun", GliderGun() }, 
                           { "RPentomino", RPentomino() }
                       };
        }

        /// <summary>
        /// The Acorn pattern initial state
        /// </summary>
        /// <returns>
        /// Enumeration of cells that represent the pattern
        /// </returns>
        public static IEnumerable<Cell> Acorn()
        {
            return Flatten(Range(2, 4), Range(3, 2, 4), Range(5, 3), Range(6, 4), Range(7, 4), Range(8, 4));
        }

        /// <summary>
        /// The Blinker pattern initial state
        /// </summary>
        /// <returns>
        /// Enumeration of cells that represent the pattern
        /// </returns>
        public static IEnumerable<Cell> Blinker()
        {
            return new[] { new Cell(4, 4), new Cell(5, 4), new Cell(6, 4), };
        }

        /// <summary>
        /// The Diehard pattern initial state
        /// </summary>
        /// <returns>
        /// Enumeration of cells that represent the pattern
        /// </returns>
        public static IEnumerable<Cell> Diehard()
        {
            return Flatten(Range(2, 3), Range(3, 3, 4), Range(7, 4), Range(8, 2, 4), Range(9, 4));
        }

        /// <summary>
        /// The Glider-Gun pattern initial state
        /// </summary>
        /// <returns>
        /// Enumeration of cells that represent the pattern
        /// </returns>
        public static IEnumerable<Cell> GliderGun()
        {
            return Flatten(
                Range(1, 6, 7), 
                Range(2, 6, 7), 
                Range(11, 6, 7, 8), 
                Range(12, 5, 9), 
                Range(13, 4, 10), 
                Range(14, 4, 10), 
                Range(15, 7), 
                Range(16, 5, 9), 
                Range(17, 6, 7, 8), 
                Range(18, 7), 
                Range(21, 4, 5, 6), 
                Range(22, 4, 5, 6), 
                Range(23, 3, 7), 
                Range(25, 2, 3, 7, 8), 
                Range(35, 4, 5), 
                Range(36, 4, 5));
        }

        /// <summary>
        /// The RPentomino pattern initial state
        /// </summary>
        /// <returns>
        /// Enumeration of cells that represent the pattern
        /// </returns>
        public static IEnumerable<Cell> RPentomino()
        {
            return Flatten(Range(2, 3), Range(3, 2, 3, 4), Range(4, 2));
        }

        private static IEnumerable<Cell> Flatten(params IEnumerable<Cell>[] cellsCells)
        {
            return cellsCells.SelectMany(x => x);
        }

        private static IEnumerable<Cell> Range(int x, params int[] ys)
        {
            return ys.Select(y => new Cell(x, y));
        }
    }
}