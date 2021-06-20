using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowFreeSolver
{
    class Solve
    {
        private int n;
        private List<Tuple<int, int>> visited = new List<Tuple<int, int>>();
        private Dictionary<char, int> colorCounts = new Dictionary<char, int>();
        private char[,] original;
        private bool showPrintOut;
        private List<char> flows = new List<char>();
        private Tuple<int, int>[] colorPositions;
        private int startColorIndex;
        private readonly object Lock = new object();
        private List<Tuple<int, int>> currentFlowNeighbors = new List<Tuple<int, int>>();
        private List<Tuple<int, int>> immediateNeighbors = new List<Tuple<int, int>>();
        private Dictionary<char, int> flowedColors = new Dictionary<char, int>();
        private List<char> letterNeighbors = new List<char>();
        private char[] charNeighbors = new char[4];
        private Queue<Tuple<int, int>> floodQ = new Queue<Tuple<int, int>>();
        private Dictionary<char, int> colorCountInSection = new Dictionary<char, int>();
        private int totalNonEmpty = 0;

        public char[,] solvePuzzle(char[,] puzzle, Tuple<int, int>[] colorPositions, int startColorIndex)
        {
            this.colorPositions = colorPositions;
            this.startColorIndex = startColorIndex;
            showPrintOut = true;
            n = (int)Math.Sqrt(puzzle.Length);
            original = (char[,])puzzle.Clone();
            Tuple<int, int, int> pos = findNextColorPosition(puzzle, startColorIndex);
            visited.Add(Tuple.Create(pos.Item1, pos.Item2));
            char[,] p1 = solve(puzzle, pos.Item1 - 1, pos.Item2, puzzle[pos.Item1, pos.Item2], startColorIndex, 0);
            if (p1 == null)
            {
                char[,] p2 = solve(puzzle, pos.Item1 + 1, pos.Item2, puzzle[pos.Item1, pos.Item2], startColorIndex, 0);
                if (p2 == null)
                {
                    char[,] p3 = solve(puzzle, pos.Item1, pos.Item2 - 1, puzzle[pos.Item1, pos.Item2], startColorIndex, 0);
                    if (p3 == null)
                    {
                        char[,] p4 = solve(puzzle, pos.Item1, pos.Item2 + 1, puzzle[pos.Item1, pos.Item2], startColorIndex, 0);
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



        private bool isPositionEdgeOrColor(int y, int x, char current)
        {
            //Console.WriteLine("y: " + y + " x: " + x + " current: " + current + " returning: " + (y == 0 || y == n - 1 || x == 0 || x == n - 1 || (current != '-' && !containsVisited(y, x))));
            //Console.WriteLine(visited.Contains)
            return (y == 0 || y == n - 1 || x == 0 || x == n - 1 || (current != '-' && !containsVisited(y,x)));
        }

        private char[,] solve(char[,] puzzle, int y, int x, char color, int startColorIndex, int depth)
        {
            depth++;
            //if (y == 7 && x == 4 && puzzle[0,0] == 'Y' && puzzle[7,0] == 'Y')
            //    ;
            //if (showPrintOut)
            //{
            //    //Console.WriteLine("x: " + x + " y: " + y);
            //    printPuzzle(puzzle);
            //}

            //if (x == 5 && y == 4)
            //    ;

            if (y == n || x == n || y < 0 || x < 0)
                return null;

            if (/*depth == 1 && */Paths.paths[y, x].Contains(color))
                return null;

            if (checkInvalidMove(puzzle))
                return null;

            if (isPositionEdgeOrColor(y, x, puzzle[y, x]))
            {
                //printPuzzle(puzzle);
                if (checkFailFloodFill(puzzle, color))
                    return null;
            }

            if (puzzleSolved(puzzle))//The puzzle has been solved return it.
            {
                //Console.WriteLine("PATHS:");
                //for (int k = 0; k < n; k++)
                //    for (int l = 0; l < n; l++)
                //        if (Paths.paths[k, l].Count > 0)
                //            Console.WriteLine(k + ", " + l + ": " + string.Join(" ", Paths.paths[k, l].ToArray()));
                return puzzle;

            }

            //removeVisitedForSingleLetters(puzzle, y, x);
            if (containsVisited(y, x))
                return null;
            try
            {               
                if (puzzle[y, x] == '-')
                {
                    puzzle[y, x] = color;
                    visited.Add(Tuple.Create(y, x));
                    //printPuzzle(puzzle);
                }            
                else if (puzzle[y, x] == color && !containsVisited(y, x))//Flow found. Find another.
                {
                    if (showPrintOut)
                    {
                        //Console.WriteLine("x: " + x + " y: " + y);
                        if (totalNonEmpty > Paths.maxTotalNonEmpty)
                        {
                            printPuzzle(puzzle);
                            Paths.maxTotalNonEmpty = totalNonEmpty;
                        }
                    }
                    //if (checkFailFloodFill(puzzle, color))
                    //    return null;
                    //visited.Add(Tuple.Create(y, x));
                    Tuple<int, int, int> nextPoint = findNextColorPosition(puzzle, startColorIndex);
                    y = nextPoint.Item1;
                    x = nextPoint.Item2;
                    startColorIndex = nextPoint.Item3;
                    color = puzzle[y, x];

                    //depth = 0;

                    if(!containsVisited(y,x))
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
            
            result1 = solve(puzzle, y - 1, x, color, startColorIndex, depth);//try moving up.
            if (result1 != null)
                return result1;
            result2 = solve(puzzle, y + 1, x, color, startColorIndex, depth);//try moving down.
            if (result2 != null)
                return result2;
            result3 = solve(puzzle, y, x - 1, color, startColorIndex, depth);//try moving left.
            if (result3 != null)
                return result3;
            result4 = solve(puzzle, y, x + 1, color, startColorIndex, depth);//try moving right.
            if (result4 != null)
                return result4;

            if (original[y, x] == '-')
            {
                puzzle[y, x] = '-';
                removeVisited(y, x);
            }

            if(depth == 1)
                Paths.paths[y, x].Add(color);

            return null;
        }

        private bool puzzleSolved(char[,] puzzle)
        {
            for (int y = 0; y < n; y++)
                for (int x = 0; x < n; x++)
                {
                    if (isSingleColor(puzzle, y, x))
                    {
                        colorCounts.Clear();
                        return false;
                    }
                    if (puzzle[y, x] == '-')
                    {
                        colorCounts.Clear();
                        return false;
                    }
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
                {
                    colorCounts.Clear();
                    return false;
                }

            colorCounts.Clear();
            return true;
        }

        private bool containsVisited(int y, int x)
        {
            foreach (Tuple<int, int> t in visited)
                if (t.Item1 == y && t.Item2 == x)
                    return true;
            return false;
        }

        private void removeVisited(int y, int x)
        {
            for (int i = 0; i < visited.Count; i++)
                if (visited[i].Item1 == y && visited[i].Item2 == x)
                {
                    visited.RemoveAt(i);
                    break;
                }
        }

        private Tuple<int, int, int> findNextColorPosition(char[,] puzzle, int index)
        {
            while (flows.Contains(puzzle[colorPositions[index].Item1, colorPositions[index].Item2]))
            {
                index = ((index + 1) % colorPositions.Length);
            }
            return Tuple.Create(colorPositions[index].Item1, colorPositions[index].Item2, index);
        }


        //static Tuple<int, int> findNextColorPosition(char[,] puzzle)
        //{
        //    for (int y = 0; y < n; y++)
        //        for (int x = 0; x < n; x++)
        //            if (puzzle[y, x] != '-' && isSingleColor(puzzle, y, x))
        //                return Tuple.Create(y, x);
        //    return null;
        //}

        private bool isSingleColor(char[,] puzzle, int y, int x)
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



        private bool checkFailFloodFill(char[,] p, char color)
        {
            //if (p[2, 2] == 'Y' && p[2, 3] == '-' && p[2, 4] == 'Y')
            //    ;
            char[,] puzzle = (char[,])p.Clone();
            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    if (puzzle[y, x] == '-')
                        if (!isTwoInSection(puzzle, color, y, x))//twoPerColorInEachSection.Add(isTwoInSection(puzzle, color, y, x));
                            return true;
                }
            }

            return false;
        }

        private bool isTwoInSection(char[,] puzzle, char color, int y, int x)
        {
            int upX, upY;
            int downX, downY;
            int leftX, leftY;
            int rightX, rightY;
            floodQ.Enqueue(Tuple.Create(y, x));



            Tuple<int, int> current;
            List<Tuple<int, int>> neighbors = null;
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

                neighbors = getAllNeighbors(current, puzzle, color);
                puzzle[current.Item1, current.Item2] = 'X';

                if (upY != -1 && puzzle[upY, upX] != color && !flows.Contains(puzzle[upY, upX]) && puzzle[upY, upX] != '-' && puzzle[upY, upX] != 'X')
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
                floodQ.Clear();
                colorCountInSection.Clear();
                return sectionContainsUnfinishedFlow((char[,])puzzle.Clone(), color, y, x);
                //if (sectionContainsUnfinishedFlow(puzzle, color, y, x))
                //    return false;
            }

            foreach (char key in colorCountInSection.Keys)
                if (colorCountInSection[key] != 2)
                {
                    floodQ.Clear();
                    colorCountInSection.Clear();
                    return false;
                }

            floodQ.Clear();
            colorCountInSection.Clear();
            return true;

        }

        private bool sectionContainsUnfinishedFlow(char[,] puzzle, char color, int sectionY, int sectionX)
        {
            Tuple<int, int> current = null;
            Tuple<int, int> c = Tuple.Create(sectionY, sectionX);
            int upX, upY;
            int downX, downY;
            int leftX, leftY;
            int rightX, rightY;
            List<Tuple<int, int>> neighbors = null;

            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    //if (c == null && puzzle[y, x] == 'X')
                    //{
                    //    c = Tuple.Create(y, x);
                    //    puzzle[y, x] = '-';
                    //}
                    if (puzzle[y, x] == 'X')
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
                {
                    currentFlowNeighbors.Clear();
                    floodQ.Clear();
                    return true;
                }

            currentFlowNeighbors.Clear();
            floodQ.Clear();
            return false;
        }

        private List<Tuple<int, int>> getAllNeighbors(Tuple<int, int> point, char[,] puzzle, char color)
        {
            immediateNeighbors.Clear();
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
                immediateNeighbors.Add(Tuple.Create(upY, upX));
            if (downY != -1 && puzzle[downY, downX] != color && !flows.Contains(puzzle[downY, downX]) && puzzle[downY, downX] != 'X'/* && puzzle[downY, downX] == '-'*/)
                immediateNeighbors.Add(Tuple.Create(downY, downX));
            if (leftX != -1 && puzzle[leftY, leftX] != color && !flows.Contains(puzzle[leftY, leftX]) && puzzle[leftY, leftX] != 'X'/* && puzzle[leftY, leftX] == ' - '*/)
                immediateNeighbors.Add(Tuple.Create(leftY, leftX));
            if (rightX != -1 && puzzle[rightY, rightX] != color && !flows.Contains(puzzle[rightY, rightX]) && puzzle[rightY, rightX] != 'X'/* && puzzle[rightY, rightX] == '-'*/)
                immediateNeighbors.Add(Tuple.Create(rightY, rightX));


            return immediateNeighbors;
        }

        private bool checkInvalidMove(char[,] puzzle)
        {
            totalNonEmpty = 0;
            flowedColors.Clear();
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
                        totalNonEmpty++;
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
                        for(int i = 0; i < 4; i++)
                            charNeighbors[i] = '\0';
                        blockedCount = 0;
                        if (upY == -1)
                            blockedCount++;
                        else
                            charNeighbors[0] = puzzle[upY, upX];

                        if (downY == -1)
                            blockedCount++;
                        else
                            charNeighbors[1] = puzzle[downY, downX];

                        if (leftX == -1)
                            blockedCount++;
                        else
                            charNeighbors[2] = puzzle[leftY, leftX];

                        if (rightX == -1)
                            blockedCount++;
                        else
                            charNeighbors[3] = puzzle[rightY, rightX];
                        char[] letterNeighbors = getLetterNeighbors(charNeighbors);
                        Array.Sort(letterNeighbors);

                        //if (y == 5 && x == 1)
                        //    ;
                        if (blockedCount == 0)//3 neighbors must be the same
                        {
                            //if (neighbors[1] == neighbors[2] && neighbors[2] == neighbors[3])
                            //    return true;
                            if (letterNeighbors.Length == 3 && letterNeighbors[0] == letterNeighbors[1] && letterNeighbors[1] == letterNeighbors[2])
                                return true;
                            else if (letterNeighbors.Length == 4 /*neighbors[0] > 0*/)
                            {
                                if (letterNeighbors[0] != letterNeighbors[1] && letterNeighbors[1] != letterNeighbors[2] && letterNeighbors[2] != letterNeighbors[3])
                                    return true;
                                else if ((letterNeighbors[0] == letterNeighbors[1] && letterNeighbors[1] == letterNeighbors[2]) ||
                                    (letterNeighbors[1] == letterNeighbors[2] && letterNeighbors[2] == letterNeighbors[3]))
                                    return true;
                            }
                        }
                        else if (blockedCount == 1)
                        {
                            if (letterNeighbors.Length == 3 && letterNeighbors[0] != letterNeighbors[1] && letterNeighbors[0] != letterNeighbors[2] && letterNeighbors[1] != letterNeighbors[2])
                                return true;
                            else if (letterNeighbors.Length == 3 && letterNeighbors[0] == letterNeighbors[1] && letterNeighbors[1] == letterNeighbors[2])
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

        private char[] getLetterNeighbors(char[] neighbors)
        {
            letterNeighbors.Clear();
            //char nonce = '\0';
            foreach (char c in neighbors)
                if (c != '\0' && c != '-')
                    letterNeighbors.Add(c);
            //else
            //    letterNeighbors.Add(nonce++);
            return letterNeighbors.ToArray();
        }

        private void printPuzzle(char[,] puzzle)
        {
            //lock(Lock)
            //{
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
                System.Threading.Thread.Sleep(250);
            //}

            //System.Threading.Thread.Sleep(500);

        }
    }

    class Paths
    {
        public static List<char>[,] paths;
        public static int maxTotalNonEmpty = 0;

        public Paths(int n)
        {
            paths = new List<char>[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    paths[i, j] = new List<char>();
                }
            }
        }
    }
}
