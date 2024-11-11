using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    public abstract class Creature
    {
        public int Energy { get; set; }
        public int SightRange { get; set; }
        public Cell CurrentCell { get; set; }

        public abstract void Move(Cell newCell);
        public abstract void Eat(Creature other);

        public Creature(int energy, int sightRange, Cell currentCell)
        {
            Energy = energy;
            SightRange = sightRange;
            CurrentCell = currentCell;
        }

        public bool IsAlive()
        {
            return Energy > 0;
        }
    }
}
