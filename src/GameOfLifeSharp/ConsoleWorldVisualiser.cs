// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleWorldVisualiser.cs" company="Thomas James">
//   Copyright Thomas James 2012
// </copyright>
// <summary>
//   The console world visualiser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameOfLife
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// A world visualiser that uses the console.
    /// </summary>
    public class ConsoleWorldVisualiser : IWorldVisualiser
    {
        /// <summary>
        /// The viewport, this should always be less than or equal to the console window size
        /// </summary>
        private readonly Size viewport = new Size(80, 24);

        /// <summary>
        /// A flag to indicate that the application should exit.
        /// </summary>
        private volatile bool exitApplication;

        /// <summary>
        /// The position in the game world to use as the top-left drawing point
        /// </summary>
        private Point topLeft = new Cell(0, 0);

        /// <summary>
        /// Initialises a new instance of the <see cref="ConsoleWorldVisualiser"/> class. 
        /// </summary>
        public ConsoleWorldVisualiser()
        {
            this.ViewportChanged += (sender, e) => { };
        }

        /// <summary>
        /// The viewport changed event will fire when the viewport size or position is changed
        /// </summary>
        public event EventHandler<EventArgs> ViewportChanged;

        /// <summary>
        /// Gets the world that is being visualised
        /// </summary>
        public World World { get; private set; }

        /// <summary>
        /// Renders the world to the console
        /// </summary>
        /// <param name="world">
        /// The world.
        /// </param>
        public void Display(World world)
        {
            Point left;
            Size size;

            lock (this)
            {
                left = this.topLeft;
                size = this.viewport;
            }

            Paint(world, left, size);
            this.World = world;
        }

        /// <summary>
        /// A REPL loop to allow the visualisation to be controlled.
        /// </summary>
        /// <returns>
        /// A task that can be waited on for the application to exit
        /// </returns>
        public Task WaitForInput()
        {
            this.MoveViewport(0, 0);
            return new Task(
                () =>
                    {
                        while (!this.exitApplication)
                        {
                            var key = Console.ReadKey();
                            var action = this.LookupActionForKey(key);
                            action();
                        }
                    });
        }

        private static bool IsCellWithinViewport(Point cell, Point topLeft, Size viewport)
        {
            if (cell.X < topLeft.X || cell.X >= topLeft.X + viewport.Width)
            {
                return false;
            }

            if (cell.Y < topLeft.Y || cell.Y >= topLeft.Y + viewport.Height)
            {
                return false;
            }

            return true;
        }

        private static void Paint(World world, Point topLeft, Size viewport)
        {
            Console.Clear();

            var printable = from cell in world
                            where IsCellWithinViewport(cell, topLeft, viewport)
                            select ProjectCellToScreen(cell, topLeft);

            foreach (var position in printable)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write("X");
            }

            Console.SetCursorPosition(0, 0);
        }

        private static Point ProjectCellToScreen(Point cell, Point topLeft)
        {
            return new Cell(cell.X - topLeft.X, cell.Y - topLeft.Y);
        }

        private Action LookupActionForKey(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Q)
            {
                return () => this.exitApplication = true;
            }

            if (key.Key == ConsoleKey.H)
            {
                return () => this.SetViewportTopLeft(0, 0);
            }

            if (key.Key == ConsoleKey.UpArrow)
            {
                return () => this.MoveViewport(0, -1);
            }

            if (key.Key == ConsoleKey.DownArrow)
            {
                return () => this.MoveViewport(0, 1);
            }

            if (key.Key == ConsoleKey.LeftArrow)
            {
                return () => this.MoveViewport(-1, 0);
            }

            if (key.Key == ConsoleKey.RightArrow)
            {
                return () => this.MoveViewport(1, 0);
            }

            return () => { };
        }

        private void MoveViewport(int dx, int dy)
        {
            lock (this)
            {
                this.topLeft = new Cell(this.topLeft.X + dx, this.topLeft.Y + dy);
            }

            this.ViewportChanged(this, EventArgs.Empty);
        }

        private void SetViewportTopLeft(int x, int y)
        {
            lock (this)
            {
                this.topLeft = new Cell(x, y);
            }

            this.ViewportChanged(this, EventArgs.Empty);
        }
    }
}