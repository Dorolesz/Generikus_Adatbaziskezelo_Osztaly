using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    internal class Cell
    {
        public int X { get; }
        public int Y { get; }
        public char Symbol { get; set; } // Jelöli a cella állapotát (pl. üres, növény, állat)

        public List<Creature> Inhabitants { get; }
        public Plant Plant { get; private set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            Symbol = '.';
            Inhabitants = new List<Creature>();
        }

        public void AddCreature(Creature creature)
        {
            Inhabitants.Add(creature);
        }

        public void RemoveCreature(Creature creature)
        {
            Inhabitants.Remove(creature);
        }

        public void AddPlant(Plant plant)
        {
            Plant = plant;
        }
    }
}
