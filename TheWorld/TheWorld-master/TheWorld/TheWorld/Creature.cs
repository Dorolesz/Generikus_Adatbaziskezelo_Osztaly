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
        private World _world;

        public abstract void Move(Cell newCell);
        public abstract void Eat(Creature other);

        public Creature(int energy, int sightRange, Cell currentCell, World world)
        {
            Energy = energy;
            SightRange = sightRange;
            CurrentCell = currentCell;
            _world = world;

        }

        protected Creature(int energy, int sightRange, Cell currentCell)
        {
            Energy = energy;
            SightRange = sightRange;
            CurrentCell = currentCell;
        }

        public bool IsAlive()
        {
            return Energy > 0;
        }

        public void EatPlant()
        {
            if (CurrentCell?.Plant != null)
            {
                Energy += 5;
                CurrentCell.Plant = null;

                _world.DecreasePlantCount();
            }
        }
    }
}
