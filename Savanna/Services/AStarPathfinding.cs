using System;
using System.Collections.Generic;
using System.Threading;
using Savanna.Constants;
using Savanna.Entities.Interfaces;

namespace Savanna.Services
{
    public class AStarPathfinding : IPathfinder
    {
        private IRenderer _renderer;
        private ISavannaFieldManager _savannaManager;

        public AStarPathfinding(IRenderer renderer, ISavannaFieldManager savannaManager)
        {
            _renderer = renderer;
            _savannaManager = savannaManager;
        }

        private List<ICellBase> openSet = new List<ICellBase>();
        private List<ICellBase> closedSet = new List<ICellBase>();
        private List<ICellBase> obstacleSet = new List<ICellBase>();
        private List<ICellBase> path = new List<ICellBase>();

        public List<ICellBase> MoveFromTo(ICellBase start, ICellBase end)
        {
            openSet.Add(start);

            while (true)
            {
                if (openSet.Count > 0)
                {
                    var winner = 0;
                    for (int i = 1; i < openSet.Count; i++)
                    {
                        if (openSet[i].sum < openSet[winner].sum)
                        {
                            winner = i;
                        }
                    }

                    var current = openSet[winner];
                    if (current.xPos == end.xPos && current.yPos == end.yPos)
                    {
                        ICellBase temp = current;
                        path.Add(current);
                        while (temp.cameFrom != null)
                        {
                            path.Add(temp.cameFrom);
                            temp = temp.cameFrom;
                        }
                        VisualizePath();
                        return path;
                    }

                    openSet.Remove(current);
                    closedSet.Add(current);

                    var neighbors = current.neighbors;
                    for (int i = 0; i < neighbors.Count; i++)
                    {
                        var neighbor = neighbors[i];

                        if (!closedSet.Contains(neighbor) && !neighbor.IsObstacle)
                        {
                            var tempG = current.distance + 1;
                            if (openSet.Contains(neighbor))
                            {
                                if (tempG < neighbor.distance)
                                {
                                    neighbor.distance = tempG;
                                }
                            }
                            else
                            {
                                neighbor.distance = tempG;
                                openSet.Add(neighbor);
                            }
                            neighbor.heuristic = Heuristic(neighbor, end);
                            neighbor.sum = neighbor.distance + neighbor.heuristic;
                            neighbor.cameFrom = current;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No Solution!");
                    return null;
                }
                //Thread.Sleep(50);
                //VisualizeOpenClosed();
            }
        }

        public void ClearOldData()
        {
            _savannaManager.ClearSavannaAStarData();
            _savannaManager.AddNeighbors();

            path.Clear();
            openSet.Clear();
            closedSet.Clear();
            obstacleSet.Clear();
        }

        private void VisualizeOpenClosed()
        {
            foreach (var open in openSet)
            {
                _renderer.DrawAtXyWithColor(open.xPos, open.yPos, ConsoleColor.Green);
            }
            foreach (var closed in closedSet)
            {
                _renderer.DrawAtXyWithColor(closed.xPos, closed.yPos, ConsoleColor.Red);
            }
        }

        private void VisualizePath()
        {
            foreach (var endPath in path)
            {
                _renderer.DrawAtXyWithColor(endPath.xPos, endPath.yPos, ConsoleColor.Magenta);
            }
            Thread.Sleep(Globals.PathDelay);
        }

        public double Heuristic(ICellBase current, ICellBase end)
        {
            return Math.Abs(current.xPos - end.xPos)
                + Math.Abs(current.yPos - end.yPos);
        }

        public double GetDistance(ICellBase current, ICellBase end)
        {
            return Math.Sqrt(Math.Pow((end.xPos - current.xPos), 2)
                + Math.Pow((end.yPos - current.yPos), 2));
        }
    }
}
