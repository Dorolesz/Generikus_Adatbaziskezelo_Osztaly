using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    internal class Carnivore : Creature
    {
        public int Size { get; set; }

        public Carnivore(int energy, int sightRange, Cell currentCell, int size) : base(energy, sightRange, currentCell)
        {
            Size = size;
        }


        public override void Move(Cell newCell)
        {
            if (newCell == null && newCell != CurrentCell)
            {
                CurrentCell.RemoveCreature(this);
                CurrentCell = newCell;
                newCell.AddCreature(this);
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
            Cell bestCell = neighbors[0];
            if (bestCell == null)
            {
                bestCell = default(Cell);
            }

            // Keresünk növényevőket a látómezőn belül
            List<Cell> visibleCells = new List<Cell>();
            for (int dx = -SightRange; dx <= SightRange; dx++)
            {
                for (int dy = -SightRange; dy <= SightRange; dy++)
                {
                    if (dx == 0 && dy == 0) continue; // Ne vegyük figyelembe a jelenlegi cellát
                    Cell cell = world.GetCell(CurrentCell.X + dx, CurrentCell.Y + dy);
                    if (cell == null)
                    {
                        cell = default(Cell);
                    }else
                    {
                        visibleCells.Add(cell);
                    }
                }
            }

            // Megkeressük a legközelebbi növényevőt
            Cell targetCell = visibleCells
                .Where(cell => cell.Inhabitants.OfType<Herbivore>().Any())
                .OrderBy(cell => Math.Abs(cell.X - CurrentCell.X) + Math.Abs(cell.Y - CurrentCell.Y))
                .FirstOrDefault() ?? new Cell(0, 0);

            if (targetCell != null)
            {
                // Mozgás a cél felé
                int targetX = targetCell.X;
                int targetY = targetCell.Y;
                int bestDistance = int.MaxValue;

                foreach (var cell in neighbors)
                {
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
                // Ha nincs cél, akkor véletlenszerű mozgás
                bestCell = neighbors.FirstOrDefault();
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
    }
}
