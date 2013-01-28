// --------------------------------------------------------------------------------------------------------------------
// <copyright file="World.cs" company="Thomas James">
//   Copyright Thomas James 2012
// </copyright>
// <summary>
//   The world.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameOfLife
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The Game of Life World, modelled as a collection of live Cells
    /// </summary>
    public class World : IEnumerable<Cell>
    {
        private static readonly HashSet<Cell> CellsAroundIdentity = new HashSet<Cell>
                                                                        {
                                                                            new Cell(-1, 1), 
                                                                            new Cell(0, 1), 
                                                                            new Cell(1, 1), 
                                                                            new Cell(-1, 0), 
                                                                            new Cell(1, 0), 
                                                                            new Cell(-1, -1), 
                                                                            new Cell(0, -1), 
                                                                            new Cell(1, -1), 
                                                                        };

        /// <summary>
        /// The live cells that make up this world
        /// </summary>
        private readonly HashSet<Cell> cells;

        /// <summary>
        /// Initialises a new instance of the <see cref="World"/> class. 
        /// </summary>
        /// <param name="cells">
        /// The cells.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if cells is null
        /// </exception>
        public World(IEnumerable<Cell> cells)
        {
            if (cells == null)
            {
                throw new ArgumentNullException("cells", "cannot be null");
            }

            this.cells = new HashSet<Cell>(cells);
        }

        /// <summary>
        ///     The get enumerator.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerator" />.
        /// </returns>
        public IEnumerator<Cell> GetEnumerator()
        {
            return this.cells.GetEnumerator();
        }

        /// <summary>
        ///     The tick.
        /// </summary>
        /// <returns>
        ///     The <see cref="World" />.
        /// </returns>
        public World Tick()
        {
            var next = new HashSet<Cell>();

            foreach (var live in this.cells)
            {
                var liveCellsAround = this.LiveCellsAround(live);
                if (liveCellsAround.Count == 2 || liveCellsAround.Count == 3)
                {
                    next.Add(live);
                }

                var cellsAround = CellsAround(live);
                foreach (var cell in cellsAround)
                {
                    var isAlive = this.cells.Contains(cell);

                    if (!isAlive)
                    {
                        var liveCellsAroundDeadOne = this.LiveCellsAround(cell);

                        if (liveCellsAroundDeadOne.Count == 3)
                        {
                            next.Add(cell);
                        }
                    }
                }
            }

            return new World(next);
        }

        #region Explicit Interface Methods

        /// <summary>
        ///     The get enumerator.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerator" />.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        private static HashSet<Cell> CellsAround(Cell cell)
        {
            var result = CellsAroundIdentity.Select(x => new Cell(x.X + cell.X, x.Y + cell.Y));
            return new HashSet<Cell>(result);
        }

        private HashSet<Cell> LiveCellsAround(Cell cell)
        {
            var around = CellsAround(cell);
            around.IntersectWith(this.cells);
            return around;
        }
    }
}