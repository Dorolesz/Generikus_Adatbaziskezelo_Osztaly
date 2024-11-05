using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            World world = new World(10, 10);
            bool running = true;

            while (running)
            {
                Console.Clear();
                world.Update();
                world.Draw();

                System.Threading.Thread.Sleep(100);

                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    running = false;
                }
            }

            Console.WriteLine("A szimuláció leállt.");
        }
    }

}
