using System;
using System.Collections.Generic;
using Savanna.Models;
using Savanna.Interfaces;

namespace Savanna.Abstract
{
    public abstract class AnimalBase : CellBase
    {
        public event EventHandler<AnimalEventArgs> AnimalBorned;
        public event EventHandler<AnimalEventArgs> AnimalMoved;

        readonly INotificator _notificator;

        protected ISavannaField _savanna;
        protected IAnimalData data;
        static int id = 0;

        //a*
        List<ICellBase> openSet;
        List<ICellBase> closedSet;
        List<ICellBase> path;
        ICellBase end;
        IRenderer _renderer;

        public AnimalBase(INotificator notificator, ISavannaField savanna, IRenderer renderer)
        {
            id++;
            data = new AnimalData() { ID = id };

            _renderer = renderer;
            _savanna = savanna;
            _notificator = notificator;

            path = new List<ICellBase>();
            openSet = new List<ICellBase>();
            openSet.Add(_savanna.Field[0, 0]);
            closedSet = new List<ICellBase>();
            end = _savanna.Field[21, 21];

            AnimalMoved += notificator.OnAnimalMoved;
            AnimalBorned += notificator.OnAnimalBorned;

            OnAnimalBorned(data);
        }

        public void Move()
        {
            MoveTo();
            
            OnAnimalMoved(data);
        }

        //A*
        public void MoveTo()
        {
            bool finished = false;

            while(!finished)
            {
                if (openSet.Count > 0)
                {
                    VisualizePaths();

                    var winner = 0;
                    for (int i = 1; i < openSet.Count; i++)
                    {
                        if (openSet[i].f < openSet[winner].f)
                        {
                            winner = i;
                        }
                    }

                    var current = openSet[winner];

                    if (current.x == end.x && current.y == end.y)
                    {
                        ICellBase temp = current;
                        path.Add(current);
                        while (temp.cameFrom != null)
                        {
                            path.Add(temp.cameFrom);
                            temp = temp.cameFrom;
                        }


                        VisualizePaths();
                        finished = true;
                        break;
                    }

                    openSet.Remove(current);
                    closedSet.Add(current);

                    var neighbors = current.neighbors;
                    for (int i = 0; i < neighbors.Count; i++)
                    {
                        var neighbor = neighbors[i];
                        if (!closedSet.Contains(neighbor))
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
                    }

                    VisualizePaths();
                }
                else
                {
                    Console.WriteLine("No Solution!");
                    finished = true;
                }
            }
        }
        //A*
        public void VisualizePaths()
        {
            foreach (var open in openSet)
            {
                _renderer.DrawAtXyWithColor(open.x, open.y, ConsoleColor.Green);
            }
            foreach (var closed in closedSet)
            {
                _renderer.DrawAtXyWithColor(closed.x, closed.y, ConsoleColor.Red);
            }
            foreach (var endPath in path)
            {
                _renderer.DrawAtXyWithColor(endPath.x, endPath.y, ConsoleColor.Magenta);
            }
        }

        //*A
        public double Heuristic(ICellBase neighbor, ICellBase end)
        {
            //return GetDistance(neighbor.x, neighbor.y, end.x, end.y); //Euclidean distance
            return Math.Abs(neighbor.x - end.x) + Math.Abs(neighbor.y - end.y);
        }

        //A*
        public double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        protected virtual void OnAnimalBorned(IAnimalData data)
        {
            AnimalBorned?.Invoke(this, new AnimalEventArgs() { AnimalData = data });
        }

        protected virtual void OnAnimalMoved(IAnimalData data)
        {
            AnimalMoved?.Invoke(this, new AnimalEventArgs() { AnimalData = data });
        }
    }
}
