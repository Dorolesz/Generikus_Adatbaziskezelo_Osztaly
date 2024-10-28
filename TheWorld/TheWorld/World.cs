using System;
using System.Collections.Generic;
using System.Linq;
using TheWorld;

internal class World
{
    private int Width { get; }
    private int Height { get; }
    private Cell[,] Cells { get; }

    public World(int width, int height)
    {
        this.Width = width;
        this.Height = height;
        this.Cells = new Cell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cells[x, y] = new Cell(x, y);
            }
        }
    }

    public Cell GetCell(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            return Cells[x, y];
        }
        return null;
    }

    public void AddCreature(Creature creature, int x, int y)
    {
        Cell cell = GetCell(x, y);
        cell?.AddCreature(creature);
        creature.CurrentCell = cell;
    }

    public void AddPlant(Plant plant, int x, int y)
    {
        Cell cell = GetCell(x, y);
        cell?.AddPlant(plant);
        plant.CurrentCell = cell;
    }

    public List<Cell> GetNeighbors(Cell cell)
    {
        var neighbors = new List<Cell>();
        int[] dx = { -1, 0, 1, 0 }; // Bal, Fel, Jobb, Lefel
        int[] dy = { 0, 1, 0, -1 };

        for (int i = 0; i < 4; i++)
        {
            Cell neighbor = GetCell(cell.X + dx[i], cell.Y + dy[i]);
            if (neighbor != null)
            {
                neighbors.Add(neighbor);
            }
        }
        return neighbors; // Visszaadja a szomszédos cellákat
    }


    public void Update()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var cell = Cells[x, y];
                cell.Plant?.Grow();
                if (cell.Plant?.IsFullyGrown == true)
                    cell.Plant.Spread(this);
            }
        }

        foreach (var cell in Cells)
        {
            foreach (var creature in cell.Inhabitants.ToArray())
            {
                creature.Act(this);
                if (!creature.IsAlive())
                {
                    cell.RemoveCreature(creature);
                }
            }
        }
    }
    public void Draw()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Console.Write(Cells[x, y]); // Kiíratja a cellák állapotát
            }
            Console.WriteLine(); // Új sor a következő y pozícióhoz
        }
    }

    public void Display()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Cell cell = Cells[x, y];
                if (cell.Plant != null)
                {
                    Console.Write("P");  // Növény
                }
                else if (cell.Inhabitants.Any(c => c is Carnivore))
                {
                    Console.Write("C");  // Ragadozó
                }
                else if (cell.Inhabitants.Any(c => c is Herbivore))
                {
                    Console.Write("H");  // Növényevő
                }
                else
                {
                    Console.Write(".");  // Üres cella
                }
            }
            Console.WriteLine();
        }
    }

    internal object GetNeighbors(object curentCell)
    {
        throw new NotImplementedException();
    }
}