﻿using System;
using System.Collections.Generic;
using System.Linq;
using TheWorld;

public class World
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

    public void RemoveCreature(Creature creature)
    {
        foreach (var cell in Cells)
        {
            if (cell.Inhabitants.Contains(creature))
            {
                cell.RemoveCreature(creature);
                break;
            }
        }
    }


    public List<Cell> GetNeighbors(Cell cell)
    {

        List<Cell> neighbors = new List<Cell>();
        int[] dx = { -1, 1, 0, 0, -1, 1, -1, 1 };  // Például mozgási irányok (bal, jobb, fent, lent, stb.)
        int[] dy = { 0, 0, -1, 1, -1, -1, 1, 1 };

        for (int i = 0; i < 8; i++)
        {
            int newX = cell.X + dx[i];
            int newY = cell.Y + dy[i];

            if (IsValidCoordinate(newX, newY))
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
        List<Herbivore> herbivoresToMove = new List<Herbivore>();
        List<Carnivore> carnivoresToMove = new List<Carnivore>();
        List<(Plant, Cell)> plantsToGrow = new List<(Plant, Cell)>();

        foreach (var cell in Cells)
        {
            herbivoresToMove.AddRange(cell.Inhabitants.OfType<Herbivore>());
            carnivoresToMove.AddRange(cell.Inhabitants.OfType<Carnivore>());
            if (cell.Plant != null)
            {
                plantsToGrow.Add((cell.Plant, cell));
            }
        }

        Dictionary<Cell, List<Creature>> targetCells = new Dictionary<Cell, List<Creature>>();

        foreach (var herbivore in herbivoresToMove)
        {
            Cell bestCell = herbivore.FindBestCellToMove(this);
            if (bestCell != null && bestCell != herbivore.CurrentCell)
            {
                if (!targetCells.ContainsKey(bestCell))
                {
                    targetCells[bestCell] = new List<Creature>();
                }
                targetCells[bestCell].Add(herbivore);
            }
        }

        foreach (var carnivore in carnivoresToMove)
        {
            Cell bestCell = carnivore.FindBestCellToMove(this);
            if (bestCell != null && bestCell != carnivore.CurrentCell)
            {
                if (!targetCells.ContainsKey(bestCell))
                {
                    targetCells[bestCell] = new List<Creature>();
                }
                targetCells[bestCell].Add(carnivore);
            }
        }

        foreach (var targetCell in targetCells)
        {
            var creatures = targetCell.Value;
            if (creatures.Count > 1)
            {
                var firstCreature = creatures.First();
                firstCreature.Move(targetCell.Key);
                creatures.Remove(firstCreature);

                foreach (var creature in creatures)
                {
                    var neighbors = GetNeighbors(targetCell.Key).Where(cell => !cell.Inhabitants.Any()).ToList();
                    if (neighbors.Any())
                    {
                        creature.Move(neighbors.First());
                    }
                }
            }
            else
            {
                creatures.First().Move(targetCell.Key);
            }
        }

        foreach (var herbivore in herbivoresToMove)
        {
            herbivore.EatPlant();
            herbivore.Reproduce(this);
        }

        foreach (var carnivore in carnivoresToMove)
        {
            carnivore.Reproduce(this);
        }

        foreach (var (plant, cell) in plantsToGrow)
        {
            plant.Grow();
            plant.Spread(this, cell);
        }
    }

    public void Display()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Cell cell = Cells[x, y];
                if (cell.Inhabitants.OfType<Carnivore>().Any())
                {
                    Console.Write("C ");
                }
                else if (cell.Inhabitants.OfType<Herbivore>().Any())
                {
                    Console.Write("H ");
                }
                else if (cell.Plant != null)
                {
                    Console.Write("P ");
                }
                else
                {
                    Console.Write(". ");
                }
            }
            Console.WriteLine();
        }
    }
}