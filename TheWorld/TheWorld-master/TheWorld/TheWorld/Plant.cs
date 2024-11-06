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
        public bool IsEaten { get; set; } = false;

        internal Cell CurrentCell { get; set; }

        public void Grow()
        {
            if (GrowthLevel < 5)
                GrowthLevel++;
            IsEaten = false;
        }

        internal void Spread(World world)
        {
            var neighbors = world.GetNeighbors(CurrentCell);
            foreach (var cell in neighbors)
            {
                if (cell.Plant == null)
                {
                    cell.AddPlant(new Plant());
                    break;
                }
            }
        }

        public void GetEaten() => GrowthLevel = 0;
    }
}
