using System;
using System.Collections.Generic;
using System.Linq;
using TheWorld;

internal class World
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Cell[,] Cells { get; private set; }

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
        if (cell == null)
        {
            throw new ArgumentNullException(nameof(cell), "A cell objektum nem lehet null.");
        }

        List<Cell> neighbors = new List<Cell>();
        int[] dx = { -1, 1, 0, 0, -1, 1, -1, 1 };  // Például mozgási irányok (bal, jobb, fent, lent, stb.)
        int[] dy = { 0, 0, -1, 1, -1, -1, 1, 1 };

        for (int i = 0; i < 8; i++)
        {
            int newX = cell.X + dx[i];
            int newY = cell.Y + dy[i];

            if (IsValidCoordinate(newX, newY)) ;
            {
                Cell neighbor = GetCell(newX, newY);
                if (neighbor != null)
                {
                    neighbors.Add(neighbor);
                }
            }
        }
        return neighbors;
    }

    public bool IsValidCoordinate(int x, int y)
    {
        return (x >= 0 && x < Width && y >= 0 && y < Height);
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
    }
    public void Draw()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Console.Write(Cells[x, y]);
            }
            Console.WriteLine();
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
}