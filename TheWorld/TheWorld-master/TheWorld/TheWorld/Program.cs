using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheWorld
{
    class Program
    {
        public static void Main(string[] args)
        {
            World world = new World(10, 10);
            world.AddCreature(new Carnivore(100, 2, world.GetCell(2, 2), 50), 2, 2);
            world.AddCreature(new Herbivore(80, 3, world.GetCell(1, 1), 30), 1, 1);
            world.AddPlant(new Plant(5, 3), 6, 3);



            while (true)
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    break;
                }

                Console.Clear();
                world.Update();
                world.Display();
                Thread.Sleep(1000);

            }
        }
    }
}
