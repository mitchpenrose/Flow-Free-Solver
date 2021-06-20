using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlowFreeSolver
{
    class Program
    {
        static int n;
        static List<Tuple<int, int>> visited = new List<Tuple<int, int>>();
        static char[,] original;
        static bool showPrintOut;
        static List<char> flows = new List<char>();
        static void Main(string[] args)
        {
            //string p =
            //    "- - - - - \n" +
            //    "- - - - - \n" +
            //    "- - - - - \n" +
            //    "- - - - - \n" +
            //    "- - - - - ";



            //string p =
            //    "B - - R O \n" +
            //    "- - - Y - \n" +
            //    "- - Y - - \n" +
            //    "- R O - G \n" +
            //    "- B G - - ";

            //string p =
            //    "- - - R G \n" +
            //    "- Y B G - \n" +
            //    "- - - - - \n" +
            //    "- - - - B \n" +
            //    "- - R - Y ";

            //string p =
            //    "- Y B G - \n" +
            //    "- - - R - \n" +
            //    "- - R - - \n" +
            //    "Y - - O - \n" +
            //    "B - O G - ";

            //string p =
            //    "B - - - - -\n" +
            //    "- - - - - -\n" +
            //    "- - - - Y -\n" +
            //    "- - Y G R -\n" +
            //    "- G - R - -\n" +
            //    "- - - B - -";

            string p =
                "B R - - - - - \n" +
                "- - - - - G - \n" +
                "- - - Y - - - \n" +
                "- - - - - - - \n" +
                "- - - - - - - \n" +
                "Y - - R - - - \n" +
                "B - - - - - G";



            //string p =
            //    "G B - - - R - -\n" +
            //    "- - - G - B T -\n" +
            //    "O - - - - P - -\n" +
            //    "- Y - - - - - -\n" +
            //    "- P O - - R - -\n" +
            //    "- - - - - - Y -\n" +
            //    "- Z - - Z - T -\n" +
            //    "- - - - - - - -";

            //string p =
            //"- - - - O P O -\n" +
            //"- - - - R - T -\n" +
            //"- - G - - - - -\n" +
            //"- - - - - P T -\n" +
            //"- - G B - - - -\n" +
            //"- - - - - Y - -\n" +
            //"- B Y R - - - -\n" +
            //"- - - - - - - -";

            //string p =
            //"- - - - - - - -\n" +
            //"- - - - - G T -\n" +
            //"- - - - - - - Y\n" +
            //"- - - - - - - -\n" +
            //"- - - R - - T R\n" +
            //"- - - - - - O B\n" +
            //"- - - - G - B -\n" +
            //"- - - - Y - - O";

            //string p =
            //"- - - - - - - - - - -\n" +
            //"- - Y B - - - - - - -\n" +
            //"- - - - - - - - R - -\n" +
            //"- - - - - - - - B - -\n" +
            //"- - - - - P - - G T -\n" +
            //"- - - - - R - - - - -\n" +
            //"- - - - - O - - - - -\n" +
            //"- - - - - - - - - - -\n" +
            //"- - G - - - - - P - -\n" +
            //"- - - Y T O - - - - -\n" +
            //"- - - - - - - - - - -";

            //string p =
            //"- - - - - - - - - - -\n" +
            //"- - - - - - - - - Y -\n" +
            //"- - - - - - - - - - -\n" +
            //"- - - P - - - - - R -\n" +
            //"- - - - - - - - - - -\n" +
            //"- - - - - - B - - O -\n" +
            //"- - - - G T - - - - -\n" +
            //"- - - - - P O - - - -\n" +
            //"- - - - R Y - - B - -\n" +
            //"- - - - - - G - T - -\n" +
            //"- - - - - - - - - - -";

            //string p =
            //"- - - - - - - - - - -\n" +
            //"- - - R - - - - - G -\n" +
            //"- - - O - - - - - - -\n" +
            //"- - - Y - - - - - - -\n" +
            //"- - - - - - G T R Y -\n" +
            //"- - - - - T B - - - -\n" +
            //"- - - - - - - - O - -\n" +
            //"- - - - - - - - - - B\n" +
            //"- - - - - - - - - - -\n" +
            //"- - - - - - - - - - -\n" +
            //"- - - - - - - - - - -";

            //string p =
            //"- - - - - - - - - - -\n" +
            //"- G - - - - - - - T -\n" +
            //"- - - Y - - - - - - -\n" +
            //"- - - - - - - - - - -\n" +
            //"- - - - - - - - - - -\n" +
            //"R O T - - - - - - - -\n" +
            //"- - - - - - B R - - -\n" +
            //"- - - - - - - Y - - -\n" +
            //"- - G - - B O - - - -\n" +
            //"- - - - - - - - - - -\n" +
            //"- - - - - - - - - - -";


            //string p =
            //"- - - - - - - - - - -\n" +
            //"- - - - - - - - - T -\n" +
            //"- - - - - - - R - - -\n" +
            //"- P T G - - - - - - -\n" +
            //"- R Y - - - - - - - -\n" +
            //"- - - - - - - - - - -\n" +
            //"- P - - - - - B - - -\n" +
            //"- - - - - - - Y - - -\n" +
            //"- - - - G - - - - - -\n" +
            //"- - O B - - - - - - -\n" +
            //"O - - - - - - - - - -";

            //string p =
            //"- - - - - - - - - - -\n" +
            //"- - - - - - G - - Y -\n" +
            //"- - - - - - - - - - -\n" +
            //"- - T P - - - - - - -\n" +
            //"- - B - - - - O - - -\n" +
            //"- - - - - R - - - P -\n" +
            //"- - - - - Y - R - - -\n" +
            //"- - - O - - - - - B -\n" +
            //"- - - - - G - - - - -\n" +
            //"- - - - - - - - - - -\n" +
            //"- - T - - - - - - - -";

            //string p =
            //"- - - - - - - - - - - - - -\n" +
            //"- - - - - - - - - - - - - -\n" +
            //"A - - - B - - - C - - - C D\n" +
            //"H - - G - - - - - - F - E -\n" +
            //"B - - - - I - - - - - - D -\n" +
            //"I - - - H - - - K - - J - -\n" +
            //"A - - - - - - - - - - - E J\n" +
            //"- - G - - - - - - - - - - -\n" +
            //"- - - - - - - - - L - - - -\n" +
            //"- - - - - - N M - - - - - -\n" +
            //"- - - - O - - - - - - - - -\n" +
            //"- M - - - - - - - - N - - -\n" +
            //"- - L - - - - - - - - - - -\n" +
            //"O - - - K - - - - - - - - F";


            showPrintOut = true;
            n = (int)Math.Sqrt(p.Replace(" ", "").Replace("\n", "").Length);
            char[,] puzzle = new char[n,n];
            int i = 0, j = 0;
            foreach(char c in p.ToCharArray())
            {
                if(c != ' ' && c != '\n')
                {
                    puzzle[i,j] = c;
                    j++;
                    if(j == n)
                    {
                        i++;
                        j = 0;
                    }
                }
            }

            new Paths(n);
            

            //////////////////MULTITHREADED//////////////////////////
            List<Tuple<int, int>> colorPositions = findColorPositions(puzzle);
            List<char[,]> possibleSolves = new List<char[,]>();
            List<Thread> threads = new List<Thread>();
            for (int z = 0; z < colorPositions.Count; z++)
            {
                threads.Add(new Thread(() => possibleSolves.Add(new Solve().solvePuzzle((char[,])puzzle.Clone(), (Tuple<int, int>[])colorPositions.ToArray().Clone(), z))));
                threads[z].Start();
                Thread.Sleep(250);
                //thr.Start();
            }

            char[,] solved;
            while (possibleSolves.Count == 0)
            {
                Thread.Sleep(250);
            }
            foreach (Thread t in threads)
                t.Abort();

            solved = possibleSolves[0];
            //////////////////MULTITHREADED//////////////////////////

            //////////////////SINGLETHREADED//////////////////////////
            //char[,] solved = solvePuzzle(puzzle);//Regular Solve
            //////////////////SINGLETHREADED//////////////////////////


            if (solved != null)
            {
                Console.WriteLine("SOLVED");
                printPuzzle(solved);
            }
            else
            {
                Console.WriteLine("CANNOT SOLVE");
            }
            Console.ReadLine();
        }

        static char[,] solvePuzzle(char[,] puzzle)
        {
            original = (char[,])puzzle.Clone();
            Tuple<int, int> pos = findNextColorPosition(puzzle);
            visited.Add(Tuple.Create(pos.Item1, pos.Item2));
            char[,] p1 = solve(puzzle, pos.Item1-1, pos.Item2, puzzle[pos.Item1, pos.Item2]);
            if (p1 == null)
            {
                char[,] p2 = solve(puzzle, pos.Item1 + 1, pos.Item2, puzzle[pos.Item1, pos.Item2]);
                if (p2 == null)
                {
                    char[,] p3 = solve(puzzle, pos.Item1, pos.Item2 - 1, puzzle[pos.Item1, pos.Item2]);
                    if (p3 == null)
                    {
                        char[,] p4 = solve(puzzle, pos.Item1, pos.Item2 + 1, puzzle[pos.Item1, pos.Item2]);
                        if (p4 == null)
                        {
                            return null;
                        }
                        else
                            return p4;
                    }
                    else
                        return p3;
                }
                else
                    return p2;
            }
            else
                return p1;
            
            
            
        }


        static List<Tuple<int, int>> findColorPositions(char[,] puzzle)
        {
            List<Tuple<int, int>> colorPositions = new List<Tuple<int, int>>();

            char direction = 'r';

            int k = n;

            int x = 0, y = 0;

            while (true)
            {
                for (int i = 0; i < k; i++)
                {
                    if (direction == 'r')
                    {
                        if (puzzle[y, x] != '-' && isSingleColor(puzzle, y, x))
                            colorPositions.Add(Tuple.Create(y, x));
                        x++;
                    }
                    else if (direction == 'd')
                    {
                        if (puzzle[y, x] != '-' && isSingleColor(puzzle, y, x))
                            colorPositions.Add(Tuple.Create(y, x));
                        y++;
                    }
                    else if (direction == 'l')
                    {
                        if (puzzle[y, x] != '-' && isSingleColor(puzzle, y, x))
                            colorPositions.Add(Tuple.Create(y, x));
                        x--;
                    }
                    else if (direction == 'u')
                    {
                        if (puzzle[y, x] != '-' && isSingleColor(puzzle, y, x))
                            colorPositions.Add(Tuple.Create(y, x));
                        y--;
                    }
                }
                if (k == 0)
                    break;

                if (direction == 'r')
                {
                    y++;
                    x--;
                    direction = 'd';
                }
                else if (direction == 'd')
                {
                    x--;
                    y--;
                    direction = 'l';
                }
                else if (direction == 'l')
                {
                    y--;
                    x++;
                    direction = 'u';
                }
                else if (direction == 'u')
                {
                    x++;
                    y++;
                    direction = 'r';
                }

                if (direction == 'd')
                    k--;
                else if (direction == 'u')
                    k--;
            }

            return colorPositions;
        }


        static bool isPositionEdgeOrColor(int y, int x, char current)
        {
            return (y == 0 || y == n - 1 || x == 0 || x == n - 1 || current != '-');
        }

        static char[,] solve(char[,] puzzle, int y, int x, char color)
        {
            //if (showPrintOut)
            //{
            //    //Console.WriteLine("x: " + x + " y: " + y);
            //    printPuzzle(puzzle);
            //}

            //if (x == 5 && y == 4)
            //    ;

            if (y == n || x == n || y < 0 || x < 0)
                return null;

            if (checkInvalidMove(puzzle))
                return null;

            if (isPositionEdgeOrColor(y, x, puzzle[y, x]))
            {
                if (checkFailFloodFill(puzzle, color))
                    return null;
            }

            if (puzzleSolved(puzzle))//The puzzle has been solved return it.
                return puzzle;

            //removeVisitedForSingleLetters(puzzle, y, x);
            if (containsVisited(y, x))
                return null;
            try
            {
                if (puzzle[y, x] == '-')
                {
                    puzzle[y, x] = color;
                    visited.Add(Tuple.Create(y, x));
                }
                else if (puzzle[y, x] == color && !containsVisited(y, x))//Flow found. Find another.
                {
                    if (showPrintOut)
                    {
                        //Console.WriteLine("x: " + x + " y: " + y);
                        printPuzzle(puzzle);
                    }
                    //if (checkFailFloodFill(puzzle, color))
                    //    return null;
                    //visited.Add(Tuple.Create(y, x));
                    Tuple<int, int> nextPoint = findNextColorPosition(puzzle);
                    y = nextPoint.Item1;
                    x = nextPoint.Item2;
                    color = puzzle[y, x];
                    visited.Add(Tuple.Create(y, x));
                }
                else
                    return null;
                //else if(puzzle[y, x] != '-' && puzzle[y,x] != color)
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            } 

            //if (puzzleSolved(puzzle))//The puzzle has been solved return it.
            //    return puzzle;
            char[,] result1;
            char[,] result2;
            char[,] result3;
            char[,] result4;
            result1 = solve(puzzle, y - 1, x, color);//try moving up.
            if (result1 != null)
                return result1;
            result2 = solve(puzzle, y + 1, x, color);//try moving down.
            if (result2 != null)
                return result2;
            result3 = solve(puzzle, y, x - 1, color);//try moving left.
            if (result3 != null)
                return result3;
            result4 = solve(puzzle, y, x + 1, color);//try moving right.
            if (result4 != null)
                return result4;

            if (original[y, x] == '-')
            {
                puzzle[y, x] = '-';
                removeVisited(y, x);
            }
            return null;
        }

        static bool puzzleSolved(char[,] puzzle)
        {
            Dictionary<char, int> colorCounts = new Dictionary<char, int>();
            for (int y = 0; y < n; y++)
                for (int x = 0; x < n; x++)
                {
                    if (isSingleColor(puzzle, y, x))
                        return false;
                    if (puzzle[y, x] == '-')
                        return false;
                    else
                    {
                        if (!colorCounts.ContainsKey(puzzle[y, x]))
                            colorCounts.Add(puzzle[y, x], 1);
                        else
                            colorCounts[puzzle[y, x]]++;
                    }
                }
            foreach (int i in colorCounts.Values)
                if (i == 2)
                    return false;

            return true;
        }

        static bool containsVisited(int y, int x)
        {
            foreach (Tuple<int, int> t in visited)
                if (t.Item1 == y && t.Item2 == x)
                    return true;
            return false;
        }

        static void removeVisited(int y, int x)
        {
            for (int i = 0; i < visited.Count; i++)
                if (visited[i].Item1 == y && visited[i].Item2 == x)
                {
                    visited.RemoveAt(i);
                    break;
                }
        }

        static Tuple<int, int> findNextColorPosition(char[,] puzzle)
        {
            char direction = 'r';

            int k = n;

            int x = 0, y = 0;

            while (true)
            {
                for (int i = 0; i < k; i++)
                {
                    if (direction == 'r')
                    {
                        if (puzzle[y, x] != '-' && isSingleColor(puzzle, y, x))
                            return Tuple.Create(y, x);
                        x++;
                    }
                    else if (direction == 'd')
                    {
                        if (puzzle[y, x] != '-' && isSingleColor(puzzle, y, x))
                            return Tuple.Create(y, x);
                        y++;
                    }
                    else if (direction == 'l')
                    {
                        if (puzzle[y, x] != '-' && isSingleColor(puzzle, y, x))
                            return Tuple.Create(y, x);
                        x--;
                    }
                    else if (direction == 'u')
                    {
                        if (puzzle[y, x] != '-' && isSingleColor(puzzle, y, x))
                            return Tuple.Create(y, x);
                        y--;
                    }
                }
                if (k == 0)
                    break;

                if (direction == 'r')
                {
                    y++;
                    x--;
                    direction = 'd';
                }
                else if (direction == 'd')
                {
                    x--;
                    y--;
                    direction = 'l';
                }
                else if (direction == 'l')
                {
                    y--;
                    x++;
                    direction = 'u';
                }
                else if (direction == 'u')
                {
                    x++;
                    y++;
                    direction = 'r';
                }

                if (direction == 'd')
                    k--;
                else if (direction == 'u')
                    k--;
            }

            return null;
        }


        //static Tuple<int, int> findNextColorPosition(char[,] puzzle)
        //{
        //    for (int y = 0; y < n; y++)
        //        for (int x = 0; x < n; x++)
        //            if (puzzle[y, x] != '-' && isSingleColor(puzzle, y, x))
        //                return Tuple.Create(y, x);
        //    return null;
        //}

        static bool isSingleColor(char[,] puzzle, int y, int x)
        {
            char color = puzzle[y, x];
            bool up = false;
            bool down = false;
            bool left = false;
            bool right = false;
            try { up = puzzle[y - 1, x] == color; } catch (IndexOutOfRangeException) { }
            try { down = puzzle[y + 1, x] == color; } catch (IndexOutOfRangeException) { }
            try { left = puzzle[y, x - 1] == color; } catch (IndexOutOfRangeException) { }
            try { right = puzzle[y, x + 1] == color; } catch (IndexOutOfRangeException) { }

            return (!up && !down && !left && !right);
        }

        

        static bool checkFailFloodFill(char[,] p, char color)
        {
            char[,] puzzle = (char[,])p.Clone();
            //Queue<Tuple<int, int>> floodQ = new Queue<Tuple<int, int>>();
            //bool breakSet = false;
            //for (int y = 0; y < n; y++)
            //{
            //    if (breakSet)
            //        break;
            //    for (int x = 0; x < n; x++)
            //    {
            //        if (puzzle[y, x] == '-')
            //        {
            //            floodQ.Enqueue(Tuple.Create(y, x));
            //            breakSet = true;
            //            break;                       
            //        }
            //    }

            //}

            //Tuple<int, int> current;
            //while(floodQ.Count != 0)
            //{
            //    current = floodQ.Dequeue();
            //    List<Tuple<int,int>> neighbors = getAllNeighbors(current, puzzle, color);
            //    puzzle[current.Item1, current.Item2] = 'X';
            //    //printPuzzle(puzzle);
            //    foreach (Tuple<int, int> n in neighbors)
            //        if (!floodQ.Contains(n))
            //            floodQ.Enqueue(n);
            //}

            //printPuzzle(puzzle);
            //List<bool> twoPerColorInEachSection = new List<bool>();
            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    if (puzzle[y, x] == '-')
                        if (!isTwoInSection(puzzle, color, y, x))//twoPerColorInEachSection.Add(isTwoInSection(puzzle, color, y, x));
                            return true;
                }
            }

            //foreach (bool res in twoPerColorInEachSection)
            //    if (!res)
            //        return true;

            return false;
        }

        static bool isTwoInSection(char[,] puzzle, char color, int y, int x)
        {
            int upX, upY;
            int downX, downY;
            int leftX, leftY;
            int rightX, rightY;
            Queue<Tuple<int, int>> floodQ = new Queue<Tuple<int, int>>();
            floodQ.Enqueue(Tuple.Create(y, x));

            Dictionary<char, int> colorCountInSection = new Dictionary<char, int>();

            Tuple<int, int> current;
            while (floodQ.Count != 0)
            {
                current = floodQ.Dequeue();

                upY = current.Item1 - 1 < 0 ? -1 : current.Item1 - 1;//-1 means edge.
                upX = current.Item2;
                downY = current.Item1 + 1 >= n ? -1 : current.Item1 + 1;
                downX = current.Item2;
                leftY = current.Item1;
                leftX = current.Item2 - 1 < 0 ? -1 : current.Item2 - 1;
                rightY = current.Item1;
                rightX = current.Item2 + 1 >= n ? -1 : current.Item2 + 1;

                List<Tuple<int, int>> neighbors = getAllNeighbors(current, puzzle, color);
                puzzle[current.Item1, current.Item2] = 'X';

                if(upY != -1 && puzzle[upY, upX] != color && !flows.Contains(puzzle[upY, upX]) && puzzle[upY, upX] != '-' && puzzle[upY, upX] != 'X')
                {
                    if (!colorCountInSection.ContainsKey(puzzle[upY, upX]))
                        colorCountInSection.Add(puzzle[upY, upX], 1);
                    else
                        colorCountInSection[puzzle[upY, upX]]++;

                    puzzle[upY, upX] = 'X';
                }

                if (downY != -1 && puzzle[downY, downX] != color && !flows.Contains(puzzle[downY, downX]) && puzzle[downY, downX] != '-' && puzzle[downY, downX] != 'X')
                {
                    if (!colorCountInSection.ContainsKey(puzzle[downY, downX]))
                        colorCountInSection.Add(puzzle[downY, downX], 1);
                    else
                        colorCountInSection[puzzle[downY, downX]]++;

                    puzzle[downY, downX] = 'X';
                }

                if (leftX != -1 && puzzle[leftY, leftX] != color && !flows.Contains(puzzle[leftY, leftX]) && puzzle[leftY, leftX] != '-' && puzzle[leftY, leftX] != 'X')
                {
                    if (!colorCountInSection.ContainsKey(puzzle[leftY, leftX]))
                        colorCountInSection.Add(puzzle[leftY, leftX], 1);
                    else
                        colorCountInSection[puzzle[leftY, leftX]]++;

                    puzzle[leftY, leftX] = 'X';
                }

                if (rightX != -1 && puzzle[rightY, rightX] != color && !flows.Contains(puzzle[rightY, rightX]) && puzzle[rightY, rightX] != '-' && puzzle[rightY, rightX] != 'X')
                {
                    if (!colorCountInSection.ContainsKey(puzzle[rightY, rightX]))
                        colorCountInSection.Add(puzzle[rightY, rightX], 1);
                    else
                        colorCountInSection[puzzle[rightY, rightX]]++;

                    puzzle[rightY, rightX] = 'X';
                }

                foreach (Tuple<int, int> n in neighbors)
                    if (!floodQ.Contains(n))
                        floodQ.Enqueue(n);
            }

            //printPuzzle(puzzle);

            if (colorCountInSection.Count == 0)
            {
                return sectionContainsUnfinishedFlow((char[,])puzzle.Clone(), color, y, x);
                //if (sectionContainsUnfinishedFlow(puzzle, color, y, x))
                //    return false;
            }

            foreach (char key in colorCountInSection.Keys)
                if (colorCountInSection[key] != 2)
                    return false;

            return true;

        }

        static bool sectionContainsUnfinishedFlow(char[,] puzzle, char color, int sectionY, int sectionX)
        {
            Tuple<int, int> current = null;
            Tuple<int, int> c = Tuple.Create(sectionY, sectionX);
            Queue<Tuple<int, int>> floodQ = new Queue<Tuple<int, int>>();
            int upX, upY;
            int downX, downY;
            int leftX, leftY;
            int rightX, rightY;
            List<Tuple<int, int>> currentFlowNeighbors = new List<Tuple<int, int>>();
            List<Tuple<int, int>> neighbors;

            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    //if (c == null && puzzle[y, x] == 'X')
                    //{
                    //    c = Tuple.Create(y, x);
                    //    puzzle[y, x] = '-';
                    //}
                    if(puzzle[y, x] == 'X')
                        puzzle[y, x] = '-';
                }
            }
            floodQ.Enqueue(c);

            while (floodQ.Count != 0)
            {
                current = floodQ.Dequeue();
                neighbors = getAllNeighbors(current, puzzle, '?');

                upY = current.Item1 - 1 < 0 ? -1 : current.Item1 - 1;//-1 means edge.
                upX = current.Item2;
                downY = current.Item1 + 1 >= n ? -1 : current.Item1 + 1;
                downX = current.Item2;
                leftY = current.Item1;
                leftX = current.Item2 - 1 < 0 ? -1 : current.Item2 - 1;
                rightY = current.Item1;
                rightX = current.Item2 + 1 >= n ? -1 : current.Item2 + 1;

                if (upY != -1 && puzzle[upY, upX] == color/*!= 'X' && puzzle[upY, upX] != '-'*/)
                {
                    Tuple<int, int> neighbor = Tuple.Create(upY, upX);
                    if (!currentFlowNeighbors.Contains(neighbor))
                        currentFlowNeighbors.Add(neighbor);
                }

                if (downY != -1 && puzzle[downY, downX] == color/*!= 'X' && puzzle[downY, downX] != '-'*/)
                {
                    Tuple<int, int> neighbor = Tuple.Create(downY, downX);
                    if (!currentFlowNeighbors.Contains(neighbor))
                        currentFlowNeighbors.Add(neighbor);
                }

                if (leftX != -1 && puzzle[leftY, leftX] == color/*!= 'X' && puzzle[leftY, leftX] != '-'*/)
                {
                    Tuple<int, int> neighbor = Tuple.Create(leftY, leftX);
                    if (!currentFlowNeighbors.Contains(neighbor))
                        currentFlowNeighbors.Add(neighbor);
                }

                if (rightX != -1 && puzzle[rightY, rightX] == color/*!= 'X' && puzzle[rightY, rightX] != '-'*/)
                {
                    Tuple<int, int> neighbor = Tuple.Create(rightY, rightX);
                    if (!currentFlowNeighbors.Contains(neighbor))
                        currentFlowNeighbors.Add(neighbor);
                }

                puzzle[current.Item1, current.Item2] = 'X';
                //printPuzzle(puzzle);
                foreach (Tuple<int, int> n in neighbors)
                    if (!floodQ.Contains(n))
                        floodQ.Enqueue(n);
            }

            foreach (Tuple<int, int> point in currentFlowNeighbors)
                if (!visited.Contains(point))
                    return true;

            return false;
        }

        static List<Tuple<int, int>> getAllNeighbors(Tuple<int, int> point, char[,] puzzle, char color)
        {
            List<Tuple<int, int>> neighbors = new List<Tuple<int, int>>();

            int upX, upY;
            int downX, downY;
            int leftX, leftY;
            int rightX, rightY;
            int y = point.Item1;
            int x = point.Item2;

            upY = y - 1 < 0 ? -1 : y - 1;//-1 means edge.
            upX = x;
            downY = y + 1 >= n ? -1 : y + 1;
            downX = x;
            leftY = y;
            leftX = x - 1 < 0 ? -1 : x - 1;
            rightY = y;
            rightX = x + 1 >= n ? -1 : x + 1;
            if (upY != -1 && puzzle[upY, upX] != color && !flows.Contains(puzzle[upY, upX]) && puzzle[upY, upX] != 'X' /* && puzzle[upY, upX] == ' - '*/)
                neighbors.Add(Tuple.Create(upY, upX));
            if (downY != -1 && puzzle[downY, downX] != color && !flows.Contains(puzzle[downY, downX]) && puzzle[downY, downX] != 'X'/* && puzzle[downY, downX] == '-'*/)
                neighbors.Add(Tuple.Create(downY, downX));
            if (leftX != -1 && puzzle[leftY, leftX] != color && !flows.Contains(puzzle[leftY, leftX]) && puzzle[leftY, leftX] != 'X'/* && puzzle[leftY, leftX] == ' - '*/)
                neighbors.Add(Tuple.Create(leftY, leftX));
            if(rightX != -1 && puzzle[rightY, rightX] != color && !flows.Contains(puzzle[rightY, rightX]) && puzzle[rightY, rightX] != 'X'/* && puzzle[rightY, rightX] == '-'*/)
                neighbors.Add(Tuple.Create(rightY, rightX));

            return neighbors;
        }

        static bool checkInvalidMove(char[,] puzzle)
        {
            Dictionary<char, int> flowedColors = new Dictionary<char, int>();
            int upX, upY;
            int downX, downY;
            int leftX, leftY;
            int rightX, rightY;
            int blockedCount;
            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    if (puzzle[y, x] != '-')
                    {
                        if (!flowedColors.ContainsKey(puzzle[y, x]))
                            flowedColors.Add(puzzle[y, x], 1);
                        else
                            flowedColors[puzzle[y, x]]++;
                    }
                    blockedCount = 0;
                    upY = y - 1 < 0 ? -1 : y - 1;//-1 means edge.
                    upX = x;
                    downY = y + 1 >= n ? -1 : y + 1;
                    downX = x;
                    leftY = y;
                    leftX = x - 1 < 0 ? -1 : x - 1;
                    rightY = y;
                    rightX = x + 1 >= n ? -1 : x + 1;

                    if (upY == -1)
                        blockedCount++;
                    else if (puzzle[upY, upX] != '-' && puzzle[y, x] != puzzle[upY, upX])
                        blockedCount++;

                    if (downY == -1)
                        blockedCount++;
                    else if (puzzle[downY, downX] != '-' && puzzle[y, x] != puzzle[downY, downX])
                        blockedCount++;

                    if (leftX == -1)
                        blockedCount++;
                    else if (puzzle[leftY, leftX] != '-' && puzzle[y, x] != puzzle[leftY, leftX])
                        blockedCount++;

                    if (rightX == -1)
                        blockedCount++;
                    else if (puzzle[rightY, rightX] != '-' && puzzle[y, x] != puzzle[rightY, rightX])
                        blockedCount++;

                    if (blockedCount == 4 && puzzle[y, x] != '-')
                        return true;

                    if (puzzle[y, x] != '-' && upY != -1 && rightX != -1 && puzzle[y, x] == puzzle[upY, upX] && puzzle[y, x] == puzzle[rightY, rightX] && puzzle[y, x] == puzzle[upY, rightX])//Get rid of two thick moves.
                        return true;

                    if (puzzle[y, x] == '-')
                    {
                        char[] neighbors = new char[4];
                        blockedCount = 0;
                        if (upY == -1)
                            blockedCount++;
                        else
                            neighbors[0] = puzzle[upY, upX];

                        if (downY == -1)
                            blockedCount++;
                        else
                            neighbors[1] = puzzle[downY, downX];

                        if (leftX == -1)
                            blockedCount++;
                        else
                            neighbors[2] = puzzle[leftY, leftX];

                        if (rightX == -1)
                            blockedCount++;
                        else
                            neighbors[3] = puzzle[rightY, rightX];
                        neighbors = getLetterNeighbors(neighbors);
                        Array.Sort(neighbors);

                        //if (y == 5 && x == 1)
                        //    ;
                        if (blockedCount == 0)//3 neighbors must be the same
                        {
                            //if (neighbors[1] == neighbors[2] && neighbors[2] == neighbors[3])
                            //    return true;
                            if (neighbors.Length == 3 && neighbors[0] == neighbors[1] && neighbors[1] == neighbors[2])
                                return true;
                            else if (neighbors.Length == 4 /*neighbors[0] > 0*/)
                            {
                                if (neighbors[0] != neighbors[1] && neighbors[1] != neighbors[2] && neighbors[2] != neighbors[3])
                                    return true;
                            }
                        }
                        else if (blockedCount == 1)
                        {
                            if (neighbors.Length == 3 && neighbors[0] != neighbors[1] && neighbors[0] != neighbors[2] && neighbors[1] != neighbors[2])
                                return true;
                            else if (neighbors.Length == 3 && neighbors[0] == neighbors[1] && neighbors[1] == neighbors[2])
                                return true;
                        }

                    }
                }
            }
            flows.Clear();
            foreach (char color in flowedColors.Keys)
                if (flowedColors[color] > 2)
                    if (!flows.Contains(color))
                        flows.Add(color);

            return false;
        }

        static char[] getLetterNeighbors(char[] neighbors)
        {
            List<char> letterNeighbors = new List<char>();
            //char nonce = '\0';
            foreach (char c in neighbors)
                if (c != '\0' && c != '-')
                    letterNeighbors.Add(c);
                //else
                //    letterNeighbors.Add(nonce++);
            return letterNeighbors.ToArray();
        }

        static void printPuzzle(char[,] puzzle)
        {
            for (int y = 0; y < n; y++)
            {
                Console.WriteLine();
                for (int x = 0; x < n; x++)
                {
                    Console.Write(puzzle[y, x] + " ");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            //System.Threading.Thread.Sleep(500);

        }
    }
}
