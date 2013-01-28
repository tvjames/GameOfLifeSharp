// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" company="Thomas James">
//   Copyright Thomas James 2012
// </copyright>
// <summary>
//   The main class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameOfLife
{
    using System;
    using System.IO;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal static class MainClass
    {
        public static void Main(string[] args)
        {
            var serialiser = new WorldSerialiser(WorldSerialiserOption.Pretty);

            if (args.Length == 0)
            {
                // ensure that the basic game boards are available
                foreach (var asWorld in BuiltIn.AsWorlds())
                {
                    using (var stream = File.Open(asWorld.Key + ".gols", FileMode.Create))
                    {
                        serialiser.Serialize(stream, new World(asWorld.Value));
                    }
                }

                // print command line options 
                return;
            }

            var file = new FileInfo(args[0]);

            var world = serialiser.Deserialize(File.OpenRead(file.FullName));
            var visualiser = new ConsoleWorldVisualiser();
            var changed = Observable.FromEvent<EventHandler<EventArgs>, EventArgs>(
                handler => (sender, e) => handler(e), 
                ev => visualiser.ViewportChanged += ev, 
                ev => visualiser.ViewportChanged -= ev);

            Observable.Interval(TimeSpan.FromSeconds(0.1)).Select(
                _ =>
                    {
                        world = world.Tick();
                        return EventArgs.Empty;
                    }).Merge(changed).Subscribe(
                        _ => visualiser.Display(world), 
                        exception =>
                            {
                                Console.Clear();
                                Console.SetCursorPosition(0, 0);
                                Console.WriteLine(exception);
                            });

            var task = visualiser.WaitForInput();
            task.Start();

            Task.WaitAll(task);

            var result = Path.Combine(
                file.DirectoryName, file.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + file.Extension);
            serialiser.Serialize(File.OpenWrite(result), visualiser.World);
        }
    }
}