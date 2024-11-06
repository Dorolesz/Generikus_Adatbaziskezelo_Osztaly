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
        public int SightRange { get; set; }
        public Cell CurrentCell { get; set; }

        public abstract void Move(Cell newCell);
        public abstract void Eat(Creature other);

        public Creature(int energy, int sightRange, Cell startCell)
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
                CurrentCell = newCell;
                newCell.AddCreature(this);
            }
        }


    }
}
