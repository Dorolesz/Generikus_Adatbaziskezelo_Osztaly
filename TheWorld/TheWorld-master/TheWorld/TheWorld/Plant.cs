using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    public class Plant
    {
        public int GrowthLevel { get; private set; }
        public bool IsFullyGrown => GrowthLevel >= 5;
        public int SpreadInterval { get; private set; } // Szaporodási intervallum
        private int spreadCounter; // Szaporodási számláló
        public Cell CurrentCell { get; set; }

        public Plant(int growthLevel, int spreadInterval)
        {
            GrowthLevel = growthLevel;
            SpreadInterval = spreadInterval;
            spreadCounter = 0; // Kezdetben 0-ra állítva
        }
        public void Grow()
        {
            if (GrowthLevel < 5)
            {
                GrowthLevel++; 
            }
        }

        public void Spread(World world, Cell currentCell)
        {
            // Növeli a számlálót minden egyes körben
            spreadCounter++;

            // Csak akkor szaporodik, ha a számláló elérte az intervallumot
            if (spreadCounter >= SpreadInterval)
            {
                // Megkeresi az üres szomszédokat
                var emptyNeighbors = world.GetNeighbors(currentCell).Where(cell => cell.Plant == null && !cell.Inhabitants.Any()).ToList();

                if (emptyNeighbors.Any())
                {
                    // Kiválaszt egy random üres cellát a szomszédok közül, ahol új növény nőhet
                    var targetCell = emptyNeighbors[new Random().Next(emptyNeighbors.Count)];
                    world.AddPlant(new Plant(GrowthLevel, SpreadInterval), targetCell.X, targetCell.Y);
                }

                // Számláló visszaállítása
                spreadCounter = 0;
            }
        }

        public void GetEaten() => GrowthLevel = 0;
    }
}
