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
        public Cell CurrentCell { get; set; }

        public Plant() 
        {
            GrowthLevel = 0;
        }
        public void Grow()
        {
            if (GrowthLevel < 5)
            {
                GrowthLevel++; 
            }
        }

        public void Spread(World world, Cell cell)
        {
            if (IsFullyGrown)
            {
                List<Cell> neighbours = world.GetNeighbors(cell);
                foreach (var neighbourCell in neighbours)
                {
                    if (cell.Plant == null && !cell.Inhabitants.Any())
                    {
                        cell.Plant = new Plant();
                        break;
                    }
                }
            }
        }

        public void GetEaten() => GrowthLevel = 0;
    }
}
