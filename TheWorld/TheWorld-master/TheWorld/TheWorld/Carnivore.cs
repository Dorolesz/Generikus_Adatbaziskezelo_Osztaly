using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    public class Carnivore : Creature
    {
        public int Size { get; set; }

        public Carnivore(int energy, int sightRange, Cell currentCell, int size) : base(energy, sightRange, currentCell)
        {
            Size = size;
        }


        public override void Move(Cell newCell)
        {
            if (newCell != null && newCell != CurrentCell && !newCell.Inhabitants.OfType<Carnivore>().Any())
            {
                CurrentCell.RemoveCreature(this);
                newCell.AddCreature(this);
                CurrentCell = newCell;

                var prey = newCell.Inhabitants.OfType<Herbivore>().FirstOrDefault();
                if (prey != null)
                {
                    EatCreature(prey);
                }
            }
        }

        public override void Eat(Creature other)
        {
            if (other != null)
            {
                Energy += other.Energy;
                other.CurrentCell.RemoveCreature(other);
            }
        }

        public Cell FindBestCellToMove(World world)
        {
            List<Cell> neighbors = world.GetNeighbors(CurrentCell);
            Cell bestCell = null;

            List<Cell> visibleCells = new List<Cell>();
            for (int dx = -SightRange; dx <= SightRange; dx++)
            {
                for (int dy = -SightRange; dy <= SightRange; dy++)
                {
                    if (dx == 0 && dy == 0) continue;
                    Cell cell = world.GetCell(CurrentCell.X + dx, CurrentCell.Y + dy);
                    if (cell != null)
                    {
                        visibleCells.Add(cell);
                    }
                }
            }

            Cell targetCell = visibleCells
                .Where(cell => cell.Inhabitants.OfType<Herbivore>().Any())
                .OrderBy(cell => Math.Abs(cell.X - CurrentCell.X) + Math.Abs(cell.Y - CurrentCell.Y))
                .FirstOrDefault();

            if (targetCell != null)
            {
                int targetX = targetCell.X;
                int targetY = targetCell.Y;
                int bestDistance = int.MaxValue;

                foreach (var cell in neighbors)
                {
                    if (cell.Plant != null || cell.Inhabitants.OfType<Carnivore>().Any()) continue;

                    int distance = Math.Abs(cell.X - targetX) + Math.Abs(cell.Y - targetY);
                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        bestCell = cell;
                    }
                }
            }
            else
            {
                bestCell = neighbors.FirstOrDefault(cell => cell.Plant == null && !cell.Inhabitants.OfType<Carnivore>().Any());
            }

            return bestCell;
        }



        public void EatCreature(Creature other)
        {
            if (other != null)
            {
                Energy += other.Energy; // Növeli az energiaszintet
                other.CurrentCell.RemoveCreature(other); // Eltávolítjuk a prédát a cellából
            }
        }

        public void Reproduce(World world)
        {
            if (Energy >= 40)
            {
                List<Cell> neighbors = world.GetNeighbors(CurrentCell);
                foreach (var cell in neighbors)
                {
                    if (!cell.Inhabitants.Any())
                    {
                        Carnivore offspring = new Carnivore(15, SightRange, cell, Size);
                        world.AddCreature(offspring, cell.X, cell.Y);
                        Energy -= 20;
                        break;
                    }
                }
            }
        }
    }
}
