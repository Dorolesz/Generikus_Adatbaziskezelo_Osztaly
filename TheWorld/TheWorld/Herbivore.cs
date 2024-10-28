using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    internal class Herbivore : Creature
    {
        public Herbivore(Cell startCell) : base(startCell)
        {
            Energy = 10;
            SightRange = 3;
        }
        public override void Move(Cell newCell) => CurrentCell = newCell;

        public override void Act(World world)
        {
            EatPlant();
            MoveRandomly(world);
        }
        public void EatPlant()
        {
            if (CurrentCell?.Plant != null)
            {
                CurrentCell.Plant.GetEaten();
                Energy += 5; // Növeli az energiát, amikor a növényt megeszi
            }
        }
    }
}
