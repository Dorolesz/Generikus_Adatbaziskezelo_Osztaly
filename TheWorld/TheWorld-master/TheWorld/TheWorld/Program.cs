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
        static void Main(string[] args)
        {
            // Létrehozzuk a 10x10-es világot
            World world = new World(10, 10);

            // Hozzáadunk néhány növényt és lényt a világ különböző celláihoz
            Plant plant1 = new Plant();
            Plant plant2 = new Plant();

            // Herbivorok hozzáadása
            Cell herbivoreCell1 = world.GetCell(2, 3);
            if (herbivoreCell1 != null)  // Ellenőrizzük, hogy a cella nem null
            {
                Herbivore herbivore1 = new Herbivore(10, 5, herbivoreCell1, 1);
                world.AddCreature(herbivore1, 2, 3);
            }

            Cell herbivoreCell2 = world.GetCell(4, 4);
            if (herbivoreCell2 != null)  // Ellenőrizzük, hogy a cella nem null
            {
                Herbivore herbivore2 = new Herbivore(10, 5, herbivoreCell2, 1);
                world.AddCreature(herbivore2, 4, 4);
            }

            Cell herbivoreCell3 = world.GetCell(6, 6);
            if (herbivoreCell3 != null)  // Ellenőrizzük, hogy a cella nem null
            {
                Herbivore herbivore3 = new Herbivore(10, 5, herbivoreCell3, 1);
                world.AddCreature(herbivore3, 6, 6);
            }

            Cell herbivoreCell4 = world.GetCell(8, 8);
            if (herbivoreCell4 != null)  // Ellenőrizzük, hogy a cella nem null
            {
                Herbivore herbivore4 = new Herbivore(10, 5, herbivoreCell4, 1);
                world.AddCreature(herbivore4, 8, 8);
            }

            // Carnivor hozzáadása
            Cell carnivoreCell = world.GetCell(5, 5);
            if (carnivoreCell != null)  // Ellenőrizzük, hogy a cella nem null
            {
                Carnivore carnivore1 = new Carnivore(15, 7, carnivoreCell, 1); // Megadjuk a size értékét
                world.AddCreature(carnivore1, 5, 5);
            }

            // Növények hozzáadása
            world.AddPlant(plant1, 1, 1);
            world.AddPlant(plant2, 3, 3);

            // Végtelen ciklus, amely másodpercenként frissíti a világot
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true); // Olvassuk be a billentyűleütést anélkül, hogy megjelenítenénk
                    break; // Kilépünk a ciklusból
                }

                Console.Clear(); // Töröljük a konzol tartalmát
                world.Update(); // Frissítjük a világot
                world.Display(); // Megjelenítjük a világot a konzolon
                Thread.Sleep(1000); // Várunk 1 másodpercet
            }

        }
    }
}
