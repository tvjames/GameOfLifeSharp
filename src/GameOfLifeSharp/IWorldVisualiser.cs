// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWorldVisualiser.cs" company="Thomas James">
//   Copyright Thomas James 2012
// </copyright>
// <summary>
//   The WorldVisualiser interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameOfLife
{
    using System;

    /// <summary>
    /// The World Visualiser interface to allow a world to be visualised in a number of different ways
    /// </summary>
    public interface IWorldVisualiser
    {
        /// <summary>
        /// The viewport changed event will fire when the viewport has changed
        /// </summary>
        event EventHandler<EventArgs> ViewportChanged;

        /// <summary>
        /// Displays the world
        /// </summary>
        /// <param name="world">
        /// The world.
        /// </param>
        void Display(World world);
    }
}