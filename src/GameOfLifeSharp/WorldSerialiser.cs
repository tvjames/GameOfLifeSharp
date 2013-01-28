// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorldSerialiser.cs" company="Thomas James">
//   Copyright Thomas James 2012
// </copyright>
// <summary>
//   The world serialiser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameOfLife
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Serialiser for the game world
    /// </summary>
    public class WorldSerialiser
    {
        /// <summary>
        /// Serialisation options
        /// </summary>
        private readonly WorldSerialiserOption options;

        /// <summary>
        /// Initialises a new instance of the <see cref="WorldSerialiser"/> class. 
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public WorldSerialiser(WorldSerialiserOption options)
        {
            this.options = options;
        }

        /// <summary>
        /// Deserialises a game world from a stream
        /// </summary>
        /// <param name="serializationStream">
        /// The serialization stream.
        /// </param>
        /// <returns>
        /// The <see cref="World"/>.
        /// </returns>
        public World Deserialize(Stream serializationStream)
        {
            var result = new List<Cell>();
            using (var reader = new StreamReader(serializationStream))
            {
                var index = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var lineNumber = index++;
                    var parts = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    var points = parts.SelectMany((x, i) => DeserialiseCell(x, i, lineNumber));
                    result.AddRange(points);
                }
            }

            return new World(result);
        }

        /// <summary>
        /// Serialises a game world to a stream
        /// </summary>
        /// <param name="serializationStream">
        /// The serialization stream.
        /// </param>
        /// <param name="world">
        /// The world.
        /// </param>
        public void Serialize(Stream serializationStream, World world)
        {
            using (var writer = new StreamWriter(serializationStream))
            {
                var lineLength = 0;
                foreach (var value in world.Select(SerialiseCell))
                {
                    writer.Write(value);
                    writer.Write(" ");

                    lineLength += value.Length + 1;

                    if (this.options == WorldSerialiserOption.Pretty && lineLength > 120)
                    {
                        writer.WriteLine();
                        lineLength = 0;
                    }
                }

                writer.WriteLine();
            }
        }

        private static IEnumerable<Cell> DeserialiseCell(string pair, int index, int line)
        {
            int x, y;
            var parts = pair.Split(",".ToCharArray(), 2);

            if (!int.TryParse(parts[0], out x))
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Line: {0}, Pair: {1} The coordinate pair's x value is not able to be parsed into an int", 
                        line, 
                        index));
            }

            if (!int.TryParse(parts[1], out y))
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Line: {0}, Pair: {1} The coordinate pair's y value is not able to be parsed into an int", 
                        line, 
                        index));
            }

            return new[] { new Cell(x, y), };
        }

        private static string SerialiseCell(Cell cell)
        {
            var result = string.Concat(cell.X, ",", cell.Y);
            return result;
        }
    }
}