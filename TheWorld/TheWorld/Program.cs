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
            World world = new World(10, 10); // Inicializáld a világot
            bool running = true;

            while (running)
            {
                Console.Clear(); // Töröljük a konzolt minden frissítés előtt
                world.Update(); // Frissíti a világ állapotát
                world.Draw();   // Kirajzolja a világot

                // Várj egy kicsit, hogy ne terheld túl a CPU-t
                System.Threading.Thread.Sleep(100); // 100 ms késleltetés

                // Kérdezd meg, hogy meg akarod-e állítani a szimulációt
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    running = false; // Állítsd le a szimulációt, ha megnyomtad az Esc gombot
                }
            }

            Console.WriteLine("A szimuláció leállt.");
        }
    }

}
