﻿using Savanna.Fauna;
using Savanna.Interfaces;
using Savanna.Rendering;
using System;
using System.Collections.Generic;

namespace Savanna.Services
{
    public class AStarPathfinding : IPathfinder
    {
        #region Singleton
        private static readonly Lazy<AStarPathfinding> lazy =
                            new Lazy<AStarPathfinding>(() => new AStarPathfinding());
        public string Name { get; private set; }

        private AStarPathfinding()
        {
            Name = Guid.NewGuid().ToString();
        }

        public static AStarPathfinding GetInstance()
        {
            return lazy.Value;
        }
        #endregion

        List<ICellBase> openSet = new List<ICellBase>();
        List<ICellBase> closedSet = new List<ICellBase>();
        List<ICellBase> obstacleSet = new List<ICellBase>();
        List<ICellBase> path = new List<ICellBase>();
        bool finished;

        IRenderer _renderer = ConsoleRenderer.GetInstance();

        public void MoveFromTo(ICellBase start, ICellBase end)
        {
            ClearOldData();
            openSet.Add(start);

            while (!finished)
            {
                if (openSet.Count > 0)
                {
                    var winner = 0;
                    for (int i = 1; i < openSet.Count; i++)
                    {
                        if (openSet[i].f < openSet[winner].f)
                        {
                            winner = i;
                        }
                    }

                    var current = openSet[winner];
                    if (current._x == end._x && current._y == end._y)
                    {
                        ICellBase temp = current;
                        path.Add(current);
                        while (temp.cameFrom != null)
                        {
                            path.Add(temp.cameFrom);
                            temp = temp.cameFrom;
                        }
                        VisualizePath();
                        VisualizeObstacles();
                        finished = true;
                        break;
                    }

                    openSet.Remove(current);
                    closedSet.Add(current);

                    var neighbors = current.neighbors;
                    for (int i = 0; i < neighbors.Count; i++)
                    {
                        var neighbor = neighbors[i];

                        if (!closedSet.Contains(neighbor) && !neighbor.IsObstacle)
                        {
                            var tempG = current.g + 1;
                            if (openSet.Contains(neighbor))
                            {
                                if (tempG < neighbor.g)
                                {
                                    neighbor.g = tempG;
                                }
                            }
                            else
                            {
                                neighbor.g = tempG;
                                openSet.Add(neighbor);
                            }
                            neighbor.h = Heuristic(neighbor, end);
                            neighbor.f = neighbor.g + neighbor.h;
                            neighbor.cameFrom = current;
                        }
                        if(neighbor.IsObstacle)
                            obstacleSet.Add(neighbor);
                    }
                }
                else
                {
                    Console.WriteLine("No Solution!");
                    finished = true;
                }
                VisualizeOpenClosed();
            }
        }

        private void ClearOldData()
        {
            finished = false;
            openSet.Clear();
            closedSet.Clear();
            obstacleSet.Clear();
            path.Clear();
        }

        private void VisualizeOpenClosed()
        {
            foreach (var open in openSet)
            {
               _renderer.DrawAtXyWithColor(open._x, open._y, ConsoleColor.Green);
            }
            foreach (var closed in closedSet)
            {
               _renderer.DrawAtXyWithColor(closed._x, closed._y, ConsoleColor.Red);
            }
        }

        private void VisualizeObstacles()
        {
            foreach (var obstacle in obstacleSet)
            {
                _renderer.DrawAtXyWithColor(obstacle._x, obstacle._y, ConsoleColor.Yellow);
            }
        }

        private void VisualizePath()
        {
            foreach (var endPath in path)
            {
                _renderer.DrawAtXyWithColor(endPath._x, endPath._y, ConsoleColor.Magenta);
            }
        }

        private double Heuristic(ICellBase neighbor, ICellBase end)
        {
            return Math.Abs(neighbor._x - end._x) + Math.Abs(neighbor._y - end._y); //Manhattan Distance
            //return GetDistance(neighbor.x, neighbor.y, end.x, end.y); //Euclidean distance
        }

        private double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }
    }
}