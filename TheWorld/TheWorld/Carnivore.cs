using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    internal class Carnivore : Creature
    {
        public Carnivore(Cell startCell) : base(startCell)
        {
            Energy = 15;
            SightRange = 5;
        }

        public override void Move(Cell newCell)
        {
            CurrentCell.RemoveCreature(this);
            CurrentCell = newCell;
            CurrentCell.AddCreature(this);
        }

        public override void Act(World world)
        {
            MoveRandomly(world);
            Eat(GetPrey(world));
        }

        private Creature GetPrey(World world)
        {
            var neighbors = world.GetNeighbors(CurrentCell);
            foreach (var neighbor in neighbors)
            {
                var prey = neighbor.Inhabitants.FirstOrDefault(c => c is Herbivore);
                if (prey != null)
                    return prey;
            }
            return null;
        }
    }
}
