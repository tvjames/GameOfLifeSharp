// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameOfLifeRules.cs" company="Thomas James">
//   Copyright Thomas James 2012
// </copyright>
// <summary>
//   The game of life rules.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameOfLife
{
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture()]
    public class GameOfLifeRules
    {
        /// <summary>
        ///     The a single cell world_ simulation run once_ an empty world is the result.
        /// </summary>
        [Test()]
        public void ASingleCellWorld_SimulationRunOnce_AnEmptyWorldIsTheResult()
        {
            var expected = new HashSet<Cell>();

            var world = new World(new[] { new Cell(0, 0), });
            var actual = world.Tick();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     The a three cell world in a row_ simulation run once_ a three cell world in a column is the result.
        /// </summary>
        [Test()]
        public void AThreeCellWorldInARow_SimulationRunOnce_AThreeCellWorldInAColumnIsTheResult()
        {
            var expected = new HashSet<Cell>() { new Cell(0, 1), new Cell(0, 0), new Cell(0, -1) };

            var world = new World(new[] { new Cell(-1, 0), new Cell(0, 0), new Cell(1, 0) });
            var actual = world.Tick();

            Is.EquivalentTo(expected).Matches(actual);
        }

        /// <summary>
        ///     The an empty world_ simulation run once_ an empty world is the result.
        /// </summary>
        [Test()]
        public void AnEmptyWorld_SimulationRunOnce_AnEmptyWorldIsTheResult()
        {
            var expected = new HashSet<Cell>();

            var world = new World(new Cell[0]);
            var actual = world.Tick();

            Assert.AreEqual(expected, actual);
        }
    }
}