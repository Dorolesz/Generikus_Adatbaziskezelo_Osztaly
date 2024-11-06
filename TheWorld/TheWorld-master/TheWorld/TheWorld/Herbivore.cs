using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    internal class Herbivore : Creature
    {
        public int Size { get; set; }
        public Herbivore(int energy, int sightRange, Cell currentCell, int size) : base(energy, sightRange, currentCell)
        {
            Size = size;
        }

        public override void Move(Cell newCell)
        {
            if (newCell != null && newCell != CurrentCell)
            {
                CurrentCell.RemoveCreature(this);
                CurrentCell = newCell;
                newCell.AddCreature(this);
            }
        }

        public override void Eat(Creature other)
        {
            // Növényevők nem esznek más lényeket
        }

        public Cell FindBestCellToMove(World world)
        {
            List<Cell> neighbors = world.GetNeighbors(CurrentCell);
            Cell bestCell = null;
            int bestScore = int.MinValue;

            // Keresünk ragadozókat a látómezőn belül
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
                    }
                    else
                    {
                        visibleCells.Add(cell);
                    }
                }
            }

            // Megkeressük a legközelebbi ragadozót
            Cell predatorCell = visibleCells
                .Where(cell => cell.Inhabitants.OfType<Carnivore>().Any())
                .OrderBy(cell => Math.Abs(cell.X - CurrentCell.X) + Math.Abs(cell.Y - CurrentCell.Y))
                .FirstOrDefault() ?? new Cell(0, 0);

            foreach (var cell in neighbors)
            {
                if (cell.Inhabitants.OfType<Herbivore>().Any() || cell.Inhabitants.OfType<Carnivore>().Any())
                {
                    continue; // Nem léphet azonos lény vagy ragadozó cellájára
                }

                int score = 0;
                if (cell.Plant != null)
                {
                    score += 10; // Növények pozitív pontot adnak
                }

                if (predatorCell != null)
                {
                    // Távolodás a ragadozótól
                    int distanceToPredator = Math.Abs(cell.X - predatorCell.X) + Math.Abs(cell.Y - predatorCell.Y);
                    score += distanceToPredator; // Minél távolabb a ragadozótól, annál jobb
                }

                if (score > bestScore)
                {
                    bestScore = score;
                    bestCell = cell;
                }
            }

            return bestCell ?? neighbors.FirstOrDefault(cell => !cell.Inhabitants.OfType<Herbivore>().Any() && !cell.Inhabitants.OfType<Carnivore>().Any());
        }

        public void EatPlant()
        {
            if (CurrentCell?.Plant != null)
            {
                CurrentCell.Plant.GetEaten();
                Energy += 5;
            }
        }
    }
}
