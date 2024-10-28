using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    internal abstract class Creature
    {
        public int Energy { get; set; }
        public int SightRange { get; protected set; }
        public Cell CurrentCell { get; set; }

        public abstract void Move(Cell newCell);
        public virtual void Eat(Creature other)
        {
            if (other != null)
            {
                Energy += other.Energy;
                other.Energy = 0;
            }
        }
        public abstract void Act(World world);

        public Creature(Cell startCell)
        {
            CurrentCell = startCell;
            CurrentCell.AddCreature(this);
        }


        public int GetEnergy()
        {
            return Energy;
        }
        public void SetEnergy(int value)
        {
            Energy = value;
        }

        public bool IsAlive()
        {
            return Energy > 0;
        }

        public void MoveRandomly(World world)
        {
            var neighbors = world.GetNeighbors(CurrentCell);
            if (neighbors.Count > 0)
            {
                var random = new Random();
                Cell newCell = neighbors[random.Next(neighbors.Count)];
                CurrentCell.RemoveCreature(this);
                newCell.AddCreature(this);
                CurrentCell = newCell;
            }
        }


    }
}
