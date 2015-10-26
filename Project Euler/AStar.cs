using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;


//Using http://www.redblobgames.com/pathfinding/a-star/implementation.html as a base
namespace Project_Euler
{
    class AStar
    {
        public interface WeightedGraph<L>
        {
            int Cost(Location a, Location b);
            IEnumerable<Location> Neighbors(Location id);
        }
        public class Location : PriorityQueueNode
        {
            public readonly int x, y;
            public Location(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public override bool Equals(object obj)
            {
                if (obj == null || !(obj is Location))
                    return false;
                Location l = (Location)obj;
                return this.x == l.x && this.y == l.y;
            }
            public static bool operator ==(Location a, Location b)
            {
                return a.Equals(b);
            }
            public static bool operator !=(Location a, Location b)
            {
                return !a.Equals(b);
            }

            public override int GetHashCode()
            {
                return x ^ y;
            }

            public override string ToString()
            {
                return x.ToString() + "," + y.ToString();
            }
        }



        public class TriangleGrid : WeightedGraph<Location>
        {
            public static readonly Location[] DIRS = new[]
            {
                new Location(0, 1),
                new Location(1, 1)
            };


            public static Location start { get; private set; } = new Location(0, 0);
            public static Location end { get; private set; } = new Location(int.MaxValue, int.MaxValue);
            public int width { get; private set; }
            public int height { get; private set;}
            private Dictionary<Location, int> costs = new Dictionary<Location, int>();

            public TriangleGrid(List<List<int>> nodes)
            {
                int lastRowSize = 0;
                for (int rowNum = 0; rowNum < nodes.Count(); rowNum++)
                {
                    var row = nodes[rowNum];
                    if (lastRowSize >= row.Count())
                        throw new ArgumentException("nodes must be triangular");
                    AddRow(row, rowNum);
                }
                width = lastRowSize;
                height = nodes.Count();
            }

            private void AddRow(List<int> row, int rowNum)
            {
                for (int colNum = 0; colNum < row.Count(); colNum++)
                {
                    int cost = row[colNum];
                    costs.Add(new Location(colNum, rowNum), cost);
                }
            }

            public bool InBounds(Location id)
            {
                return 0 <= id.x && id.x <= id.y
                    && 0 <= id.y && id.y < height;
            }

            public int Cost(Location a, Location b)
            {
                return costs.ContainsKey(a) ? costs[a] : 0;
            }

            public IEnumerable<Location> Neighbors(Location id)
            {
                foreach (var dir in DIRS)
                {
                    Location next = new Location(id.x + dir.x, id.y + dir.y);
                    if (InBounds(next))
                    {
                        yield return next;
                    } else
                    {
                        yield return end;
                    }
                }
            }
        }

        public class AStarSearch
        {
            public Dictionary<Location, Location> cameFrom
                = new Dictionary<Location, Location>();
            public Dictionary<Location, int> costSoFar
                = new Dictionary<Location, int>();

            // Note: a generic version of A* would abstract over Location and
            // also Heuristic
            static public int Heuristic(Location a, Location b)
            {
                return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
            }

            public AStarSearch(WeightedGraph<Location> graph, Location start, Location goal, int heapsize)
            {
                var frontier = new HeapPriorityQueue<Location>(heapsize);
                frontier.Enqueue(start, 0);

                cameFrom[start] = start;
                costSoFar[start] = 0;

                while (frontier.Count > 0)
                {
                    var current = frontier.Dequeue();

                    if (current.Equals(goal))
                    {
                        break;
                    }

                    foreach (var next in graph.Neighbors(current))
                    {
                        int newCost = costSoFar[current]
                            + graph.Cost(current, next);
                        if (!costSoFar.ContainsKey(next)
                            || newCost < costSoFar[next])
                        {
                            costSoFar[next] = newCost;
                            int priority = newCost + Heuristic(next, goal);
                            frontier.Enqueue(next, priority);
                            cameFrom[next] = current;
                        }
                    }
                }
            }
        }
    }
}
