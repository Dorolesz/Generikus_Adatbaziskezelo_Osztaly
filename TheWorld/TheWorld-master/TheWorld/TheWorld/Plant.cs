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
            spreadCounter++;

            if (spreadCounter >= SpreadInterval)
            {
                var emptyNeighbors = world.GetNeighbors(currentCell).Where(cell => cell.Plant == null && !cell.Inhabitants.Any()).ToList();

                if (emptyNeighbors.Any())
                {
                    var targetCell = emptyNeighbors[new Random().Next(emptyNeighbors.Count)];
                    world.AddPlant(new Plant(GrowthLevel, SpreadInterval), targetCell.X, targetCell.Y);
                }

                spreadCounter = 0;
            }
        }

        public void GetEaten() => GrowthLevel = 0;
    }
}
