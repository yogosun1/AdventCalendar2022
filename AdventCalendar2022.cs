using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Headers;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Xml;
using System.Xml.Linq;

namespace AdventCalendar2022
{
    [TestClass]
    public class AdventCalendar2022
    {
        [TestMethod]
        public void Day1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day1.txt").ToList();
            List<int> elfCalorieList = new List<int>();
            int elfCalorie = 0;
            int itemCalorie = 0;
            foreach (string input in inputList)
            {
                if (int.TryParse(input, out itemCalorie))
                    elfCalorie += itemCalorie;
                else
                {
                    elfCalorieList.Add(elfCalorie);
                    elfCalorie = 0;
                }
            }
            if (elfCalorie > 0)
                elfCalorieList.Add(elfCalorie);

            int maxCalories = elfCalorieList.Max();
            int top3Calories = elfCalorieList.OrderByDescending(o => o).Take(3).Sum();
        }

        [TestMethod]
        public void Day2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day2.txt").ToList();

            /*
             * A = Rock
             * B = Paper
             * C = Scissors
             * 
             * X = Rock
             * Y = Paper
             * Z = Scissors
             * */
            int totalScoreTask1 = 0;
            int totalScoreTask2 = 0;
            foreach (string input in inputList)
            {
                string[] splitRow = input.Split(' ');
                string opponent = splitRow[0];
                string player = splitRow[1];

                int optionScoreTask1 = player == "X" ? 1 : player == "Y" ? 2 : 3;
                int resultScoreTask1 = 0;
                if ((opponent == "A" && player == "X")
                    ||
                    (opponent == "B" && player == "Y")
                    ||
                    (opponent == "C" && player == "Z"))
                    resultScoreTask1 = 3;
                else if ((opponent == "A" && player == "Y")
                    ||
                    (opponent == "B" && player == "Z")
                    ||
                    (opponent == "C" && player == "X"))
                    resultScoreTask1 = 6;
                totalScoreTask1 += (optionScoreTask1 + resultScoreTask1);


                int optionScoreTask2 = 1;
                int resultScoreTask2 = player == "X" ? 0 : player == "Y" ? 3 : 6;
                if ((opponent == "A" && player == "X")
                    ||
                    (opponent == "C" && player == "Y")
                    ||
                    (opponent == "B" && player == "Z"))
                    optionScoreTask2 = 3;
                else if ((opponent == "C" && player == "X")
                    ||
                    (opponent == "B" && player == "Y")
                    ||
                    (opponent == "A" && player == "Z"))
                    optionScoreTask2 = 2;

                totalScoreTask2 += (optionScoreTask2 + resultScoreTask2);
            }
        }

        [TestMethod]
        public void Day3()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day3.txt").ToList();
            int sum = 0;
            foreach (string input in inputList)
            {
                List<char> firstCompartment = input.Take(input.Length / 2).ToList();
                List<char> secondCompartment = input.Skip(input.Length / 2).ToList();
                foreach (char item in firstCompartment)
                {
                    if (secondCompartment.Contains(item))
                    {
                        int ascii = item;
                        if (ascii < 91)
                            sum += ascii - 38;
                        else
                            sum += ascii - 96;
                        break;
                    }
                }
            }

            int sum2 = 0;
            for (int i = 0; i < inputList.Count(); i = i + 3)
            {
                string firstRucksack = inputList[i];
                string secondRucksack = inputList[i + 1];
                string thirdRucksack = inputList[i + 2];
                foreach (char item in firstRucksack)
                {
                    if (secondRucksack.Contains(item) && thirdRucksack.Contains(item))
                    {
                        int ascii = item;
                        if (ascii < 91)
                            sum2 += ascii - 38;
                        else
                            sum2 += ascii - 96;
                        break;
                    }
                }
            }
        }

        [TestMethod]
        public void Day4()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day4.txt").ToList();
            int count = 0;
            int count2 = 0;
            foreach (string pair in inputList)
            {
                string[] splitPair = pair.Split(',');
                string elf1 = splitPair[0];
                string elf2 = splitPair[1];
                string[] elf1Split = elf1.Split('-');
                int elf1Start = int.Parse(elf1Split[0]);
                int elf1End = int.Parse(elf1Split[1]);
                string[] elf2Split = elf2.Split('-');
                int elf2Start = int.Parse(elf2Split[0]);
                int elf2End = int.Parse(elf2Split[1]);
                if ((elf1Start >= elf2Start && elf1End <= elf2End) || (elf2Start >= elf1Start && elf2End <= elf1End))
                    count++;
                if (elf1Start <= elf2End && elf2Start <= elf1End)
                    count2++;
            }
        }

        [TestMethod]
        public void Day5()
        {
            List<List<char>> stack1List = GetStackList();
            List<List<char>> stack2List = GetStackList();
            List<string> inputList = File.ReadAllLines(@"Input\Day5.txt").ToList();
            foreach (string input in inputList)
            {
                string[] splitList = input.Split(' ');
                int moveCount = int.Parse(splitList[1]);
                int moveFrom = int.Parse(splitList[3]) - 1;
                int moveTo = int.Parse(splitList[5]) - 1;
                for (int i = 0; i < moveCount; i++)
                {
                    stack1List[moveTo].Add(stack1List[moveFrom].Last());
                    stack1List[moveFrom].RemoveAt(stack1List[moveFrom].Count() - 1);
                }
                stack2List[moveTo].AddRange(stack2List[moveFrom].TakeLast(moveCount));
                stack2List[moveFrom].RemoveRange(stack2List[moveFrom].Count() - moveCount, moveCount);
            }
            string result1 = string.Empty;
            stack1List.ForEach(f => result1 += f.Last());
            string result2 = string.Empty;
            stack2List.ForEach(f => result2 += f.Last());
        }

        private List<List<char>> GetStackList()
        {
            List<List<char>> stackList = new List<List<char>>();
            stackList.Add(new List<char> { 'B', 'P', 'N', 'Q', 'H', 'D', 'R', 'T' });
            stackList.Add(new List<char> { 'W', 'G', 'B', 'J', 'T', 'V' });
            stackList.Add(new List<char> { 'N', 'R', 'H', 'D', 'S', 'V', 'M', 'Q' });
            stackList.Add(new List<char> { 'P', 'Z', 'N', 'M', 'C' });
            stackList.Add(new List<char> { 'D', 'Z', 'B' });
            stackList.Add(new List<char> { 'V', 'C', 'W', 'Z' });
            stackList.Add(new List<char> { 'G', 'Z', 'N', 'C', 'V', 'Q', 'L', 'S' });
            stackList.Add(new List<char> { 'L', 'G', 'J', 'M', 'D', 'N', 'V' });
            stackList.Add(new List<char> { 'T', 'P', 'M', 'F', 'Z', 'C', 'G' });
            return stackList;
        }

        [TestMethod]
        public void Day6()
        {
            string input = File.ReadAllText(@"Input\Day6.txt");
            List<char> char1List = new List<char>();
            List<char> char2List = new List<char>();
            foreach (char c in input)
            {
                char1List.Add(c);
                if (char1List.TakeLast(4).Distinct().Count() == 4)
                    break;
            }
            foreach (char c in input)
            {
                char2List.Add(c);
                if (char2List.TakeLast(14).Distinct().Count() == 14)
                    break;
            }
            int count1 = char1List.Count();
            int count2 = char2List.Count();
        }

        [TestMethod]
        public void Day7()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day7.txt").ToList();
            List<SystemFile> fileList = new List<SystemFile>();
            List<string> directoryPathList = new List<string>();
            string currentPath = string.Empty;

            foreach (string input in inputList)
            {
                List<string> inputSplit = input.Split(' ').ToList();
                if (input.StartsWith('$')) // Command
                {
                    string command = inputSplit[1];
                    if (command == "cd") // change directory
                    {
                        string folder = inputSplit[2];
                        if (folder == "..")
                            currentPath = currentPath.Substring(0, currentPath.LastIndexOf('/'));
                        else
                        {
                            currentPath = currentPath + "/" + folder;
                            if (!directoryPathList.Any(a => a == currentPath))
                                directoryPathList.Add(currentPath);
                        }
                    }
                    else if (command == "ls") // list directory
                        continue;
                }
                else if (input.StartsWith("dir")) // directory
                {
                    string path = currentPath + "/" + inputSplit[1];
                    if (!directoryPathList.Any(a => a == path))
                        directoryPathList.Add(path);
                }
                else // file
                {
                    int size = int.Parse(inputSplit[0]);
                    string name = inputSplit[1];
                    SystemFile systemFile = fileList.FirstOrDefault(w => w.Name == name && w.Path == currentPath);
                    if (systemFile == null)
                    {
                        systemFile = new SystemFile { Size = size, Name = name, Path = currentPath };
                        fileList.Add(systemFile);
                    }
                }
            }

            int filteredSize = 0;
            foreach (string directoryPath in directoryPathList)
            {
                Debug.WriteLine(directoryPath + " " + fileList.Where(w => w.Path.Contains(directoryPath)).Count() + " " + fileList.Where(w => w.Path.Contains(directoryPath)).Sum(s => s.Size));
                int directorySize = fileList.Where(w => w.Path.Contains(directoryPath)).Sum(s => s.Size);
                if (directorySize <= 100000)
                    filteredSize += directorySize;
            }

            int minSize = 70000000;
            foreach (string directoryPath in directoryPathList)
            {
                int directorySize = fileList.Where(w => w.Path.Contains(directoryPath)).Sum(s => s.Size);
                if (directorySize < minSize && directorySize >= 8381165)
                    minSize = directorySize;
            }
        }

        private class SystemFile
        {
            public string Name { get; set; }
            public int Size { get; set; }
            public string Path { get; set; }
        }

        [TestMethod]
        public void Day8()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day8.txt").ToList();
            List<Tree> treeList = new List<Tree>();
            int x = 0, y = 0;
            foreach (string input in inputList)
            {
                x = 0;
                input.Cast<char>().ToList().ForEach(e =>
                {
                    treeList.Add(new Tree { x = x, y = y, Height = int.Parse(e.ToString()) });
                    x++;
                });
                y++;
            }

            int visibleTrees = 0;
            foreach (Tree tree in treeList)
            {
                if (!treeList.Any(w => w.x < tree.x && w.y == tree.y && w.Height >= tree.Height) // Visible left?
                    || !treeList.Any(w => w.x > tree.x && w.y == tree.y && w.Height >= tree.Height) // Visible right?
                    || !treeList.Any(w => w.y < tree.y && w.x == tree.x && w.Height >= tree.Height) // Visible up?
                    || !treeList.Any(w => w.y > tree.y && w.x == tree.x && w.Height >= tree.Height)) // Visible down?
                    visibleTrees++;
            }

            int maxViewDistance = 0;
            foreach (Tree tree in treeList)
            {
                int viewDistance = CheckDirectionViewDistance(treeList.Where(w => w.x < tree.x && w.y == tree.y).OrderByDescending(o => o.x).ToList(), tree.Height); // left
                viewDistance *= CheckDirectionViewDistance(treeList.Where(w => w.x > tree.x && w.y == tree.y).OrderBy(o => o.x).ToList(), tree.Height); // right
                viewDistance *= CheckDirectionViewDistance(treeList.Where(w => w.y < tree.y && w.x == tree.x).OrderByDescending(o => o.y).ToList(), tree.Height); // top
                viewDistance *= CheckDirectionViewDistance(treeList.Where(w => w.y > tree.y && w.x == tree.x).OrderBy(o => o.y).ToList(), tree.Height); // bottom
                if (maxViewDistance < viewDistance)
                    maxViewDistance = viewDistance;
            }
        }

        private class Tree
        {
            public int x { get; set; }
            public int y { get; set; }
            public int Height { get; set; }
        }

        private int CheckDirectionViewDistance(List<Tree> treeList, int height)
        {
            //int viewDistance = 0;
            //foreach (Tree tree in treeList)
            //{
            //    viewDistance++;
            //    if (tree.Height >= height)
            //        break;
            //}
            //return viewDistance;

            int viewDistance = treeList.FindIndex(w => w.Height >= height);
            if (viewDistance == -1)
                return treeList.Count();
            else
                return viewDistance + 1;
        }

        [TestMethod]
        public void Day9_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day9.txt").ToList();
            Point tailPoint = new Point(0, 0);
            Point headPoint = new Point(0, 0);
            List<Point> tailPositions = new List<Point>() { tailPoint };
            foreach (string input in inputList)
            {
                string[] inputSplit = input.Split(' ');
                string moveDirection = inputSplit[0];
                int moveCount = int.Parse(inputSplit[1]);
                for (int i = 0; i < moveCount; i++)
                {
                    if (moveDirection == "U")
                        headPoint.Y++;
                    else if (moveDirection == "D")
                        headPoint.Y--;
                    else if (moveDirection == "L")
                        headPoint.X--;
                    else if (moveDirection == "R")
                        headPoint.X++;
                    tailPoint = CalculateNewTailPoint(headPoint, tailPoint);
                    if (!tailPositions.Contains(tailPoint))
                        tailPositions.Add(tailPoint);
                }
            }
        }

        [TestMethod]
        public void Day9_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day9.txt").ToList();
            Point[] knotList = new Point[10];
            List<Point> tailPositions = new List<Point>() { knotList[knotList.Count() - 1] };
            foreach (string input in inputList)
            {
                string[] inputSplit = input.Split(' ');
                string moveDirection = inputSplit[0];
                int moveCount = int.Parse(inputSplit[1]);
                for (int i = 0; i < moveCount; i++)
                {
                    if (moveDirection == "U")
                        knotList[0].Y++;
                    else if (moveDirection == "D")
                        knotList[0].Y--;
                    else if (moveDirection == "L")
                        knotList[0].X--;
                    else if (moveDirection == "R")
                        knotList[0].X++;
                    for (int k = 1; k < knotList.Count(); k++)
                        knotList[k] = CalculateNewTailPoint(knotList[k - 1], knotList[k]);
                    if (!tailPositions.Contains(knotList[knotList.Count() - 1]))
                        tailPositions.Add(knotList[knotList.Count() - 1]);
                }
            }
        }

        private Point CalculateNewTailPoint(Point headPoint, Point tailPoint)
        {
            if (Math.Abs(headPoint.Y - tailPoint.Y) > 1)
            {
                if (headPoint.Y > tailPoint.Y)
                    tailPoint.Y++;
                else
                    tailPoint.Y--;

                if (headPoint.X - tailPoint.X > 1)
                    tailPoint.X++;
                else if (headPoint.X - tailPoint.X < -1)
                    tailPoint.X--;
                else
                    tailPoint.X = headPoint.X;
            }
            else if (Math.Abs(headPoint.X - tailPoint.X) > 1)
            {
                if (headPoint.X > tailPoint.X)
                    tailPoint.X++;
                else
                    tailPoint.X--;

                if (headPoint.Y - tailPoint.Y > 1)
                    tailPoint.Y++;
                else if (headPoint.Y - tailPoint.Y < -1)
                    tailPoint.Y--;
                else
                    tailPoint.Y = headPoint.Y;
            }
            return tailPoint;
        }

        [TestMethod]
        public void Day10()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day10.txt").ToList();
            List<int> cycleList = new List<int> { 1 };
            int x = 1;
            foreach (string input in inputList)
            {
                string[] inputSplit = input.Split(' ');
                string command = inputSplit[0];
                if (command == "addx")
                {
                    cycleList.Add(x);
                    x += int.Parse(inputSplit[1]);
                    cycleList.Add(x);
                }
                else
                    cycleList.Add(x);
            }

            int signalStrengthSum = 0;
            for (int i = 19; i < 240; i += 40)
                signalStrengthSum += (cycleList[i] * (i + 1));

            string image = string.Empty;
            for (int i = 0; i < cycleList.Count; i++)
            {
                int horizontalPosition = i % 40;
                if (horizontalPosition <= (cycleList[i] + 1) && horizontalPosition >= (cycleList[i] - 1))
                    image += "#";
                else
                    image += ".";
            }

            for (int i = 0; i < image.Count() - 1; i += 40)
                Debug.WriteLine(image.Substring(i, 40));
        }

        [TestMethod]
        public void Day11_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day11.txt").ToList();
            List<Monkey> monkeyList = new List<Monkey>();
            for (int i = 0; i < inputList.Count; i += 7)
            {
                List<string> operationSplit = inputList[i + 2].Split('=')[1].Trim().Split(' ').ToList();
                Monkey monkey = new Monkey
                {
                    ItemList = inputList[i + 1].Split(':')[1].Split(',').Select(s => Int64.Parse(s.Trim())).ToList(),
                    OperationLeft = operationSplit[0],
                    OperationSign = operationSplit[1],
                    OperationRight = operationSplit[2],
                    TestDivider = int.Parse(inputList[i + 3].Trim().Split(' ')[3]),
                    TrueMonkeyIdThrow = int.Parse(inputList[i + 4].Trim().Split(' ')[5]),
                    FalseMonkeyIdThrow = int.Parse(inputList[i + 5].Trim().Split(' ')[5]),
                };
                monkeyList.Add(monkey);
            }
            for (int round = 1; round <= 20; round++)
            {
                foreach (Monkey monkey in monkeyList)
                {
                    foreach (Int64 item in monkey.ItemList.ToArray())
                    {
                        Int64 leftValue = monkey.OperationLeft == "old" ? item : Int64.Parse(monkey.OperationLeft);
                        Int64 rightValue = monkey.OperationRight == "old" ? item : Int64.Parse(monkey.OperationRight);
                        Int64 worryLevel = monkey.OperationSign == "*" ? leftValue * rightValue : leftValue + rightValue;
                        worryLevel /= 3;
                        if (worryLevel % monkey.TestDivider == 0)
                            monkeyList[monkey.TrueMonkeyIdThrow].ItemList.Add(worryLevel);
                        else
                            monkeyList[monkey.FalseMonkeyIdThrow].ItemList.Add(worryLevel);
                        monkey.InspectionCount++;
                        monkey.ItemList.Remove(item);
                    }
                }
            }
            int monkeyBusiness = monkeyList.Select(s => s.InspectionCount).OrderByDescending(o => o).Take(2).Aggregate((sum, next) => sum * next);
        }

        [TestMethod]
        public void Day11_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day11.txt").ToList();
            List<Monkey> monkeyList = new List<Monkey>();
            for (int i = 0; i < inputList.Count; i += 7)
            {
                List<string> operationSplit = inputList[i + 2].Split('=')[1].Trim().Split(' ').ToList();
                Monkey monkey = new Monkey
                {
                    ItemList = inputList[i + 1].Split(':')[1].Split(',').Select(s => Int64.Parse(s.Trim())).ToList(),
                    OperationLeft = operationSplit[0],
                    OperationSign = operationSplit[1],
                    OperationRight = operationSplit[2],
                    TestDivider = int.Parse(inputList[i + 3].Trim().Split(' ')[3]),
                    TrueMonkeyIdThrow = int.Parse(inputList[i + 4].Trim().Split(' ')[5]),
                    FalseMonkeyIdThrow = int.Parse(inputList[i + 5].Trim().Split(' ')[5]),
                };
                monkeyList.Add(monkey);
            }
            int leastCommonMultiple = monkeyList.Select(s => s.TestDivider).Aggregate((sum, next) => sum * next); // all dividers are prime numbers
            for (int round = 1; round <= 10000; round++)
            {
                foreach (Monkey monkey in monkeyList)
                {
                    foreach (Int64 item in monkey.ItemList.ToArray())
                    {
                        Int64 leftValue = monkey.OperationLeft == "old" ? item : Int64.Parse(monkey.OperationLeft);
                        Int64 rightValue = monkey.OperationRight == "old" ? item : Int64.Parse(monkey.OperationRight);
                        Int64 worryLevel = monkey.OperationSign == "*" ? leftValue * rightValue : leftValue + rightValue;
                        worryLevel %= leastCommonMultiple;
                        if (worryLevel % monkey.TestDivider == 0)
                            monkeyList[monkey.TrueMonkeyIdThrow].ItemList.Add(worryLevel);
                        else
                            monkeyList[monkey.FalseMonkeyIdThrow].ItemList.Add(worryLevel);
                        monkey.InspectionCount++;
                        monkey.ItemList.Remove(item);
                    }
                }
            }
            Int64 monkeyBusiness = monkeyList.Select(s => (Int64)s.InspectionCount).OrderByDescending(o => o).Take(2).Aggregate((Int64 sum, Int64 next) => sum * next);
        }

        private class Monkey
        {
            public List<Int64> ItemList { get; set; }
            public string OperationLeft { get; set; }
            public string OperationRight { get; set; }
            public string OperationSign { get; set; }
            public int TestDivider { get; set; }
            public int TrueMonkeyIdThrow { get; set; }
            public int FalseMonkeyIdThrow { get; set; }
            public int InspectionCount { get; set; }
        }

        [TestMethod]
        public void Day12_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day12.txt").ToList();
            List<Location> locationList = new List<Location>();
            int x = 0;
            int y = 0;
            foreach (string input in inputList)
            {
                x = 0;
                foreach (char height in input)
                {
                    locationList.Add(new Location { X = x, Y = y, Height = height == 'S' ? 'a' : height == 'E' ? 'z' : height, IsEnd = height == 'E', IsVisited = false, MinDistance = height == 'S' ? 0 : int.MaxValue });
                    x++;
                }
                y++;
            }
            Location current = null;
            while (current == null || !current.IsEnd)
            {
                current = locationList.Where(w => !w.IsVisited).OrderBy(o => o.MinDistance).First();
                current.IsVisited = true;
                locationList.Where(w => (Math.Abs(w.X - current.X) + Math.Abs(w.Y - current.Y)) == 1 && current.Height >= (w.Height - 1) && w.MinDistance > current.MinDistance)
                        .ToList().ForEach(e => e.MinDistance = current.MinDistance + 1);
            }
        }

        [TestMethod]
        public void Day12_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day12.txt").ToList();
            List<Location> locationList = new List<Location>();
            int x = 0;
            int y = 0;
            foreach (string input in inputList)
            {
                x = 0;
                foreach (char height in input)
                {
                    locationList.Add(new Location { X = x, Y = y, Height = height == 'S' ? 'a' : height == 'E' ? 'z' : height, IsEnd = height == 'E', IsVisited = false, MinDistance = int.MaxValue });
                    x++;
                }
                y++;
            }
            List<int> minDistanceList = new List<int>();
            Location current = null;
            foreach (Location start in locationList.Where(w => w.Height == 'a'))
            {
                start.MinDistance = 0;
                current = null;
                while (current == null || !current.IsEnd)
                {
                    current = locationList.Where(w => !w.IsVisited).OrderBy(o => o.MinDistance).First();
                    if (current.MinDistance == int.MaxValue)
                        break;
                    current.IsVisited = true;
                    locationList.Where(w => (Math.Abs(w.X - current.X) + Math.Abs(w.Y - current.Y)) == 1 && current.Height >= (w.Height - 1) && w.MinDistance > current.MinDistance)
                        .ToList().ForEach(e => e.MinDistance = current.MinDistance + 1);
                }
                minDistanceList.Add(current.MinDistance);
                locationList.ForEach(e =>
                {
                    e.MinDistance = int.MaxValue;
                    e.IsVisited = false;
                });
            }
            int minStart = minDistanceList.Min();
        }

        private class Location
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public bool IsEnd { get; set; }
            public bool IsVisited { get; set; }
            public int MinDistance { get; set; }
        }

        [TestMethod]
        public void Day13_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day13.txt").ToList();
            List<PacketPair> packetPairList = new List<PacketPair>();
            int pairCounter = 0;
            for (int i = 0; i < inputList.Count(); i += 3)
            {
                pairCounter++;
                packetPairList.Add(new PacketPair
                {
                    Index = pairCounter,
                    LeftList = ParsePacketValueList(inputList[i].Substring(1, inputList[i].Length - 2)),
                    RightList = ParsePacketValueList(inputList[i + 1].Substring(1, inputList[i + 1].Length - 2)),
                });
            }

            //PrintPackages1(packetPairList);

            int indicesSum = 0;
            List<int> indexList = new List<int>();
            foreach (PacketPair packetPair in packetPairList)
            {
                int result = ComparePackets(packetPair.LeftList, packetPair.RightList);
                if (result == 1)
                {
                    indicesSum += packetPair.Index;
                    indexList.Add(packetPair.Index);
                }
            }
        }

        [TestMethod]
        public void Day13_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day13.txt").ToList();
            List<Packet> packetList = new List<Packet>();
            foreach (string input in inputList)
            {
                if (string.IsNullOrEmpty(input))
                    continue;
                packetList.Add(new Packet { PacketValueList = ParsePacketValueList(input.Substring(1, input.Length - 2)) });
            }
            List<Packet> orderedPacketPairList = new List<Packet>();
            packetList.Add(new Packet { IsDecoderPacket = true, PacketValueList = ParsePacketValueList("[2]") });
            packetList.Add(new Packet { IsDecoderPacket = true, PacketValueList = ParsePacketValueList("[6]") });
            foreach (Packet packet in packetList)
            {
                if (orderedPacketPairList.Count() == 0)
                {
                    packet.Index = 1;
                    orderedPacketPairList.Add(packet);
                }
                else
                {
                    packet.Index = orderedPacketPairList.Max(m => m.Index) + 1;
                    foreach (Packet orderedPacket in orderedPacketPairList.OrderBy(o => o.Index))
                    {
                        if (ComparePackets(packet.PacketValueList, orderedPacket.PacketValueList) == 1)
                        {
                            packet.Index = orderedPacket.Index;
                            break;
                        }
                    }
                    orderedPacketPairList.Where(w => w.Index >= packet.Index).ToList().ForEach(e => e.Index++);
                    orderedPacketPairList.Add(packet);
                }
            }
            int decoderKey = (int)orderedPacketPairList.Where(w => w.IsDecoderPacket).Select(s => s.Index).Aggregate((sum, next) => sum * next);
            //PrintPackages2(orderedPacketPairList.OrderBy(o => o.Index).ToList());
        }

        private void PrintPackages1(List<PacketPair> packetPairList)
        {
            foreach (PacketPair packetPair in packetPairList)
            {
                Debug.WriteLine(GetPacketString(packetPair.LeftList));
                Debug.WriteLine(GetPacketString(packetPair.RightList));
                Debug.WriteLine(string.Empty);
            }
        }

        private void PrintPackages2(List<Packet> packetList)
        {
            foreach (Packet packet in packetList)
                Debug.WriteLine(GetPacketString(packet.PacketValueList));
        }

        private string GetPacketString(List<PacketValue> packetValueList)
        {
            string packetString = "[";
            bool isFirst = true;
            foreach (PacketValue packetValue in packetValueList)
            {
                if (!isFirst)
                    packetString += ",";
                if (packetValue.Value != null)
                    packetString += packetValue.Value;
                else if (packetValue.PacketValueList != null)
                    packetString += GetPacketString(packetValue.PacketValueList);
                isFirst = false;
            }
            packetString += "]";
            return packetString;
        }

        private int ComparePackets(List<PacketValue> leftList, List<PacketValue> rightList)
        {
            int result = -1; // 0 = false, 1 = true, 2 = continue; -1 = error
            if (leftList.Count() == 0 && rightList.Count() == 0)
                result = 2;
            else if (leftList.Count() == 0)
                result = 1;
            else if (rightList.Count() == 0)
                result = 0;
            else
            {
                for (int i = 0; i < Math.Max(leftList.Count(), rightList.Count()); i++)
                {
                    if (i >= rightList.Count())
                        result = 0;
                    else if (i >= leftList.Count())
                        result = 1;
                    else
                    {
                        PacketValue left = leftList[i];
                        PacketValue right = rightList[i];
                        if (left.Value != null && right.Value != null)
                        {
                            if (left.Value < right.Value)
                                result = 1;
                            else if (left.Value > right.Value)
                                result = 0;
                            else
                                result = 2;
                        }
                        else if (left.Value != null)
                        {
                            if (right.PacketValueList.Count() == 0 || right.PacketValueList == null)
                                result = 0;
                            else
                                result = ComparePackets(new List<PacketValue> { new PacketValue { Value = left.Value } }, right.PacketValueList);
                        }
                        else if (right.Value != null)
                        {
                            if (left.PacketValueList.Count() == 0 || left.PacketValueList == null)
                                result = 1;
                            else
                                result = ComparePackets(left.PacketValueList, new List<PacketValue> { new PacketValue { Value = right.Value } });
                        }
                        else
                        {
                            if ((left.PacketValueList.Count() == 0 || left.PacketValueList == null) && (right.PacketValueList.Count() == 0 || right.PacketValueList == null))
                                result = 2;
                            else if (left.PacketValueList.Count() == 0 || left.PacketValueList == null)
                                result = 1;
                            else if (right.PacketValueList.Count() == 0 || right.PacketValueList == null)
                                result = 0;
                            else
                                result = ComparePackets(left.PacketValueList, right.PacketValueList);
                        }
                    }
                    if (result != 2)
                        break;
                }
            }
            if (result == -1)
                throw new Exception("Invalid result");
            return result;
        }

        private List<PacketValue> ParsePacketValueList(string data)
        {
            //Debug.Print(data);
            List<PacketValue> packetValueList = new List<PacketValue>();
            for (int i = 0; i < data.Length; i++)
            {
                char c = data[i];
                if (c == '[')
                {
                    int closeIndex = FindCloseListIndex(data.Substring(i));
                    packetValueList.Add(new PacketValue { PacketValueList = ParsePacketValueList(data.Substring(i + 1, closeIndex - 1)) });
                    i = i + closeIndex;
                }
                else if (char.IsDigit(c))
                {
                    int intEndIndex = FindEndIndexOfInteger(data.Substring(i));
                    packetValueList.Add(new PacketValue { Value = int.Parse(data.Substring(i, intEndIndex)) });
                    i = i + intEndIndex - 1;
                }
                else if (c == ']')
                {
                    packetValueList.Add(new PacketValue { });
                }
            }
            //Debug.Print("Complete");
            return packetValueList;
        }

        private int FindEndIndexOfInteger(string data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (!char.IsDigit(data[i]))
                    return i;
            }
            return data.Length;
        }

        private int FindCloseListIndex(string data)
        {
            int closeCounter = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == '[')
                    closeCounter++;
                else if (data[i] == ']')
                {
                    closeCounter--;
                    if (closeCounter == 0)
                        return i;
                }
            }
            throw new Exception("Cannot find list closer");
        }

        private class PacketPair
        {
            public List<PacketValue> LeftList { get; set; }
            public List<PacketValue> RightList { get; set; }
            public int Index { get; set; }
        }

        private class Packet
        {
            public Packet()
            {
                IsDecoderPacket = false;
            }
            public List<PacketValue> PacketValueList { get; set; }
            public int? Index { get; set; }
            public bool IsDecoderPacket { get; set; }
        }

        private class PacketValue
        {
            public List<PacketValue> PacketValueList { get; set; }
            public int? Value { get; set; }
        }

        [TestMethod]
        public void Day14_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day14.txt").ToList();
            List<Coordinate> coordinateList = new List<Coordinate>();
            foreach (string input in inputList)
            {
                List<string> inputSplit = input.Split(" -> ").ToList();
                string start = string.Empty;
                foreach (string end in inputSplit)
                {
                    if (!string.IsNullOrEmpty(start))
                    {
                        List<string> startSplit = start.Split(',').ToList();
                        List<string> endSplit = end.Split(',').ToList();
                        int startX = int.Parse(startSplit[0]);
                        int startY = int.Parse(startSplit[1]);
                        int endX = int.Parse(endSplit[0]);
                        int endY = int.Parse(endSplit[1]);
                        if (startX != endX)
                        {
                            for (int i = Math.Min(startX, endX); i <= Math.Max(startX, endX); i++)
                            {
                                Coordinate startCoordinate = coordinateList.FirstOrDefault(w => w.X == i && w.Y == startY);
                                if (startCoordinate == null)
                                {
                                    startCoordinate = new Coordinate { X = i, Y = startY };
                                    coordinateList.Add(startCoordinate);
                                }
                                startCoordinate.Rock = true;
                            }
                        }
                        else
                        {
                            for (int i = Math.Min(startY, endY); i <= Math.Max(startY, endY); i++)
                            {
                                Coordinate startCoordinate = coordinateList.FirstOrDefault(w => w.X == startX && w.Y == i);
                                if (startCoordinate == null)
                                {
                                    startCoordinate = new Coordinate { X = startX, Y = i };
                                    coordinateList.Add(startCoordinate);
                                }
                                startCoordinate.Rock = true;
                            }
                        }
                    }
                    start = end;
                }
            }
            int maxDepth = coordinateList.Max(m => m.Y);
            bool endReached = false;
            while (!endReached)
            {
                int currentX = 500;
                int currentY = 0;
                bool sandEndReached = false;
                while (!sandEndReached)
                {
                    if (maxDepth + 10 <= currentY)
                    {
                        sandEndReached = true;
                        endReached = true;
                        break;
                    }
                    Coordinate next = coordinateList.FirstOrDefault(w => w.X == currentX && w.Y == currentY + 1 && (w.Sand || w.Rock));
                    if (next == null)
                    {
                        currentY = currentY + 1;
                        continue;
                    }
                    next = coordinateList.FirstOrDefault(w => w.X == currentX - 1 && w.Y == currentY + 1 && (w.Sand || w.Rock));
                    if (next == null)
                    {
                        currentY = currentY + 1;
                        currentX = currentX - 1;
                        continue;
                    }
                    next = coordinateList.FirstOrDefault(w => w.X == currentX + 1 && w.Y == currentY + 1 && (w.Sand || w.Rock));
                    if (next == null)
                    {
                        currentY = currentY + 1;
                        currentX = currentX + 1;
                        continue;
                    }
                    else
                    {
                        coordinateList.Add(new Coordinate { X = currentX, Y = currentY, Sand = true });
                        sandEndReached = true;
                    }
                }
            }
            PrintCave(coordinateList);
            int sandCount = coordinateList.Where(w => w.Sand).Count();
        }

        [TestMethod]
        public void Day14_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day14.txt").ToList();
            List<Coordinate> coordinateList = new List<Coordinate>();
            foreach (string input in inputList)
            {
                List<string> inputSplit = input.Split(" -> ").ToList();
                string start = string.Empty;
                foreach (string end in inputSplit)
                {
                    if (!string.IsNullOrEmpty(start))
                    {
                        List<string> startSplit = start.Split(',').ToList();
                        List<string> endSplit = end.Split(',').ToList();
                        int startX = int.Parse(startSplit[0]);
                        int startY = int.Parse(startSplit[1]);
                        int endX = int.Parse(endSplit[0]);
                        int endY = int.Parse(endSplit[1]);
                        if (startX != endX)
                        {
                            for (int i = Math.Min(startX, endX); i <= Math.Max(startX, endX); i++)
                            {
                                Coordinate startCoordinate = coordinateList.FirstOrDefault(w => w.X == i && w.Y == startY);
                                if (startCoordinate == null)
                                {
                                    startCoordinate = new Coordinate { X = i, Y = startY };
                                    coordinateList.Add(startCoordinate);
                                }
                                startCoordinate.Rock = true;
                            }
                        }
                        else
                        {
                            for (int i = Math.Min(startY, endY); i <= Math.Max(startY, endY); i++)
                            {
                                Coordinate startCoordinate = coordinateList.FirstOrDefault(w => w.X == startX && w.Y == i);
                                if (startCoordinate == null)
                                {
                                    startCoordinate = new Coordinate { X = startX, Y = i };
                                    coordinateList.Add(startCoordinate);
                                }
                                startCoordinate.Rock = true;
                            }
                        }
                    }
                    start = end;
                }
            }
            int maxDepth = coordinateList.Max(m => m.Y);
            for (int x = 500 - (maxDepth + 5); x <= 500 + (maxDepth + 5); x++)
                coordinateList.Add(new Coordinate { X = x, Y = maxDepth + 2, Rock = true });
            bool endReached = false;
            while (!endReached)
            {
                int currentX = 500;
                int currentY = 0;
                bool sandEndReached = false;
                while (!sandEndReached)
                {
                    Coordinate next = coordinateList.FirstOrDefault(w => w.X == currentX && w.Y == currentY + 1 && (w.Sand || w.Rock));
                    if (next == null)
                    {
                        currentY = currentY + 1;
                        continue;
                    }
                    next = coordinateList.FirstOrDefault(w => w.X == currentX - 1 && w.Y == currentY + 1 && (w.Sand || w.Rock));
                    if (next == null)
                    {
                        currentY = currentY + 1;
                        currentX = currentX - 1;
                        continue;
                    }
                    next = coordinateList.FirstOrDefault(w => w.X == currentX + 1 && w.Y == currentY + 1 && (w.Sand || w.Rock));
                    if (next == null)
                    {
                        currentY = currentY + 1;
                        currentX = currentX + 1;
                        continue;
                    }
                    else
                    {
                        coordinateList.Add(new Coordinate { X = currentX, Y = currentY, Sand = true });
                        sandEndReached = true;
                        if (currentY == 0)
                        {
                            endReached = true;
                            break;
                        }
                    }
                }
            }
            PrintCave(coordinateList);
            int sandCount = coordinateList.Where(w => w.Sand).Count();
        }

        private void PrintCave(List<Coordinate> coordinateList)
        {
            for (int y = 0; y <= coordinateList.Max(m => m.Y) + 5; y++)
            {
                string line = string.Empty;
                for (int x = coordinateList.Min(m => m.X) - 5; x <= coordinateList.Max(m => m.X) + 5; x++)
                {
                    Coordinate coordinate = coordinateList.FirstOrDefault(w => w.X == x && w.Y == y);
                    if (x == 500 && y == 0)
                        line += "+";
                    else if (coordinate == null)
                        line += ".";
                    else if (coordinate.Rock)
                        line += "#";
                    else if (coordinate.Sand)
                        line += "O";
                }
                Debug.Print(line);
            }
        }

        private class Coordinate
        {
            public Coordinate()
            {
                Sand = false;
                Rock = false;
            }
            public int X { get; set; }
            public int Y { get; set; }
            public bool Sand { get; set; }
            public bool Rock { get; set; }
        }

        [TestMethod]
        public void Day15_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day15.txt").ToList();
            List<Day15Coordinate> deviceList = new List<Day15Coordinate>();
            foreach (string input in inputList)
            {
                List<string> inputSplit = input.Split('=').ToList();
                int sensorX = int.Parse(inputSplit[1].Substring(0, inputSplit[1].Length - 3));
                int sensorY = int.Parse(inputSplit[2].Substring(0, inputSplit[2].Length - 24));
                int beaconX = int.Parse(inputSplit[3].Substring(0, inputSplit[3].Length - 3));
                int beaconY = int.Parse(inputSplit[4]);
                Sensor sensor = new Sensor { X = sensorX, Y = sensorY };
                Day15Coordinate beacon = deviceList.FirstOrDefault(w => w.X == beaconX && w.Y == beaconY);
                if (beacon == null)
                {
                    beacon = new Beacon { X = beaconX, Y = beaconY };
                    deviceList.Add(beacon);
                }
                sensor.Beacon = (Beacon)beacon;
                deviceList.Add(sensor);
            }

            List<Range> rangeList = new List<Range>();
            int row = 2000000;
            foreach (Sensor sensor in deviceList.Where(w => w is Sensor))
            {
                int distance = Math.Abs(sensor.X - sensor.Beacon.X) + Math.Abs(sensor.Y - sensor.Beacon.Y);
                if ((sensor.Y - distance) <= row && (sensor.Y + distance) >= row)
                {
                    int xDistance = distance - Math.Abs(row - sensor.Y);
                    int xMin = sensor.X - xDistance;
                    int xMax = sensor.X + xDistance;
                    rangeList.Add(new Range { Start = xMin, End = xMax });
                }
            }
            int count = 0;
            for (int i = rangeList.Min(m => m.Start); i <= rangeList.Max(m => m.End); i++)
            {
                if (rangeList.Any(a => a.Start <= i && a.End >= i) && !deviceList.Any(w => w.X == i && w.Y == row))
                    count++;
            }
        }

        [TestMethod]
        public void Day15_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day15.txt").ToList();
            List<Day15Coordinate> deviceList = new List<Day15Coordinate>();
            foreach (string input in inputList)
            {
                List<string> inputSplit = input.Split('=').ToList();
                int sensorX = int.Parse(inputSplit[1].Substring(0, inputSplit[1].Length - 3));
                int sensorY = int.Parse(inputSplit[2].Substring(0, inputSplit[2].Length - 24));
                int beaconX = int.Parse(inputSplit[3].Substring(0, inputSplit[3].Length - 3));
                int beaconY = int.Parse(inputSplit[4]);
                Sensor sensor = new Sensor { X = sensorX, Y = sensorY };
                Day15Coordinate beacon = deviceList.FirstOrDefault(w => w.X == beaconX && w.Y == beaconY);
                if (beacon == null)
                    beacon = new Beacon { X = beaconX, Y = beaconY };
                sensor.Beacon = (Beacon)beacon;
                deviceList.Add(sensor);
            }
            Int64 tuningFrequency = 0;
            int maxXY = 4000000;
            List<Range> rangeList = new List<Range>();
            for (int y = 0; y <= maxXY; y++)
            {
                rangeList.Clear();
                foreach (Sensor sensor in deviceList.Where(w => w is Sensor))
                {
                    int distance = Math.Abs(sensor.X - sensor.Beacon.X) + Math.Abs(sensor.Y - sensor.Beacon.Y);
                    if ((sensor.Y - distance) <= y && (sensor.Y + distance) >= y)
                    {
                        int xDistance = distance - Math.Abs(y - sensor.Y);
                        int xMin = sensor.X - xDistance;
                        int xMax = sensor.X + xDistance;
                        rangeList.Add(new Range { Start = xMin, End = xMax });
                    }
                }
                for (int x = 0; x <= maxXY; x++)
                {
                    int max = rangeList.Where(w => w.Start <= x && w.End >= x).Select(s => s.End).DefaultIfEmpty(-1).Max();
                    if (max == -1)
                        tuningFrequency = (Int64)x * 4000000 + (Int64)y;
                    else
                        x = max;
                }
            }
        }

        private class Day15Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private class Beacon : Day15Coordinate
        {

        }

        private class Sensor : Day15Coordinate
        {
            public Beacon Beacon { get; set; }
        }

        private class Range
        {
            public int Start { get; set; }
            public int End { get; set; }
        }

        [TestMethod]
        public void Day16_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day16.txt").ToList();
            List<Valve> valveList = new List<Valve>();
            int valveIndex = 0;
            foreach (string input in inputList)
            {
                List<string> inputSplit = input.Split(' ').ToList();
                string name = inputSplit[1];
                Valve valve = valveList.FirstOrDefault(w => w.Name == name);
                if (valve == null)
                {
                    valve = new Valve { Name = name, Index = valveIndex++ };
                    valveList.Add(valve);
                }
                valve.FlowRate = int.Parse(inputSplit[4].Substring(5).TrimEnd(';'));
                List<string> neighbourValveNameList = input.Split("valve")[1].TrimStart('s').Trim().Split(", ").ToList();
                foreach (string neighbourName in neighbourValveNameList)
                {
                    Valve neighbourValve = valveList.FirstOrDefault(w => w.Name == neighbourName);
                    if (neighbourValve == null)
                    {
                        neighbourValve = new Valve { Name = neighbourName, Index = valveIndex++ };
                        valveList.Add(neighbourValve);
                    }
                    valve.NeighourValveList.Add(neighbourValve);
                }
            }
            // I made two solutions for part 1. They both works. I made the second one also since the first solution was not useful for part 2
            RecordFlow recordFlow = new RecordFlow();
            CheckValvePart1(valveList.FirstOrDefault(w => w.Name == "AA"), String.Empty, 31, 0, 0, valveList.Sum(s => s.FlowRate), string.Empty, recordFlow); // start at 31 since i also include movement to AA
            int maxFlowRate = recordFlow.MaxFlow;

            // This is the second solution and its faster (not very noticeable in part 1)
            int?[,] dist = FloydMarshal(valveList);
            recordFlow = new RecordFlow { MaxFlow = 0 };
            CheckValvePart1_2(dist, valveList.Where(w => w.FlowRate > 0).ToList(), valveList.First(w => w.Name == "AA"), 30, string.Empty, 0, 0, recordFlow);
            maxFlowRate = recordFlow.MaxFlow;
        }

        private void CheckValvePart1(Valve current, string combination, int minutesLeft, int currentFlow, int totalFlow, int maxPossibleFlow, string previous, RecordFlow recordFlow)
        {
            if (minutesLeft <= 0) // time is out
            {
                if (totalFlow > recordFlow.MaxFlow)
                    recordFlow.MaxFlow = totalFlow;
                return;
            }
            if ((totalFlow + minutesLeft * currentFlow + (maxPossibleFlow - currentFlow) * minutesLeft) < recordFlow.MaxFlow)
                return;
            combination += current.Name;
            minutesLeft--; // move to this valve
            totalFlow += currentFlow;
            if (current.FlowRate > 0 && minutesLeft > 0 && !combination.Contains(current.Name + "#")) // turn current valve
            {
                int turnValveMinutesLeft = minutesLeft - 1;
                string turnValveCombination = combination + "#";
                int turnValveCurrentFlow = currentFlow + current.FlowRate;
                int turnValveTotalFlow = totalFlow + currentFlow;
                foreach (Valve neighbour in current.NeighourValveList)
                    CheckValvePart1(neighbour, turnValveCombination, turnValveMinutesLeft, turnValveCurrentFlow, turnValveTotalFlow, maxPossibleFlow, current.Name, recordFlow);
            }
            foreach (Valve neighbour in current.NeighourValveList.Where(w => w.Name != previous)) // Dont turn the valve
                CheckValvePart1(neighbour, combination, minutesLeft, currentFlow, totalFlow, maxPossibleFlow, current.Name, recordFlow);
        }

        private void CheckValvePart1_2(int?[,] dist, List<Valve> valveList, Valve current, int minutesLeft, string turnedValves, int currentFlow, int totalFlow, RecordFlow recordFlow)
        {
            List<Valve> nextValveList = valveList.Where(w => !turnedValves.Contains(w.Name) && minutesLeft >= ((int)dist[current.Index, w.Index] + 1)).ToList();
            if (!nextValveList.Any()) // no possible next valves
            {
                totalFlow = totalFlow + currentFlow * minutesLeft;
                if (recordFlow.MaxFlow < totalFlow)
                {
                    recordFlow.MaxFlow = totalFlow;
                    Debug.WriteLine(turnedValves + " " + totalFlow);
                }
                return;
            }
            foreach (Valve valve in nextValveList)
            {
                int travelTime = ((int)dist[current.Index, valve.Index] + 1); // +1 for valve turn
                CheckValvePart1_2(dist, valveList, valve, minutesLeft - travelTime, turnedValves + valve.Name
                    , currentFlow + valve.FlowRate, totalFlow + travelTime * currentFlow, recordFlow);
            }
        }

        [TestMethod]
        public void Day16_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day16.txt").ToList();
            List<Valve> valveList = new List<Valve>();
            int valveIndex = 0;
            foreach (string input in inputList)
            {
                List<string> inputSplit = input.Split(' ').ToList();
                string name = inputSplit[1];
                Valve valve = valveList.FirstOrDefault(w => w.Name == name);
                if (valve == null)
                {
                    valve = new Valve { Name = name, Index = valveIndex++ };
                    valveList.Add(valve);
                }
                valve.FlowRate = int.Parse(inputSplit[4].Substring(5).TrimEnd(';'));
                List<string> neighbourValveNameList = input.Split("valve")[1].TrimStart('s').Trim().Split(", ").ToList();
                foreach (string neighbourName in neighbourValveNameList)
                {
                    Valve neighbourValve = valveList.FirstOrDefault(w => w.Name == neighbourName);
                    if (neighbourValve == null)
                    {
                        neighbourValve = new Valve { Name = neighbourName, Index = valveIndex++ };
                        valveList.Add(neighbourValve);
                    }
                    valve.NeighourValveList.Add(neighbourValve);
                }
            }
            int?[,] dist = FloydMarshal(valveList);
            RecordFlow recordFlow = new RecordFlow { MaxFlow = 0 };
            int minutes = 27;
            ValveSimulator(dist, valveList.Where(w => w.FlowRate > 0).ToList(), valveList.First(w => w.Name == "AA"), valveList.First(w => w.Name == "AA"), minutes,
                1, 1, string.Empty, 0, 0, recordFlow, string.Empty, valveList.Sum(s => s.FlowRate));
            int maxFlowRate = recordFlow.MaxFlow;
        }

        private int?[,] FloydMarshal(List<Valve> valveList)
        {
            int v = valveList.Count();
            int?[,] dist = new int?[v, v];
            for (int i = 0; i < v; i++)
                dist[i, i] = 0;
            foreach (Valve valve in valveList)
                foreach (Valve neighbour in valve.NeighourValveList)
                    dist[valve.Index, neighbour.Index] = 1;
            for (int k = 0; k < v; k++)
                for (int i = 0; i < v; i++)
                    for (int j = 0; j < v; j++)
                        if ((dist[i, j] ?? int.MaxValue) > dist[i, k] + dist[k, j])
                            dist[i, j] = dist[i, k] + dist[k, j];
            return dist;
        }

        private void Day16PrintDist(int?[,] dist, int length)
        {
            for (int i = 0; i < length; i++)
            {
                string row = string.Empty;
                for (int k = 0; k < length; k++)
                    row += string.IsNullOrEmpty(dist[i, k].ToString()) ? " " : dist[i, k];
                Debug.WriteLine(row);
            }
        }

        private void ValveSimulator(int?[,] dist, List<Valve> valveList, Valve humanDestination, Valve elephantDestination, int minutesLeft
            , int humanTravelTime, int elephantTravelTime, string turnedValves, int currentFlow, int totalFlow, RecordFlow recordFlow
            , string actualOpenVales, int finalMaxFlow)
        {
            minutesLeft--;
            humanTravelTime--;
            elephantTravelTime--;
            totalFlow += currentFlow;
            List<Valve> nextHumanValveList = valveList.Where(w => !turnedValves.Contains(w.Name) && minutesLeft >= ((int)dist[humanDestination.Index, w.Index] + 1)).ToList();
            List<Valve> nextElephantValveList = valveList.Where(w => !turnedValves.Contains(w.Name) && minutesLeft >= ((int)dist[elephantDestination.Index, w.Index] + 1)).ToList();
            if (totalFlow + finalMaxFlow * minutesLeft <= recordFlow.MaxFlow) // Ends path early since it cant reach the record. Speeds it up alot
                return;
            if (minutesLeft == 0)
            {
                if (recordFlow.MaxFlow < totalFlow)
                {
                    recordFlow.MaxFlow = totalFlow;
                    Debug.WriteLine(turnedValves + " " + totalFlow);
                }
                return;
            }
            if (humanTravelTime == 0 && elephantTravelTime == 0) // both needs new destinations
            {
                currentFlow += humanDestination.FlowRate + elephantDestination.FlowRate;
                actualOpenVales += humanDestination.Name + elephantDestination.Name;
                if (!nextHumanValveList.Any() && !nextElephantValveList.Any()) // no new destinations possible. Calculate end result and stop path
                {
                    totalFlow += currentFlow * minutesLeft;
                    if (recordFlow.MaxFlow < totalFlow)
                    {
                        recordFlow.MaxFlow = totalFlow;
                        Debug.WriteLine(turnedValves + " " + totalFlow);
                    }
                    return;
                }
                foreach (Valve nextHumanValve in nextHumanValveList)
                {
                    foreach (Valve nextElephantValve in nextElephantValveList.Where(w => w.Name != nextHumanValve.Name))
                    {
                        int hTravelTime = ((int)dist[humanDestination.Index, nextHumanValve.Index] + 1); // +1 for valve turn
                        int eTravelTime = ((int)dist[elephantDestination.Index, nextElephantValve.Index] + 1); // +1 for valve turn
                        ValveSimulator(dist, valveList, nextHumanValve, nextElephantValve, minutesLeft, hTravelTime, eTravelTime
                            , turnedValves + nextHumanValve.Name + nextElephantValve.Name, currentFlow, totalFlow, recordFlow, actualOpenVales, finalMaxFlow);
                    }
                }
            }
            else if (humanTravelTime == 0) // human needs a new destination
            {
                currentFlow += humanDestination.FlowRate;
                actualOpenVales += humanDestination.Name;
                foreach (Valve nextValve in nextHumanValveList)
                {
                    int travelTime = ((int)dist[humanDestination.Index, nextValve.Index] + 1); // +1 for valve turn
                    ValveSimulator(dist, valveList, nextValve, elephantDestination, minutesLeft, travelTime, elephantTravelTime, turnedValves + nextValve.Name, currentFlow, totalFlow, recordFlow, actualOpenVales, finalMaxFlow);
                }
                if (!nextHumanValveList.Any())
                    ValveSimulator(dist, valveList, humanDestination, elephantDestination, minutesLeft, humanTravelTime, elephantTravelTime, turnedValves, currentFlow, totalFlow, recordFlow, actualOpenVales, finalMaxFlow);
            }
            else if (elephantTravelTime == 0) // elephant needs a new destination
            {
                currentFlow += elephantDestination.FlowRate;
                actualOpenVales += elephantDestination.Name;
                foreach (Valve nextValve in nextElephantValveList)
                {
                    int travelTime = ((int)dist[elephantDestination.Index, nextValve.Index] + 1); // +1 for valve turn
                    ValveSimulator(dist, valveList, humanDestination, nextValve, minutesLeft, humanTravelTime, travelTime, turnedValves + nextValve.Name, currentFlow, totalFlow, recordFlow, actualOpenVales, finalMaxFlow);
                }
                if (!nextElephantValveList.Any())
                    ValveSimulator(dist, valveList, humanDestination, elephantDestination, minutesLeft, humanTravelTime, elephantTravelTime, turnedValves, currentFlow, totalFlow, recordFlow, actualOpenVales, finalMaxFlow);
            }
            else // both are still traveling
            {
                ValveSimulator(dist, valveList, humanDestination, elephantDestination, minutesLeft, humanTravelTime, elephantTravelTime, turnedValves, currentFlow, totalFlow, recordFlow, actualOpenVales, finalMaxFlow);
            }
        }

        private class Valve
        {
            public Valve()
            {
                NeighourValveList = new List<Valve>();
            }
            public int Index { get; set; }
            public string Name { get; set; }
            public int FlowRate { get; set; }
            public List<Valve> NeighourValveList { get; set; }
        }

        private class RecordFlow
        {
            public int MaxFlow { get; set; }
        }

        [TestMethod]
        public void Day17_1()
        {
            string inputList = File.ReadAllText(@"Input\Day17.txt");
            int jetStreamLength = inputList.Length;
            List<Day17Coordinate> cave = new List<Day17Coordinate>();
            List<Day17Coordinate> shape;
            int jetStreamCounter = 0;
            for (int i = 0; i < 2022; i++)
            {
                shape = CreateShape(2, (cave.Max(m => (int?)m.Y) ?? -1) + 4, i % 5);
                //Day17PrintCave(cave, shape);
                bool fall = false;
                bool shapeEnd = false;
                while (!shapeEnd)
                {
                    if (!fall) // jet stream
                    {
                        char jetStream = inputList[jetStreamCounter % jetStreamLength];
                        bool moveAllowed = true;
                        for (int y = shape.Min(m => m.Y); y <= shape.Max(m => m.Y); y++)
                        {
                            int newX;
                            if (jetStream == '<') // Move left
                                newX = shape.Where(w => w.Y == y).Min(m => m.X) - 1;
                            else // Move right
                                newX = shape.Where(w => w.Y == y).Max(m => m.X) + 1;
                            if (cave.Any(a => a.X == newX && a.Y == y) || newX < 0 || newX > 6)
                                moveAllowed = false;
                        }
                        if (moveAllowed)
                        {
                            int moveX = jetStream == '<' ? -1 : 1;
                            shape.ForEach(e => e.X += moveX);
                        }
                        jetStreamCounter++;
                    }
                    else // fall
                    {
                        bool moveAllowed = true;
                        for (int x = shape.Min(m => m.X); x <= shape.Max(m => m.X); x++)
                        {
                            int newY = shape.Where(w => w.X == x).Min(m => m.Y) - 1;
                            if (cave.Any(a => a.Y == newY && a.X == x) || newY < 0)
                                moveAllowed = false;
                        }
                        if (moveAllowed)
                            shape.ForEach(e => e.Y -= 1);
                        else // shape end position
                        {
                            foreach (Day17Coordinate coordinate in shape)
                                cave.Add(coordinate);
                            shapeEnd = true;
                        }
                    }
                    fall = !fall;
                    //Day17PrintCave(cave, shape);
                }
                //Day17PrintCave(cave, shape);
            }

            int towerHeight = cave.Max(m => m.Y) + 1;
        }

        [TestMethod]
        public void Day17_2()
        {
            string inputList = File.ReadAllText(@"Input\Day17.txt");
            Day17TowerResult towerResult = CalculateTowerHeight(inputList, 6000);
            Int64 patternTotalRocks = (Int64)1000000000000 / (Int64)towerResult.PatternRocks;
            Int64 patternTotalHeight = patternTotalRocks * (Int64)towerResult.PatternHeight;
            Int64 remainingRocks = ((Int64)1000000000000 % (Int64)towerResult.PatternRocks);
            towerResult = CalculateTowerHeight(inputList, (int)remainingRocks);
            Int64 finalHeight = patternTotalHeight + towerResult.Height;
        }

        private Day17TowerResult CalculateTowerHeight(string inputList, int rocks)
        {
            Day17TowerResult towerResult = new Day17TowerResult();
            int jetStreamLength = inputList.Length;
            List<Day17Coordinate> cave = new List<Day17Coordinate>();
            List<Day17Coordinate> shape;
            int jetStreamCounter = 0;
            List<int> heightList = new List<int>();
            for (int i = 0; i < rocks; i++)
            {
                int shapeId = i % 5;
                shape = CreateShape(2, (cave.Max(m => (int?)m.Y) ?? -1) + 4, i % 5);
                //Day17PrintCave(cave, shape);
                bool fall = false;
                bool shapeEnd = false;
                while (!shapeEnd)
                {
                    if (!fall) // jet stream
                    {
                        char jetStream = inputList[jetStreamCounter % jetStreamLength];
                        bool moveAllowed = true;
                        for (int y = shape.Min(m => m.Y); y <= shape.Max(m => m.Y); y++)
                        {
                            int newX;
                            if (jetStream == '<') // Move left
                                newX = shape.Where(w => w.Y == y).Min(m => m.X) - 1;
                            else // Move right
                                newX = shape.Where(w => w.Y == y).Max(m => m.X) + 1;
                            if (cave.Any(a => a.X == newX && a.Y == y) || newX < 0 || newX > 6)
                                moveAllowed = false;
                        }
                        if (moveAllowed)
                        {
                            int moveX = jetStream == '<' ? -1 : 1;
                            shape.ForEach(e => e.X += moveX);
                        }
                        jetStreamCounter++;
                    }
                    else // fall
                    {
                        bool moveAllowed = true;
                        for (int x = shape.Min(m => m.X); x <= shape.Max(m => m.X); x++)
                        {
                            int newY = shape.Where(w => w.X == x).Min(m => m.Y) - 1;
                            if (cave.Any(a => a.Y == newY && a.X == x) || newY < 0)
                                moveAllowed = false;
                        }
                        if (moveAllowed)
                            shape.ForEach(e => e.Y -= 1);
                        else // shape end position
                        {
                            foreach (Day17Coordinate coordinate in shape)
                                cave.Add(coordinate);
                            shapeEnd = true;
                        }
                    }
                    fall = !fall;
                    //Day17PrintCave(cave, shape);
                }

                if (cave.Max(m => m.Y) > 100)
                    if (towerResult.PatternRocks == null)
                        FindPattern(cave, towerResult, i + 1);
                //Day17PrintCave(cave, shape);
            }
            towerResult.Height = cave.Max(m => m.Y) + 1;
            towerResult.Cave = cave;
            return towerResult;
        }

        private void FindPattern(List<Day17Coordinate> cave, Day17TowerResult towerResult, int rocks)
        {
            List<Day17Coordinate> orderedList = cave.OrderByDescending(o => o.Y).ThenBy(t => t.X).ToList();
            List<Day17Coordinate> compareList;
            int patternMatchCount = 0;
            if (towerResult.Pattern != null)
                compareList = towerResult.Pattern;
            else
                compareList = orderedList.Take(50).ToList();
            for (int i = 50; i <= orderedList.Count() - 50; i++)
            {
                List<Day17Coordinate> tempList = orderedList.Skip(i).Take(50).ToList();
                bool foundMatch = true;
                for (int j = 0; j < 50; j++)
                {
                    if (tempList[j].X != compareList[j].X)
                    {
                        foundMatch = false;
                        break;
                    }
                }
                if (foundMatch)
                {
                    patternMatchCount++;
                    if (towerResult.Pattern == null)
                    {
                        towerResult.Pattern = compareList;
                        towerResult.PatternFirstMatchHeight = cave.Max(m => m.Y);
                        towerResult.PatternFirstMatchRocksNr = rocks;
                    }
                    else if (patternMatchCount == 2 && towerResult.PatternSecondMatchRocksNr == null)
                    {
                        towerResult.PatternSecondMatchHeight = cave.Max(m => m.Y);
                        towerResult.PatternSecondMatchRocksNr = rocks;
                    }
                    else if (patternMatchCount == 3 && towerResult.PatternRocks == null)
                    {
                        // For some reason i only get the correct pattern rocks and height after the second match and not on the first.
                        // Maybe ill investigate this later, but for now i get the correct result.
                        towerResult.PatternRocks = rocks - towerResult.PatternSecondMatchRocksNr;
                        towerResult.PatternHeight = cave.Max(m => m.Y) - towerResult.PatternSecondMatchHeight;
                    }
                }
            }
        }

        private List<Day17Coordinate> CreateShape(int x, int y, int shapeId)
        {
            List<Day17Coordinate> shape = null;
            if (shapeId == 0)
            {
                shape = new List<Day17Coordinate>
                {
                    new Day17Coordinate { X = x, Y = y },
                    new Day17Coordinate { X = x + 1, Y = y },
                    new Day17Coordinate { X = x + 2, Y = y },
                    new Day17Coordinate { X = x + 3, Y = y }
                };
            }
            else if (shapeId == 1)
            {
                shape = new List<Day17Coordinate>
                {
                    new Day17Coordinate { X = x + 1, Y = y },
                    new Day17Coordinate { X = x, Y = y + 1 },
                    new Day17Coordinate { X = x + 1, Y = y + 1 },
                    new Day17Coordinate { X = x + 2, Y = y + 1 },
                    new Day17Coordinate { X = x + 1, Y = y + 2 }
                };
            }
            else if (shapeId == 2)
            {
                shape = new List<Day17Coordinate>
                {
                    new Day17Coordinate { X = x, Y = y },
                    new Day17Coordinate { X = x + 1, Y = y },
                    new Day17Coordinate { X = x + 2, Y = y },
                    new Day17Coordinate { X = x + 2, Y = y + 1 },
                    new Day17Coordinate { X = x + 2, Y = y + 2 }
                };
            }
            else if (shapeId == 3)
            {
                shape = new List<Day17Coordinate>
                {
                    new Day17Coordinate { X = x, Y = y },
                    new Day17Coordinate { X = x, Y = y + 1 },
                    new Day17Coordinate { X = x, Y = y + 2 },
                    new Day17Coordinate { X = x, Y = y + 3 }
                };
            }
            else if (shapeId == 4)
            {
                shape = new List<Day17Coordinate>
                {
                    new Day17Coordinate { X = x, Y = y },
                    new Day17Coordinate { X = x + 1, Y = y },
                    new Day17Coordinate { X = x + 1, Y = y + 1 },
                    new Day17Coordinate { X = x, Y = y + 1 }
                };
            }
            return shape;
        }

        private void Day17PrintCave(List<Day17Coordinate> cave, List<Day17Coordinate> shape)
        {
            int maxY = Math.Max(cave.Max(m => (int?)m.Y) ?? 0, shape.Max(m => (int?)m.Y) ?? 0);
            for (int y = maxY + 1; y >= -1; y--)
            {
                string row = string.Empty;
                for (int x = 0; x < 7; x++)
                {
                    Day17Coordinate coordinate = cave.FirstOrDefault(w => w.X == x && w.Y == y);
                    if (coordinate == null)
                        coordinate = shape.FirstOrDefault(w => w.X == x && w.Y == y);
                    if (coordinate == null)
                        row += ".";
                    else
                        row += "#";
                }
                Debug.WriteLine(row);
            }

        }

        private class Shape
        {
            public List<Day17Coordinate> CoordinateList { get; set; }
        }

        private class Day17Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private class Day17TowerResult
        {
            public List<Day17Coordinate> Cave { get; set; }
            public int Height { get; set; }
            public List<Day17Coordinate> Pattern { get; set; }
            public int? PatternFirstMatchRocksNr { get; set; }
            public int? PatternFirstMatchHeight { get; set; }
            public int? PatternSecondMatchRocksNr { get; set; }
            public int? PatternSecondMatchHeight { get; set; }
            public int? PatternRocks { get; set; }
            public int? PatternHeight { get; set; }
        }

        [TestMethod]
        public void Day18_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day18.txt").ToList();
            List<Cube> cubeList = new List<Cube>();
            foreach (string input in inputList)
            {
                List<string> inputSplit = input.Split(',').ToList();
                cubeList.Add(new Cube { X = int.Parse(inputSplit[0]), Y = int.Parse(inputSplit[1]), Z = int.Parse(inputSplit[2]) });
            }
            int exposedSides = 0;
            foreach (Cube cube in cubeList)
                exposedSides += 6 - cubeList.Where(w => (Math.Abs(w.X - cube.X) + Math.Abs(w.Y - cube.Y) + Math.Abs(w.Z - cube.Z)) == 1).Count();
        }

        [TestMethod]
        public void Day18_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day18.txt").ToList();
            List<Cube> cubeList = new List<Cube>();
            foreach (string input in inputList)
            {
                List<string> inputSplit = input.Split(',').ToList();
                cubeList.Add(new Cube { X = int.Parse(inputSplit[0]), Y = int.Parse(inputSplit[1]), Z = int.Parse(inputSplit[2]), IsAir = false, IsExposed = false });
            }

            int minX = cubeList.Min(m => m.X) - 1;
            int maxX = cubeList.Max(m => m.X) + 1;
            int minY = cubeList.Min(m => m.Y) - 1;
            int maxY = cubeList.Max(m => m.Y) + 1;
            int minZ = cubeList.Min(m => m.Z) - 1;
            int maxZ = cubeList.Max(m => m.Z) + 1;

            for (int x = minX; x <= maxX; x++)
                for (int y = minY; y <= maxY; y++)
                    for (int z = minZ; z <= maxZ; z++)
                    {
                        Cube cube = cubeList.FirstOrDefault(w => w.X == x && w.Y == y && w.Z == z);
                        if (cube == null)
                        {
                            cubeList.Add(new Cube
                            {
                                X = x,
                                Y = y,
                                Z = z,
                                IsAir = true,
                                IsExposed = z == minZ || z == maxZ || y == minY || y == maxY || x == minX || x == maxX
                            });
                        }
                    }

            int previousExposedAirCount = 0;
            for (int i = 0; i < 50; i++)
            {
                List<Cube> exposedAirList = cubeList.Where(w => w.IsAir && !w.IsExposed).ToList();
                if (previousExposedAirCount != exposedAirList.Count())
                    previousExposedAirCount = exposedAirList.Count();
                else
                    break;
                foreach (Cube cube in exposedAirList.OrderBy(o => o.X).ThenBy(t => t.Y).ThenBy(b => b.Z))
                    if (cubeList.Any(w => (Math.Abs(w.X - cube.X) + Math.Abs(w.Y - cube.Y) + Math.Abs(w.Z - cube.Z)) == 1 && w.IsExposed))
                        cube.IsExposed = true;
            }

            int exposedSides = 0;
            foreach (Cube cube in cubeList.Where(w => !w.IsAir))
            {
                exposedSides += 6 - cubeList.Where(w => (Math.Abs(w.X - cube.X) + Math.Abs(w.Y - cube.Y) + Math.Abs(w.Z - cube.Z)) == 1 && !w.IsExposed).Count();
            }
        }
        private class Cube
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public bool IsAir { get; set; }
            public bool IsExposed { get; set; }
        }

        [TestMethod]
        public void Day19_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day19.txt").ToList();
            List<Blueprint> blueprintList = new List<Blueprint>();
            foreach (string input in inputList)
            {
                List<string> inputSplit = input.Split(' ').ToList();
                blueprintList.Add(new Blueprint
                {
                    BlueprintId = int.Parse(inputSplit[1].TrimEnd(':')),
                    OreRobotOreCost = int.Parse(inputSplit[6]),
                    ClayRobotOreCost = int.Parse(inputSplit[12]),
                    ObsidianRobotOreCost = int.Parse(inputSplit[18]),
                    ObsidianRobotClayCost = int.Parse(inputSplit[21]),
                    GeodeRobotOreCost = int.Parse(inputSplit[27]),
                    GeodeRobotObsidianCost = int.Parse(inputSplit[30]),
                });
            }
            List<int> qualityLevels = new List<int>();
            foreach (Blueprint blueprint in blueprintList)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                RecordGeode recordGeode = new RecordGeode { MaxGeode = 0 };
                int maxOre = new List<int> { blueprint.OreRobotOreCost, blueprint.ObsidianRobotOreCost, blueprint.GeodeRobotOreCost, blueprint.ClayRobotOreCost }.Max();
                SimulateRobots(24, blueprint, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, maxOre, recordGeode);
                qualityLevels.Add(blueprint.BlueprintId * recordGeode.MaxGeode);
                Debug.WriteLine("Blueprint " + blueprint.BlueprintId + " MaxGeode: " + recordGeode.MaxGeode + " qualityLevel: " + blueprint.BlueprintId * recordGeode.MaxGeode + " Elapsed: " + stopwatch.Elapsed);
                stopwatch.Stop();
            }
            int sumQualityLevels = qualityLevels.Sum();
            Debug.WriteLine("Quality: " + sumQualityLevels);
        }

        [TestMethod]
        public void Day19_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day19.txt").ToList();
            List<Blueprint> blueprintList = new List<Blueprint>();
            foreach (string input in inputList)
            {
                List<string> inputSplit = input.Split(' ').ToList();
                blueprintList.Add(new Blueprint
                {
                    BlueprintId = int.Parse(inputSplit[1].TrimEnd(':')),
                    OreRobotOreCost = int.Parse(inputSplit[6]),
                    ClayRobotOreCost = int.Parse(inputSplit[12]),
                    ObsidianRobotOreCost = int.Parse(inputSplit[18]),
                    ObsidianRobotClayCost = int.Parse(inputSplit[21]),
                    GeodeRobotOreCost = int.Parse(inputSplit[27]),
                    GeodeRobotObsidianCost = int.Parse(inputSplit[30]),
                });
            }
            List<int> geodeList = new List<int>();
            foreach (Blueprint blueprint in blueprintList.Take(3))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                RecordGeode recordGeode = new RecordGeode { MaxGeode = 0 };
                int maxOre = new List<int> { blueprint.OreRobotOreCost, blueprint.ObsidianRobotOreCost, blueprint.GeodeRobotOreCost, blueprint.ClayRobotOreCost }.Max();
                SimulateRobots(32, blueprint, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, maxOre, recordGeode);
                geodeList.Add(recordGeode.MaxGeode);
                Debug.WriteLine("Blueprint " + blueprint.BlueprintId + " MaxGeode: " + recordGeode.MaxGeode + " Elapsed: " + stopwatch.Elapsed);
                stopwatch.Stop();
            }
            int multipliedGeodes = geodeList.Aggregate((sum, next) => sum * next);
            Debug.WriteLine("Multiplied geodes: " + multipliedGeodes);
        }

        private void SimulateRobots(int minutesLeft, Blueprint blueprint, int oreRobots, int clayRobots, int obsidianRobots, int geodeRobots,
            int ore, int clay, int obsidian, int geode, int oreRobotConstruction, int clayRobotConstruction, int obsidianRobotConstruction, int geodeRobotConstruction
            , int maxOre, RecordGeode recordGeode)
        {
            ore += oreRobots;
            clay += clayRobots;
            obsidian += obsidianRobots;
            geode += geodeRobots;
            minutesLeft--;
            oreRobots += oreRobotConstruction;
            clayRobots += clayRobotConstruction;
            obsidianRobots += obsidianRobotConstruction;
            geodeRobots += geodeRobotConstruction;
            if (geode + geodeRobots * minutesLeft + (minutesLeft * minutesLeft) / 2 < recordGeode.MaxGeode)
                return;
            if (minutesLeft == 0)
            {
                if (recordGeode.MaxGeode < geode)
                {
                    Debug.WriteLine("MaxGeode: " + geode + " OreRobots: " + oreRobots + " ClayRobots: " + clayRobots + " ObsidianRobots: " + obsidianRobots + " GeodeRobots: " + geodeRobots
                        + " Ore: " + ore + " Clay: " + clay + " Obsidian: " + obsidian);
                    recordGeode.MaxGeode = geode;
                }
                return;
            }
            if (ore >= blueprint.OreRobotOreCost)
                SimulateRobots(minutesLeft, blueprint, oreRobots, clayRobots, obsidianRobots, geodeRobots, ore - blueprint.OreRobotOreCost, clay, obsidian, geode, 1, 0, 0, 0, maxOre, recordGeode);
            if (ore >= blueprint.ClayRobotOreCost)
                SimulateRobots(minutesLeft, blueprint, oreRobots, clayRobots, obsidianRobots, geodeRobots, ore - blueprint.ClayRobotOreCost, clay, obsidian, geode, 0, 1, 0, 0, maxOre, recordGeode);
            if (ore >= blueprint.ObsidianRobotOreCost && clay >= blueprint.ObsidianRobotClayCost)
                SimulateRobots(minutesLeft, blueprint, oreRobots, clayRobots, obsidianRobots, geodeRobots, ore - blueprint.ObsidianRobotOreCost
                    , clay - blueprint.ObsidianRobotClayCost, obsidian, geode, 0, 0, 1, 0, maxOre, recordGeode);
            if (ore >= blueprint.GeodeRobotOreCost && obsidian >= blueprint.GeodeRobotObsidianCost)
                SimulateRobots(minutesLeft, blueprint, oreRobots, clayRobots, obsidianRobots, geodeRobots, ore - blueprint.GeodeRobotOreCost
                    , clay, obsidian - blueprint.GeodeRobotObsidianCost, geode, 0, 0, 0, 1, maxOre, recordGeode);
            if (ore < maxOre
                || (clay < blueprint.ObsidianRobotClayCost && clayRobots > 0)
                || (obsidian < blueprint.GeodeRobotObsidianCost && obsidianRobots > 0))
                SimulateRobots(minutesLeft, blueprint, oreRobots, clayRobots, obsidianRobots, geodeRobots, ore, clay, obsidian, geode, 0, 0, 0, 0, maxOre, recordGeode);
            return;
        }

        private class RecordGeode
        {
            public int MaxGeode { get; set; }
        }

        private class Blueprint
        {
            public int BlueprintId { get; set; }
            public int OreRobotOreCost { get; set; }
            public int ClayRobotOreCost { get; set; }
            public int ObsidianRobotOreCost { get; set; }
            public int ObsidianRobotClayCost { get; set; }
            public int GeodeRobotOreCost { get; set; }
            public int GeodeRobotObsidianCost { get; set; }
        }

        [TestMethod]
        public void Day20_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day20.txt").ToList();
            int order = 0;
            List<EncryptedNumber> encryptedNumberList = new List<EncryptedNumber>();
            foreach (string input in inputList)
                encryptedNumberList.Add(new EncryptedNumber { Number = int.Parse(input), OriginalOrder = order++ });
            EncryptedNumber nextEncryptedNumber = encryptedNumberList.OrderBy(o => o.OriginalOrder).FirstOrDefault();
            while (nextEncryptedNumber != null)
            {
                BigInteger newIndex = encryptedNumberList.IndexOf(nextEncryptedNumber);
                newIndex = (newIndex + nextEncryptedNumber.Number) % (encryptedNumberList.Count() - 1);
                if (newIndex <= 0)
                    newIndex = (encryptedNumberList.Count() - 1) + newIndex;
                encryptedNumberList.Remove(nextEncryptedNumber);
                encryptedNumberList.Insert((int)newIndex, nextEncryptedNumber);
                nextEncryptedNumber = encryptedNumberList.Where(w => w.OriginalOrder > nextEncryptedNumber.OriginalOrder).OrderBy(o => o.OriginalOrder).FirstOrDefault();
            }
            int zeroIndex = encryptedNumberList.FindIndex(w => w.Number == 0);
            Int64 value1 = encryptedNumberList[(1000 + zeroIndex) % encryptedNumberList.Count()].Number;
            Int64 value2 = encryptedNumberList[(2000 + zeroIndex) % encryptedNumberList.Count()].Number;
            Int64 value3 = encryptedNumberList[(3000 + zeroIndex) % encryptedNumberList.Count()].Number;
            Int64 sum = value1 + value2 + value3;
            Debug.WriteLine(sum);
        }

        [TestMethod]
        public void Day20_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day20.txt").ToList();
            int order = 0;
            List<EncryptedNumber> encryptedNumberList = new List<EncryptedNumber>();
            foreach (string input in inputList)
                encryptedNumberList.Add(new EncryptedNumber { Number = (Int64)int.Parse(input), OriginalOrder = order++ });
            encryptedNumberList.ForEach(e => e.Number *= (Int64)811589153);
            for (int i = 0; i < 10; i++)
            {
                //Day20PrintEncryptedMessage(encryptedNumberList);
                EncryptedNumber nextEncryptedNumber = encryptedNumberList.OrderBy(o => o.OriginalOrder).FirstOrDefault();
                while (nextEncryptedNumber != null)
                {
                    int move = (int)(nextEncryptedNumber.Number % (encryptedNumberList.Count() - 1));
                    int currentIndex = encryptedNumberList.IndexOf(nextEncryptedNumber);
                    int newIndex = currentIndex + move;
                    if (newIndex < 0)
                        newIndex += encryptedNumberList.Count() - 1;
                    if (newIndex >= encryptedNumberList.Count())
                        newIndex -= encryptedNumberList.Count() - 1;
                    encryptedNumberList.Remove(nextEncryptedNumber);
                    encryptedNumberList.Insert(newIndex, nextEncryptedNumber);
                    nextEncryptedNumber = encryptedNumberList.Where(w => w.OriginalOrder > nextEncryptedNumber.OriginalOrder).OrderBy(o => o.OriginalOrder).FirstOrDefault();
                }
            }
            int zeroIndex = encryptedNumberList.FindIndex(w => w.Number == 0);
            Int64 value1 = encryptedNumberList[(1000 + zeroIndex) % encryptedNumberList.Count()].Number;
            Int64 value2 = encryptedNumberList[(2000 + zeroIndex) % encryptedNumberList.Count()].Number;
            Int64 value3 = encryptedNumberList[(3000 + zeroIndex) % encryptedNumberList.Count()].Number;
            Int64 sum = value1 + value2 + value3;
            Debug.WriteLine(sum);
        }

        private void Day20PrintEncryptedMessage(List<EncryptedNumber> encryptedNumberList)
        {
            string message = string.Empty;
            foreach (EncryptedNumber encryptedNumber in encryptedNumberList)
            {
                message += encryptedNumber.Number + ",";
            }
            Debug.WriteLine(message.TrimEnd(','));
        }

        private class EncryptedNumber
        {
            public int OriginalOrder { get; set; }
            public Int64 Number { get; set; }
        }

        [TestMethod]
        public void Day21_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day21.txt").ToList();
            List<Day21MonkeyValue> monkeyValueList = new List<Day21MonkeyValue>();
            List<Day21MonkeyOperation> monkeyOperationList = new List<Day21MonkeyOperation>();
            foreach (string input in inputList)
            {
                List<string> inputSplit = input.Split(' ').ToList();
                string name = inputSplit[0].TrimEnd(':');
                int leftValue;
                if (int.TryParse(inputSplit[1], out leftValue))
                    monkeyValueList.Add(new Day21MonkeyValue { Name = name, Value = leftValue });
                else
                    monkeyOperationList.Add(new Day21MonkeyOperation { Name = name, LeftName = inputSplit[1], Operation = inputSplit[2], RightName = inputSplit[3] });
            }
            while (monkeyOperationList.Any())
            {
                Day21MonkeyOperation monkeyOperation = monkeyOperationList.FirstOrDefault(w => monkeyValueList.Any(a => a.Name == w.LeftName) && monkeyValueList.Any(a => a.Name == w.RightName));
                if (monkeyOperation == null)
                    break;

                double leftValue = monkeyValueList.First(w => w.Name == monkeyOperation.LeftName).Value;
                double rightValue = monkeyValueList.First(w => w.Name == monkeyOperation.RightName).Value;
                double result;
                if (monkeyOperation.Operation == "+")
                    result = leftValue + rightValue;
                else if (monkeyOperation.Operation == "-")
                    result = leftValue - rightValue;
                else if (monkeyOperation.Operation == "*")
                    result = leftValue * rightValue;
                else if (monkeyOperation.Operation == "/")
                    result = leftValue / rightValue;
                else
                    throw new Exception("Invalid operation");
                monkeyValueList.Add(new Day21MonkeyValue { Name = monkeyOperation.Name, Value = result });
                monkeyOperationList.Remove(monkeyOperation);
            }
            double rootValue = monkeyValueList.First(w => w.Name == "root").Value;
        }

        [TestMethod]
        public void Day21_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day21.txt").ToList();
            List<Day21MonkeyValue> monkeyValueList = new List<Day21MonkeyValue>();
            List<Day21MonkeyOperation> monkeyOperationList = new List<Day21MonkeyOperation>();
            foreach (string input in inputList)
            {
                List<string> inputSplit = input.Split(' ').ToList();
                string name = inputSplit[0].TrimEnd(':');
                int leftValue;
                if (int.TryParse(inputSplit[1], out leftValue))
                    monkeyValueList.Add(new Day21MonkeyValue { Name = name, Value = leftValue });
                else
                    monkeyOperationList.Add(new Day21MonkeyOperation { Name = name, LeftName = inputSplit[1], Operation = inputSplit[2], RightName = inputSplit[3] });
            }
            CalculateMonkeyValue(monkeyValueList, monkeyOperationList, monkeyOperationList.First(w => w.Name == "root"));
            Day21MonkeyOperation rootMonkeyOperation = monkeyOperationList.First(w => w.Name == "root");
            Day21MonkeyOperation monkeyOperation = monkeyOperationList.First(w => w.Name == rootMonkeyOperation.LeftName || w.Name == rootMonkeyOperation.RightName);
            double resultValue = monkeyValueList.First(w => w.Name == rootMonkeyOperation.LeftName || w.Name == rootMonkeyOperation.RightName).Value;
            double humnValue = CalculateHumnValue(monkeyValueList, monkeyOperationList, monkeyOperation, resultValue);
        }

        private double CalculateHumnValue(List<Day21MonkeyValue> monkeyValueList, List<Day21MonkeyOperation> monkeyOperationList, Day21MonkeyOperation currentMonkeyOperation, double resultValue)
        {
            double? leftValue = monkeyValueList.FirstOrDefault(w => w.Name == currentMonkeyOperation.LeftName && w.Name != "humn")?.Value;
            double? rightValue = monkeyValueList.FirstOrDefault(w => w.Name == currentMonkeyOperation.RightName && w.Name != "humn")?.Value;
            double newResultValue;
            if (leftValue == null)
            {
                if (currentMonkeyOperation.Operation == "+")
                    newResultValue = resultValue - rightValue.Value;
                else if (currentMonkeyOperation.Operation == "-")
                    newResultValue = resultValue + rightValue.Value;
                else if (currentMonkeyOperation.Operation == "*")
                    newResultValue = resultValue / rightValue.Value;
                else if (currentMonkeyOperation.Operation == "/")
                    newResultValue = resultValue * rightValue.Value;
                else
                    throw new Exception("Invalid operation");
                if (currentMonkeyOperation.LeftName == "humn")
                    return newResultValue;
                return CalculateHumnValue(monkeyValueList, monkeyOperationList, monkeyOperationList.First(w => w.Name == currentMonkeyOperation.LeftName), newResultValue);
            }
            else if (rightValue == null)
            {
                if (currentMonkeyOperation.Operation == "+")
                    newResultValue = resultValue - leftValue.Value;
                else if (currentMonkeyOperation.Operation == "-")
                    newResultValue = leftValue.Value - resultValue;
                else if (currentMonkeyOperation.Operation == "*")
                    newResultValue = resultValue / leftValue.Value;
                else if (currentMonkeyOperation.Operation == "/")
                    newResultValue = resultValue / leftValue.Value;
                else
                    throw new Exception("Invalid operation");
                if (currentMonkeyOperation.RightName == "humn")
                    return newResultValue;
                return CalculateHumnValue(monkeyValueList, monkeyOperationList, monkeyOperationList.First(w => w.Name == currentMonkeyOperation.RightName), newResultValue);
            }
            else
                throw new Exception("Bad data");
        }

        private double? CalculateMonkeyValue(List<Day21MonkeyValue> monkeyValueList, List<Day21MonkeyOperation> monkeyOperationList, Day21MonkeyOperation currentMonkeyOperation)
        {
            double? leftValue = monkeyValueList.FirstOrDefault(w => w.Name == currentMonkeyOperation.LeftName && w.Name != "humn")?.Value;
            double? rightValue = monkeyValueList.FirstOrDefault(w => w.Name == currentMonkeyOperation.RightName && w.Name != "humn")?.Value;

            if (leftValue == null && currentMonkeyOperation.LeftName != "humn")
                leftValue = CalculateMonkeyValue(monkeyValueList, monkeyOperationList, monkeyOperationList.First(w => w.Name == currentMonkeyOperation.LeftName));
            if (rightValue == null && currentMonkeyOperation.RightName != "humn")
                rightValue = CalculateMonkeyValue(monkeyValueList, monkeyOperationList, monkeyOperationList.First(w => w.Name == currentMonkeyOperation.RightName));
            if (leftValue != null)
                monkeyValueList.Add(new Day21MonkeyValue { Name = currentMonkeyOperation.LeftName, Value = leftValue.Value });
            if (rightValue != null)
                monkeyValueList.Add(new Day21MonkeyValue { Name = currentMonkeyOperation.RightName, Value = rightValue.Value });
            if (leftValue == null || rightValue == null)
                return null;

            double result;
            if (currentMonkeyOperation.Operation == "+")
                result = leftValue.Value + rightValue.Value;
            else if (currentMonkeyOperation.Operation == "-")
                result = leftValue.Value - rightValue.Value;
            else if (currentMonkeyOperation.Operation == "*")
                result = leftValue.Value * rightValue.Value;
            else if (currentMonkeyOperation.Operation == "/")
                result = leftValue.Value / rightValue.Value;
            else
                throw new Exception("Invalid operation");
            monkeyOperationList.Remove(currentMonkeyOperation);
            return result;
        }

        private class Day21MonkeyValue
        {
            public string Name { get; set; }
            public double Value { get; set; }
        }

        private class Day21MonkeyOperation
        {
            public string Name { get; set; }
            public string LeftName { get; set; }
            public string RightName { get; set; }
            public string Operation { get; set; }
        }

        [TestMethod]
        public void Day22_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day22.txt").ToList();
            HashSet<Day22Coordinate> coordinateList = new HashSet<Day22Coordinate>();
            List<string> instructionList = new List<string>();
            int y = 1;
            bool coordinatesComplete = false;
            foreach (string row in inputList)
            {
                if (string.IsNullOrEmpty(row))
                    coordinatesComplete = true;
                if (!coordinatesComplete)
                {
                    int x = 1;
                    foreach (char c in row)
                    {
                        if (c == '.')
                            coordinateList.Add(new Day22Coordinate { X = x, Y = y });
                        else if (c == '#')
                            coordinateList.Add(new Day22Coordinate { X = x, Y = y, IsRock = true });
                        x++;
                    }
                    y++;
                }
                else if (!string.IsNullOrEmpty(row))
                {
                    string distance = string.Empty;
                    foreach (char c in row)
                    {
                        if (char.IsDigit(c))
                            distance += c;
                        else
                        {
                            if (!string.IsNullOrEmpty(distance))
                            {
                                instructionList.Add(distance);
                                distance = string.Empty;
                            }
                            instructionList.Add(c.ToString());
                        }
                    }
                    if (!string.IsNullOrEmpty(distance))
                        instructionList.Add(distance);
                }
            }
            Day22Coordinate currentPosition = coordinateList.OrderBy(o => o.Y).ThenBy(t => t.X).First();
            currentPosition.VisitedSign = ">";
            char currentDirection = 'E'; // E = east, W = west, N = north, S = south
            foreach (string instruction in instructionList)
            {
                int distance;
                if (!int.TryParse(instruction, out distance))
                {
                    currentDirection = CalculateDirection(currentDirection, instruction, 1);
                    continue;
                }
                Day22Coordinate coordinate;
                for (int i = 0; i < distance; i++)
                {
                    if (currentDirection == 'E' || currentDirection == 'W')
                    {
                        List<Day22Coordinate> row = coordinateList.Where(w => w.Y == currentPosition.Y).ToList();
                        if (currentDirection == 'E')
                        {
                            coordinate = row.FirstOrDefault(w => w.X == currentPosition.X + 1);
                            if (coordinate == null)
                                coordinate = row.OrderBy(o => o.X).First();
                            coordinate.VisitedSign = ">";
                        }
                        else
                        {
                            coordinate = row.FirstOrDefault(w => w.X == currentPosition.X - 1);
                            if (coordinate == null)
                                coordinate = row.OrderByDescending(o => o.X).First();
                            coordinate.VisitedSign = "<";
                        }
                    }
                    else
                    {
                        List<Day22Coordinate> column = coordinateList.Where(w => w.X == currentPosition.X).ToList();
                        if (currentDirection == 'N')
                        {
                            coordinate = column.FirstOrDefault(w => w.Y == currentPosition.Y - 1);
                            if (coordinate == null)
                                coordinate = column.OrderByDescending(o => o.Y).First();
                            coordinate.VisitedSign = "A";
                        }
                        else
                        {
                            coordinate = column.FirstOrDefault(w => w.Y == currentPosition.Y + 1);
                            if (coordinate == null)
                                coordinate = column.OrderBy(o => o.Y).First();
                            coordinate.VisitedSign = "v";
                        }
                    }
                    if (coordinate.IsRock)
                        break;
                    else
                        currentPosition = coordinate;
                }
            }
            Day22PrintMap(coordinateList);
            int sum = currentPosition.Y * 1000 + currentPosition.X * 4 + (currentDirection == 'E' ? 0 : currentDirection == 'S' ? 1 : currentDirection == 'W' ? 2 : 3);
            Debug.WriteLine("Sum: " + sum);
        }

        [TestMethod]
        public void Day22_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day22.txt").ToList();
            HashSet<Day22Coordinate> coordinateList = new HashSet<Day22Coordinate>();
            List<string> instructionList = new List<string>();
            int y = 0;
            bool coordinatesComplete = false;
            foreach (string row in inputList)
            {
                if (string.IsNullOrEmpty(row))
                    coordinatesComplete = true;

                if (!coordinatesComplete)
                {
                    int x = 0;
                    foreach (char c in row)
                    {
                        if (c == '.')
                            coordinateList.Add(new Day22Coordinate { X = x, Y = y });
                        else if (c == '#')
                            coordinateList.Add(new Day22Coordinate { X = x, Y = y, IsRock = true });
                        x++;
                    }
                    y++;
                }
                else if (!string.IsNullOrEmpty(row))
                {
                    string distance = string.Empty;
                    foreach (char c in row)
                    {
                        if (char.IsDigit(c))
                            distance += c;
                        else
                        {
                            if (!string.IsNullOrEmpty(distance))
                            {
                                instructionList.Add(distance);
                                distance = string.Empty;
                            }
                            instructionList.Add(c.ToString());
                        }
                    }
                    if (!string.IsNullOrEmpty(distance))
                        instructionList.Add(distance);
                }
            }
            int sideLength = Math.Max(coordinateList.Max(m => m.X) + 1, coordinateList.Max(m => m.Y) + 1) / 4;
            foreach (Day22Coordinate coordinate in coordinateList)
            {
                coordinate.SideX = coordinate.X / sideLength;
                coordinate.SideY = coordinate.Y / sideLength;
                coordinate.X = coordinate.X % sideLength;
                coordinate.Y = coordinate.Y % sideLength;
            }
            List<Day22Side> sideList = coordinateList.GroupBy(g => new { g.SideX, g.SideY }).Select(s => new Day22Side { X = s.Key.SideX, Y = s.Key.SideY }).ToList();
            Day22Coordinate currentPosition = coordinateList.OrderBy(o => o.Y).ThenBy(t => t.X).First();
            currentPosition.VisitedSign = "X";
            char currentDirection = 'E'; // E = east, W = west, N = north, S = south
            foreach (string instruction in instructionList)
            {
                int distance;
                if (!int.TryParse(instruction, out distance))
                {
                    currentDirection = CalculateDirection(currentDirection, instruction, 1);
                    continue;
                }
                for (int i = 0; i < distance; i++)
                {
                    char newDirection = currentDirection;
                    Day22Coordinate coordinate;
                    if (currentDirection == 'E' || currentDirection == 'W')
                    {
                        if (currentDirection == 'E')
                        {
                            coordinate = coordinateList.FirstOrDefault(w => w.X == currentPosition.X + 1 && w.Y == currentPosition.Y && w.SideX == currentPosition.SideX && w.SideY == currentPosition.SideY);
                            if (coordinate == null)
                                coordinate = coordinateList.FirstOrDefault(w => w.X == 0 && w.Y == currentPosition.Y && w.SideX == (currentPosition.SideX + 1) % 4 && w.SideY == currentPosition.SideY);
                            if (coordinate == null && sideList.Count(c => (c.X == currentPosition.SideX && c.Y == (currentPosition.SideY + 3) % 4)
                                    || (c.X == (currentPosition.SideX + 1) % 4 && c.Y == (currentPosition.SideY + 3) % 4)) == 2)
                            {
                                int newY = sideLength - 1;
                                int newX = currentPosition.Y;
                                int newSideY = (currentPosition.SideY + 3) % 4;
                                int newSideX = (currentPosition.SideX + 1) % 4;
                                coordinate = coordinateList.FirstOrDefault(w => w.X == newX && w.Y == newY && w.SideX == newSideX && w.SideY == newSideY);
                                newDirection = CalculateDirection(currentDirection, "L", 1);
                            }
                            if (coordinate == null && sideList.Count(c => (c.X == currentPosition.SideX && c.Y == (currentPosition.SideY + 3) % 4)
                                    || (c.X == currentPosition.SideX && c.Y == (currentPosition.SideY + 2) % 4)
                                    || (c.X == (currentPosition.SideX + 1) % 4 && c.Y == (currentPosition.SideY + 2) % 4)) == 3)
                            {
                                int newY = sideLength - currentPosition.Y - 1;
                                int newX = sideLength - 1;
                                int newSideY = (currentPosition.SideY + 2) % 4;
                                int newSideX = (currentPosition.SideX + 1) % 4;
                                coordinate = coordinateList.FirstOrDefault(w => w.X == newX && w.Y == newY && w.SideX == newSideX && w.SideY == newSideY);
                                newDirection = CalculateDirection(currentDirection, "R", 2);
                            }
                            if (coordinate == null && sideList.Count(c => (c.X == (currentPosition.SideX + 3) % 4 && c.Y == currentPosition.SideY)
                                    || (c.X == (currentPosition.SideX + 3) % 4 && c.Y == (currentPosition.SideY + 1) % 4)
                                    || (c.X == (currentPosition.SideX + 3) % 4 && c.Y == (currentPosition.SideY + 2) % 4)) == 3)
                            {
                                int newY = sideLength - currentPosition.Y - 1;
                                int newX = sideLength - 1;
                                int newSideY = (currentPosition.SideY + 2) % 4;
                                int newSideX = (currentPosition.SideX + 3) % 4;
                                coordinate = coordinateList.FirstOrDefault(w => w.X == newX && w.Y == newY && w.SideX == newSideX && w.SideY == newSideY);
                                newDirection = CalculateDirection(currentDirection, "R", 2);
                            }
                            if (coordinate == null && sideList.Count(c => (c.X == currentPosition.SideX && c.Y == (currentPosition.SideY + 1 % 4))
                                 || (c.X == (currentPosition.SideX + 1) % 4 && c.Y == (currentPosition.SideY + 1) % 4)) == 2)
                            {
                                int newY = 0;
                                int newX = sideLength - currentPosition.Y - 1;
                                int newSideY = (currentPosition.SideY + 1) % 4;
                                int newSideX = (currentPosition.SideX + 1) % 4;
                                coordinate = coordinateList.FirstOrDefault(w => w.X == newX && w.Y == newY && w.SideX == newSideX && w.SideY == newSideY);
                                newDirection = CalculateDirection(currentDirection, "R", 1);
                            }
                        }
                        else
                        {
                            coordinate = coordinateList.FirstOrDefault(w => w.X == currentPosition.X - 1 && w.Y == currentPosition.Y && w.SideX == currentPosition.SideX && w.SideY == currentPosition.SideY);
                            if (coordinate == null)
                                coordinate = coordinateList.FirstOrDefault(w => w.X == sideLength - 1 && w.Y == currentPosition.Y && w.SideX == (currentPosition.SideX + 3) % 4 && w.SideY == currentPosition.SideY);
                            if (coordinate == null && sideList.Count(c => (c.X == currentPosition.SideX && c.Y == (currentPosition.SideY + 1) % 4)
                                    || (c.X == currentPosition.SideX && c.Y == (currentPosition.SideY + 2) % 4)
                                    || (c.X == (currentPosition.SideX + 3) % 4 && c.Y == (currentPosition.SideY + 2) % 4)) == 3)
                            {
                                int newY = sideLength - currentPosition.Y - 1;
                                int newX = 0;
                                int newSideY = (currentPosition.SideY + 2) % 4;
                                int newSideX = (currentPosition.SideX + 3) % 4;
                                coordinate = coordinateList.FirstOrDefault(w => w.X == newX && w.Y == newY && w.SideX == newSideX && w.SideY == newSideY);
                                newDirection = CalculateDirection(currentDirection, "R", 2);
                            }
                            if (coordinate == null && sideList.Count(c => (c.X == (currentPosition.SideX + 1) % 4 && c.Y == currentPosition.SideY)
                                    || (c.X == (currentPosition.SideX + 1) % 4 && c.Y == (currentPosition.SideY + 3) % 4)
                                    || (c.X == (currentPosition.SideX + 1) % 4 && c.Y == (currentPosition.SideY + 2) % 4)) == 3)
                            {
                                int newY = sideLength - currentPosition.Y - 1;
                                int newX = 0;
                                int newSideY = (currentPosition.SideY + 2) % 4;
                                int newSideX = (currentPosition.SideX + 1) % 4;
                                coordinate = coordinateList.FirstOrDefault(w => w.X == newX && w.Y == newY && w.SideX == newSideX && w.SideY == newSideY);
                                newDirection = CalculateDirection(currentDirection, "R", 2);
                            }
                            if (coordinate == null && sideList.Count(c => (c.X == currentPosition.SideX && c.Y == (currentPosition.SideY + 1) % 4)
                                  || (c.X == (currentPosition.SideX + 3) % 4 && c.Y == (currentPosition.SideY + 1) % 4)) == 2)
                            {
                                int newY = 0;
                                int newX = currentPosition.Y;
                                int newSideY = (currentPosition.SideY + 1) % 4;
                                int newSideX = (currentPosition.SideX + 3) % 4;
                                coordinate = coordinateList.FirstOrDefault(w => w.X == newX && w.Y == newY && w.SideX == newSideX && w.SideY == newSideY);
                                newDirection = CalculateDirection(currentDirection, "L", 1);
                            }
                            if (coordinate == null && sideList.Count(c => (c.X == currentPosition.SideX && c.Y == (currentPosition.SideY + 3) % 4)
                                    || (c.X == (currentPosition.SideX + 1) % 4 && c.Y == (currentPosition.SideY + 3) % 4)
                                    || (c.X == (currentPosition.SideX + 1) % 4 && c.Y == (currentPosition.SideY + 2) % 4)
                                    || (c.X == (currentPosition.SideX + 1) % 4 && c.Y == (currentPosition.SideY + 1) % 4)) == 4)
                            {
                                int newY = 0;
                                int newX = currentPosition.Y;
                                int newSideY = (currentPosition.SideY + 1) % 4;
                                int newSideX = (currentPosition.SideX + 1) % 4;
                                coordinate = coordinateList.FirstOrDefault(w => w.X == newX && w.Y == newY && w.SideX == newSideX && w.SideY == newSideY);
                                newDirection = CalculateDirection(currentDirection, "L", 1);
                            }
                        }
                    }
                    else
                    {
                        if (currentDirection == 'N')
                        {
                            coordinate = coordinateList.FirstOrDefault(w => w.X == currentPosition.X && w.Y == currentPosition.Y - 1 && w.SideX == currentPosition.SideX && w.SideY == currentPosition.SideY);
                            if (coordinate == null)
                                coordinate = coordinateList.FirstOrDefault(w => w.X == currentPosition.X && w.Y == sideLength - 1 && w.SideX == currentPosition.SideX && w.SideY == (currentPosition.SideY + 3) % 4);
                            if (coordinate == null && sideList.Count(c => (c.X == (currentPosition.SideX + 3) % 4 && c.Y == currentPosition.SideY)
                                    || (c.X == (currentPosition.SideX + 3) % 4 && c.Y == (currentPosition.SideY + 1) % 4)
                                    || (c.X == (currentPosition.SideX + 3) % 4 && c.Y == (currentPosition.SideY + 2) % 4)
                                    || (c.X == (currentPosition.SideX + 2) % 4 && c.Y == (currentPosition.SideY + 2) % 4)
                                    || (c.X == (currentPosition.SideX + 2) % 4 && c.Y == (currentPosition.SideY + 3) % 4)) == 5)
                            {
                                int newY = sideLength - 1;
                                int newX = currentPosition.X;
                                int newSideY = (currentPosition.SideY + 3) % 4;
                                int newSideX = (currentPosition.SideX + 2) % 4;
                                coordinate = coordinateList.FirstOrDefault(w => w.X == newX && w.Y == newY && w.SideX == newSideX && w.SideY == newSideY);
                            }
                            if (coordinate == null && sideList.Count(c => (c.X == (currentPosition.SideX + 1) % 4 && c.Y == currentPosition.SideY)
                                    || (c.X == (currentPosition.SideX + 1) % 4 && c.Y == (currentPosition.SideY + 3) % 4)) == 2)
                            {
                                int newY = currentPosition.X;
                                int newX = 0;
                                int newSideY = (currentPosition.SideY + 3) % 4;
                                int newSideX = (currentPosition.SideX + 1) % 4;
                                coordinate = coordinateList.FirstOrDefault(w => w.X == newX && w.Y == newY && w.SideX == newSideX && w.SideY == newSideY);
                                newDirection = CalculateDirection(currentDirection, "R", 1);
                            }
                            if (coordinate == null && sideList.Count(c => (c.X == currentPosition.SideX && c.Y == (currentPosition.SideY + 1) % 4)
                                    || (c.X == currentPosition.SideX && c.Y == (currentPosition.SideY + 2) % 4)
                                    || (c.X == (currentPosition.SideX + 3) % 4 && c.Y == (currentPosition.SideY + 2) % 4)
                                    || (c.X == (currentPosition.SideX + 3) % 4 && c.Y == (currentPosition.SideY + 3) % 4)) == 4)
                            {
                                int newY = currentPosition.X;
                                int newX = 0;
                                int newSideY = (currentPosition.SideY + 3) % 4;
                                int newSideX = (currentPosition.SideX + 3) % 4;
                                coordinate = coordinateList.FirstOrDefault(w => w.X == newX && w.Y == newY && w.SideX == newSideX && w.SideY == newSideY);
                                newDirection = CalculateDirection(currentDirection, "R", 1);
                            }
                        }
                        else
                        {
                            coordinate = coordinateList.FirstOrDefault(w => w.X == currentPosition.X && w.Y == currentPosition.Y + 1 && w.SideX == currentPosition.SideX && w.SideY == currentPosition.SideY);
                            if (coordinate == null)
                                coordinate = coordinateList.FirstOrDefault(w => w.X == currentPosition.X && w.Y == 0 && w.SideX == currentPosition.SideX && w.SideY == (currentPosition.SideY + 1) % 4);
                            if (coordinate == null && sideList.Count(c => (c.X == (currentPosition.SideX + 3) % 4 && c.Y == currentPosition.SideY)
                                    || (c.X == (currentPosition.SideX + 3) % 4 && c.Y == (currentPosition.SideY + 1) % 4)) == 2)
                            {
                                int newY = currentPosition.X;
                                int newX = sideLength - 1;
                                int newSideY = (currentPosition.SideY + 1) % 4;
                                int newSideX = (currentPosition.SideX + 3) % 4;
                                coordinate = coordinateList.FirstOrDefault(w => w.X == newX && w.Y == newY && w.SideX == newSideX && w.SideY == newSideY);
                                newDirection = CalculateDirection(currentDirection, "R", 1);
                            }
                            if (coordinate == null && sideList.Count(c => (c.X == currentPosition.SideX && c.Y == (currentPosition.SideY + 3) % 4)
                                    || (c.X == (currentPosition.SideX + 1) % 4 && c.Y == (currentPosition.SideY + 3) % 4)
                                    || (c.X == (currentPosition.SideX + 1) % 4 && c.Y == (currentPosition.SideY + 2) % 4)
                                    || (c.X == (currentPosition.SideX + 1) % 4 && c.Y == (currentPosition.SideY + 1) % 4)
                                    || (c.X == (currentPosition.SideX + 2) % 4 && c.Y == (currentPosition.SideY + 1) % 4)) == 5)
                            {
                                int newY = 0;
                                int newX = currentPosition.X;
                                int newSideY = (currentPosition.SideY + 1) % 4;
                                int newSideX = (currentPosition.SideX + 2) % 4;
                                coordinate = coordinateList.FirstOrDefault(w => w.X == newX && w.Y == newY && w.SideX == newSideX && w.SideY == newSideY);
                            }
                            if (coordinate == null && sideList.Count(c => (c.X == currentPosition.SideX && c.Y == (currentPosition.SideY + 3) % 4)
                                    || (c.X == (currentPosition.SideX + 3) % 4 && c.Y == (currentPosition.SideY + 3) % 4)
                                    || (c.X == (currentPosition.SideX + 2) % 4 && c.Y == (currentPosition.SideY + 3) % 4)) == 3)
                            {
                                int newY = sideLength - 1;
                                int newX = sideLength - 1 - currentPosition.X;
                                int newSideY = (currentPosition.SideY + 3) % 4;
                                int newSideX = (currentPosition.SideX + 2) % 4;
                                coordinate = coordinateList.FirstOrDefault(w => w.X == newX && w.Y == newY && w.SideX == newSideX && w.SideY == newSideY);
                                newDirection = CalculateDirection(currentDirection, "R", 2);
                            }
                        }
                    }
                    if (coordinate.IsRock)
                        break;
                    else
                    {
                        currentPosition.VisitedSign = "X";
                        currentPosition = coordinate;
                        currentDirection = newDirection;
                        coordinate.VisitedSign = "A";
                    }
                }
            }
            Day22PrintMap2(coordinateList);
            int actualY = currentPosition.Y + sideLength * currentPosition.SideY + 1;
            int actualX = currentPosition.X + sideLength * currentPosition.SideX + 1;
            int sum = actualY * 1000 + actualX * 4 + (currentDirection == 'E' ? 0 : currentDirection == 'S' ? 1 : currentDirection == 'W' ? 2 : 3);
            Debug.WriteLine("Sum: " + sum);
        }

        private char CalculateDirection(char currentDirection, string leftRight, int numberOfTurns)
        {
            for (int i = 0; i < numberOfTurns; i++)
                currentDirection
                    = currentDirection == 'E' && leftRight == "R" ? 'S'
                    : currentDirection == 'E' && leftRight == "L" ? 'N'
                    : currentDirection == 'W' && leftRight == "R" ? 'N'
                    : currentDirection == 'W' && leftRight == "L" ? 'S'
                    : currentDirection == 'N' && leftRight == "R" ? 'E'
                    : currentDirection == 'N' && leftRight == "L" ? 'W'
                    : currentDirection == 'S' && leftRight == "R" ? 'W'
                    : currentDirection == 'S' && leftRight == "L" ? 'E'
                    : throw new Exception("Invalid direction");
            return currentDirection;
        }

        private void Day22PrintMap(HashSet<Day22Coordinate> coordinateList)
        {
            int maxX = coordinateList.Max(m => m.X);
            int maxY = coordinateList.Max(m => m.Y);

            for (int y = 1; y <= maxY; y++)
            {
                string row = string.Empty;
                for (int x = 1; x <= maxX; x++)
                {
                    Day22Coordinate coordinate = coordinateList.FirstOrDefault(w => w.Y == y && w.X == x);
                    if (coordinate == null)
                        row += " ";
                    else if (coordinate.IsRock)
                        row += "#";
                    else if (!string.IsNullOrEmpty(coordinate.VisitedSign))
                        row += coordinate.VisitedSign;
                    else
                        row += ".";
                }
                Debug.WriteLine(row);
            }
        }

        private void Day22PrintMap2(HashSet<Day22Coordinate> coordinateList)
        {
            int maxX = coordinateList.Max(m => m.X);
            int maxY = coordinateList.Max(m => m.Y);
            string grid = string.Empty;
            for (int sideY = 0; sideY < 4; sideY++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    for (int sideX = 0; sideX < 4; sideX++)
                    {
                        for (int x = 0; x <= maxX; x++)
                        {
                            Day22Coordinate coordinate = coordinateList.FirstOrDefault(w => w.Y == y && w.X == x && w.SideX == sideX && w.SideY == sideY);
                            if (coordinate == null)
                                grid += " ";
                            else if (coordinate.IsRock)
                                grid += "#";
                            else if (!string.IsNullOrEmpty(coordinate.VisitedSign))
                                grid += coordinate.VisitedSign;
                            else
                                grid += ".";
                        }
                    }
                    grid += Environment.NewLine;
                }
            }
            Debug.WriteLine(grid);
        }

        private class Day22Coordinate
        {
            public Day22Coordinate()
            {
                IsRock = false;
            }
            public int X { get; set; }
            public int Y { get; set; }
            public bool IsRock { get; set; }
            public string VisitedSign { get; set; }
            public int SideX { get; set; }
            public int SideY { get; set; }
        }

        private class Day22Side
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        [TestMethod]
        public void Day23_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day23.txt").ToList();
            List<Day23Elf> elfList = new List<Day23Elf>();
            int y = 0;
            foreach (string row in inputList)
            {
                int x = 0;
                foreach (char tile in row)
                {
                    if (tile == '#')
                        elfList.Add(new Day23Elf { X = x, Y = y });
                    x++;
                }
                y++;
            }
            List<char> directionList = new List<char> { 'N', 'S', 'W', 'E' };
            Day23PrintGrid(elfList);
            for (int i = 0; i < 10; i++)
            {
                foreach (Day23Elf elf in elfList)
                {
                    elf.Move = false;
                    List<Day23Elf> neighbourElfList = elfList.Where(w => (Math.Abs(w.X - elf.X) + Math.Abs(w.Y - elf.Y) == 1)
                        || (Math.Abs(w.X - elf.X) == 1 && Math.Abs(w.Y - elf.Y) == 1)).ToList();
                    if (neighbourElfList.Count() == 0)
                        continue;
                    foreach (char direction in directionList)
                    {
                        if (direction == 'N')
                        {
                            if (!neighbourElfList.Any(a => a.Y == elf.Y - 1))
                            {
                                elf.MoveX = elf.X;
                                elf.MoveY = elf.Y - 1;
                                elf.Move = true;
                                break;
                            }
                        }
                        else if (direction == 'S')
                        {
                            if (!neighbourElfList.Any(a => a.Y == elf.Y + 1))
                            {
                                elf.MoveX = elf.X;
                                elf.MoveY = elf.Y + 1;
                                elf.Move = true;
                                break;
                            }
                        }
                        else if (direction == 'W')
                        {
                            if (!neighbourElfList.Any(a => a.X == elf.X - 1))
                            {
                                elf.MoveX = elf.X - 1;
                                elf.MoveY = elf.Y;
                                elf.Move = true;
                                break;
                            }
                        }
                        else if (direction == 'E')
                        {
                            if (!neighbourElfList.Any(a => a.X == elf.X + 1))
                            {
                                elf.MoveX = elf.X + 1;
                                elf.MoveY = elf.Y;
                                elf.Move = true;
                                break;
                            }
                        }
                    }
                }
                List<string> invalidDestinationList = elfList.Where(w => w.Move).GroupBy(g => new { X = g.MoveX, Y = g.MoveY })
                    .Select(s => new { X = s.Key.X, Y = s.Key.Y, Count = s.Count() }).Where(w => w.Count > 1).Select(s => s.X + "," + s.Y).ToList();
                elfList.Where(w => w.Move && invalidDestinationList.Contains(w.MoveX + "," + w.MoveY)).ToList().ForEach(e => e.Move = false);
                elfList.Where(w => w.Move).ToList().ForEach(e => { e.X = e.MoveX; e.Y = e.MoveY; });
                char last = directionList.First();
                directionList.Remove(last);
                directionList.Add(last);
                Day23PrintGrid(elfList);
            }
            int emptyTiles = (elfList.Max(m => m.X) + 1 - elfList.Min(m => m.X)) * (elfList.Max(m => m.Y) + 1 - elfList.Min(m => m.Y)) - elfList.Count();
            Debug.WriteLine("Empty tiles: " + emptyTiles);
        }

        [TestMethod]
        public void Day23_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day23.txt").ToList();
            List<Day23Elf> elfList = new List<Day23Elf>();
            int y = 500;
            foreach (string row in inputList)
            {
                int x = 500;
                foreach (char tile in row)
                {
                    if (tile == '#')
                        elfList.Add(new Day23Elf { X = x, Y = y });
                    x++;
                }
                y++;
            }
            List<char> directionList = new List<char> { 'N', 'S', 'W', 'E' };
            int noMoveRound = 0;
            for (int i = 1; true; i++)
            {
                foreach (Day23Elf elf in elfList)
                {
                    List<Day23Elf> neighbourElfList = elfList.Where(w => (Math.Abs(w.X - elf.X) + Math.Abs(w.Y - elf.Y) == 1)
                        || (Math.Abs(w.X - elf.X) == 1 && Math.Abs(w.Y - elf.Y) == 1)).ToList();
                    if (neighbourElfList.Count() == 0)
                        continue;
                    foreach (char direction in directionList)
                    {
                        if (direction == 'N')
                        {
                            if (!neighbourElfList.Any(a => a.Y == elf.Y - 1))
                            {
                                elf.MoveX = elf.X;
                                elf.MoveY = elf.Y - 1;
                                elf.Move = true;
                                break;
                            }
                        }
                        else if (direction == 'S')
                        {
                            if (!neighbourElfList.Any(a => a.Y == elf.Y + 1))
                            {
                                elf.MoveX = elf.X;
                                elf.MoveY = elf.Y + 1;
                                elf.Move = true;
                                break;
                            }
                        }
                        else if (direction == 'W')
                        {
                            if (!neighbourElfList.Any(a => a.X == elf.X - 1))
                            {
                                elf.MoveX = elf.X - 1;
                                elf.MoveY = elf.Y;
                                elf.Move = true;
                                break;
                            }
                        }
                        else if (direction == 'E')
                        {
                            if (!neighbourElfList.Any(a => a.X == elf.X + 1))
                            {
                                elf.MoveX = elf.X + 1;
                                elf.MoveY = elf.Y;
                                elf.Move = true;
                                break;
                            }
                        }
                    }
                }
                List<string> invalidDestinationList = elfList.Where(w => w.Move).GroupBy(g => new { X = g.MoveX, Y = g.MoveY })
                    .Select(s => new { X = s.Key.X, Y = s.Key.Y, Count = s.Count() }).Where(w => w.Count > 1).Select(s => s.X + "," + s.Y).ToList();
                elfList.Where(w => w.Move && invalidDestinationList.Contains(w.MoveX + "," + w.MoveY)).ToList().ForEach(e => e.Move = false);
                int moveCount = elfList.Count(c => c.Move);
                if (moveCount == 0)
                {
                    noMoveRound = i;
                    break;
                }
                elfList.Where(w => w.Move).ToList().ForEach(e => { e.X = e.MoveX; e.Y = e.MoveY; e.Move = false; });
                char last = directionList.First();
                directionList.Remove(last);
                directionList.Add(last);
            }
            Debug.WriteLine("No move round: " + noMoveRound);
        }

        private void Day23PrintGrid(List<Day23Elf> elfList)
        {
            int maxX = elfList.Max(m => m.X) + 1;
            int maxY = elfList.Max(m => m.Y) + 1;
            int minX = elfList.Min(m => m.X) - 1;
            int minY = elfList.Min(m => m.Y) - 1;
            string grid = string.Empty;
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (elfList.Any(w => w.X == x && w.Y == y))
                        grid += "#";
                    else
                        grid += ".";
                }
                grid += Environment.NewLine;
            }
            Debug.WriteLine(grid);
        }

        private class Day23Elf
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool Move { get; set; }
            public int MoveX { get; set; }
            public int MoveY { get; set; }
        }

        [TestMethod]
        public void Day24_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day24.txt").ToList();
            List<List<Day24Blizzard>> blizzardTimeList = new List<List<Day24Blizzard>>();
            List<Day24Blizzard> blizzardList = new List<Day24Blizzard>();
            List<Day24Location> locationList = new List<Day24Location>();
            int y = 0;
            int x = 0;
            foreach (string input in inputList)
            {
                x = 0;
                foreach (char c in input)
                {
                    locationList.Add(new Day24Location { X = x, Y = y, IsWall = c == '#', IsEnd = false });
                    if (c != '.' && c != '#')
                        blizzardList.Add(new Day24Blizzard { X = x, Y = y, BlizzardDirection = c });
                    x++;
                }
                y++;
            }
            blizzardTimeList.Add(blizzardList);
            int yMax = y - 1;
            int xMax = x - 1;
            int maxMinutesAllowed = 500;
            for (int minute = 1; minute < maxMinutesAllowed; minute++)
            {
                List<Day24Blizzard> nextBlizzardList = new List<Day24Blizzard>();
                foreach (Day24Blizzard blizzard in blizzardList)
                {
                    Day24Blizzard movedBlizzard = new Day24Blizzard { BlizzardDirection = blizzard.BlizzardDirection, X = blizzard.X, Y = blizzard.Y };
                    if (movedBlizzard.BlizzardDirection == '>')
                    {
                        movedBlizzard.X++;
                        if (movedBlizzard.X == xMax)
                            movedBlizzard.X = 1;
                    }
                    else if (movedBlizzard.BlizzardDirection == '<')
                    {
                        movedBlizzard.X--;
                        if (movedBlizzard.X == 0)
                            movedBlizzard.X = xMax - 1;
                    }
                    else if (movedBlizzard.BlizzardDirection == '^')
                    {
                        movedBlizzard.Y--;
                        if (movedBlizzard.Y == 0)
                            movedBlizzard.Y = yMax - 1;
                    }
                    else if (blizzard.BlizzardDirection == 'v')
                    {
                        movedBlizzard.Y++;
                        if (movedBlizzard.Y == yMax)
                            movedBlizzard.Y = 1;
                    }
                    nextBlizzardList.Add(movedBlizzard);
                }
                blizzardTimeList.Add(nextBlizzardList);
                blizzardList = nextBlizzardList;
            }
            Day24Location current = null;
            List<Day24Location> locationQueue = new List<Day24Location>();
            Day24Location endLocation = locationList.Last(w => !w.IsWall);
            Day24Location startLocation = locationList.First(w => !w.IsWall);
            locationList.Where(w => !w.IsWall).ToList().ForEach(e => e.TargetDistance = endLocation.X - e.X + endLocation.Y - e.Y);
            locationQueue.Add(startLocation);
            int loopCount = 0;
            int queueMinutes = 0;
            int record = maxMinutesAllowed;
            while (true)
            {
                current = locationQueue.OrderBy(o => o.CurrentMinutes + o.TargetDistance).FirstOrDefault();
                if (current.X == endLocation.X && current.Y == endLocation.Y)
                {
                    if (record > current.CurrentMinutes)
                    {
                        Debug.WriteLine("Solution found: " + current.CurrentMinutes);
                        record = current.CurrentMinutes;
                    }
                    locationQueue.Remove(current);
                    continue;
                }
                queueMinutes = current.CurrentMinutes;
                locationQueue.Remove(current);
                current = locationList.First(w => w.X == current.X && w.Y == current.Y);
                List<Day24Blizzard> nextBlizzardList = blizzardTimeList[queueMinutes + 1];
                List<Day24Location> nextLocationList = locationList.Where(w => !w.IsWall && Math.Abs(w.X - current.X) + Math.Abs(w.Y - current.Y) == 1).ToList();
                nextLocationList.Add(current);
                foreach (Day24Location location in nextLocationList.Where(w =>
                    ((w.X > 0 && w.Y > 0 && w.X < xMax && w.Y < yMax) || (w.X == endLocation.X && w.Y == endLocation.Y))
                    && !nextBlizzardList.Any(a => a.X == w.X && a.Y == w.Y)))
                {
                    Day24Location queueLocation = new Day24Location();
                    queueLocation.X = location.X;
                    queueLocation.Y = location.Y;
                    queueLocation.TargetDistance = location.TargetDistance;
                    queueLocation.CurrentMinutes = queueMinutes + 1;
                    if (queueLocation.TargetDistance + queueLocation.CurrentMinutes >= record)
                        continue;
                    if (!locationQueue.Any(a => a.X == queueLocation.X && a.Y == queueLocation.Y && a.CurrentMinutes == queueLocation.CurrentMinutes))
                        locationQueue.Add(queueLocation);
                }
                if (locationQueue.Count() == 0)
                    break;
            }
            Debug.WriteLine("Distance: " + record);
            //Day24PrintGrid(blizzardTimeList[current.MinDistance], current.X, current.Y);
        }

        [TestMethod]
        public void Day24_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day24.txt").ToList();
            List<List<Day24Blizzard>> blizzardTimeList = new List<List<Day24Blizzard>>();
            List<Day24Blizzard> blizzardList = new List<Day24Blizzard>();
            List<Day24Location> locationList = new List<Day24Location>();
            int y = 0;
            int x = 0;
            foreach (string input in inputList)
            {
                x = 0;
                foreach (char c in input)
                {
                    locationList.Add(new Day24Location { X = x, Y = y, IsWall = c == '#', IsEnd = false });
                    if (c != '.' && c != '#')
                        blizzardList.Add(new Day24Blizzard { X = x, Y = y, BlizzardDirection = c });
                    x++;
                }
                y++;
            }
            blizzardTimeList.Add(blizzardList);
            int yMax = y - 1;
            int xMax = x - 1;
            int maxMinutesAllowed = 1500;
            for (int minute = 1; minute < maxMinutesAllowed; minute++)
            {
                List<Day24Blizzard> nextBlizzardList = new List<Day24Blizzard>();
                foreach (Day24Blizzard blizzard in blizzardList)
                {
                    Day24Blizzard movedBlizzard = new Day24Blizzard { BlizzardDirection = blizzard.BlizzardDirection, X = blizzard.X, Y = blizzard.Y };
                    if (movedBlizzard.BlizzardDirection == '>')
                    {
                        movedBlizzard.X++;
                        if (movedBlizzard.X == xMax)
                            movedBlizzard.X = 1;
                    }
                    else if (movedBlizzard.BlizzardDirection == '<')
                    {
                        movedBlizzard.X--;
                        if (movedBlizzard.X == 0)
                            movedBlizzard.X = xMax - 1;
                    }
                    else if (movedBlizzard.BlizzardDirection == '^')
                    {
                        movedBlizzard.Y--;
                        if (movedBlizzard.Y == 0)
                            movedBlizzard.Y = yMax - 1;
                    }
                    else if (blizzard.BlizzardDirection == 'v')
                    {
                        movedBlizzard.Y++;
                        if (movedBlizzard.Y == yMax)
                            movedBlizzard.Y = 1;
                    }
                    nextBlizzardList.Add(movedBlizzard);
                }
                blizzardTimeList.Add(nextBlizzardList);
                blizzardList = nextBlizzardList;
            }
            Day24Location endLocation = locationList.Last(w => !w.IsWall);
            Day24Location startLocation = locationList.First(w => !w.IsWall);
            int toEnd = Day24ShortestTime(locationList, blizzardTimeList, maxMinutesAllowed, startLocation.X, startLocation.Y, endLocation.X, endLocation.Y, 0);
            Debug.WriteLine("Time: " + toEnd);
            int toStart = Day24ShortestTime(locationList, blizzardTimeList, maxMinutesAllowed, endLocation.X, endLocation.Y, startLocation.X, startLocation.Y, toEnd);
            Debug.WriteLine("Time: " + toStart);
            int toEnd2 = Day24ShortestTime(locationList, blizzardTimeList, maxMinutesAllowed, startLocation.X, startLocation.Y, endLocation.X, endLocation.Y, toStart + toEnd);
            Debug.WriteLine("Time: " + toEnd2);
            Debug.WriteLine("Total time: " + toEnd + toStart + toEnd2);
        }

        private int Day24ShortestTime(List<Day24Location> locationList, List<List<Day24Blizzard>> blizzardTimeList, int maxMinutesAllowed
            , int startX, int startY, int endX, int endY, int startMinute)
        {
            int xMax = locationList.Max(m => m.X);
            int yMax = locationList.Max(m => m.Y);
            Day24Location current = null;
            List<Day24Location> locationQueue = new List<Day24Location>();
            Day24Location endLocation = locationList.Last(w => w.X == endX && w.Y == endY);
            Day24Location startLocation = locationList.First(w => w.X == startX && w.Y == startY);
            locationList.Where(w => !w.IsWall).ToList().ForEach(e => e.TargetDistance = Math.Abs(endLocation.X - e.X) + Math.Abs(endLocation.Y - e.Y));
            startLocation.CurrentMinutes = startMinute;
            locationQueue.Add(startLocation);
            int loopCount = 0;
            int queueMinutes = 0;
            int record = maxMinutesAllowed;
            while (true)
            {
                current = locationQueue.OrderBy(o => o.CurrentMinutes + o.TargetDistance).FirstOrDefault();
                if (current.X == endLocation.X && current.Y == endLocation.Y)
                {
                    if (record > current.CurrentMinutes)
                    {
                        Debug.WriteLine("Solution found: " + current.CurrentMinutes);
                        record = current.CurrentMinutes;
                    }
                    locationQueue.Remove(current);
                    continue;
                }
                queueMinutes = current.CurrentMinutes;
                locationQueue.Remove(current);
                current = locationList.First(w => w.X == current.X && w.Y == current.Y);
                List<Day24Blizzard> nextBlizzardList = blizzardTimeList[queueMinutes + 1];
                List<Day24Location> nextLocationList = locationList.Where(w => !w.IsWall && Math.Abs(w.X - current.X) + Math.Abs(w.Y - current.Y) == 1).ToList();
                nextLocationList.Add(current);
                foreach (Day24Location location in nextLocationList.Where(w =>
                    ((w.X > 0 && w.Y > 0 && w.X < xMax && w.Y < yMax)
                    || (w.X == endLocation.X && w.Y == endLocation.Y)
                    || (w.X == startLocation.X && w.Y == startLocation.Y))
                    && !nextBlizzardList.Any(a => a.X == w.X && a.Y == w.Y)))
                {
                    Day24Location queueLocation = new Day24Location();
                    queueLocation.X = location.X;
                    queueLocation.Y = location.Y;
                    queueLocation.TargetDistance = location.TargetDistance;
                    queueLocation.CurrentMinutes = queueMinutes + 1;
                    if (queueLocation.TargetDistance + queueLocation.CurrentMinutes >= record)
                        continue;
                    if (!locationQueue.Any(a => a.X == queueLocation.X && a.Y == queueLocation.Y && a.CurrentMinutes == queueLocation.CurrentMinutes))
                        locationQueue.Add(queueLocation);
                }
                if (locationQueue.Count() == 0)
                    break;
            }
            return record - startMinute;
        }

        private void Day24PrintGrid(List<Day24Blizzard> blizzardList, int? currentX, int? currentY)
        {
            int maxX = blizzardList.Max(m => m.X) + 1;
            int maxY = blizzardList.Max(m => m.Y) + 1;
            int minX = blizzardList.Min(m => m.X) - 1;
            int minY = blizzardList.Min(m => m.Y) - 1;
            string grid = string.Empty;
            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    List<Day24Blizzard> tileList = blizzardList.Where(w => w.X == x && w.Y == y).ToList();
                    if (x == currentX && y == currentY)
                        grid += 'E';
                    else if (tileList.Count() > 1)
                        grid += tileList.Count();
                    else if (tileList.Count() == 1)
                        grid += tileList.First().BlizzardDirection;
                    else
                        grid += ".";
                }
                grid += Environment.NewLine;
            }
            Debug.WriteLine(grid);
        }

        private void Day24PrintGrid2(List<Day24Location> locationList)
        {
            int maxX = locationList.Max(m => m.X) + 1;
            int maxY = locationList.Max(m => m.Y) + 1;
            int minX = locationList.Min(m => m.X) - 1;
            int minY = locationList.Min(m => m.Y) - 1;
            string grid = string.Empty;
            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    List<Day24Location> tileList = locationList.Where(w => w.X == x && w.Y == y).ToList();
                    if (tileList.Count() > 0)
                        grid += "X";
                    else
                        grid += ".";
                }
                grid += Environment.NewLine;
            }
            Debug.WriteLine(grid);
        }

        private class Day24Location
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool IsEnd { get; set; }
            public bool IsWall { get; set; }
            public int TargetDistance { get; set; }
            public int CurrentMinutes { get; set; }
        }

        private class Day24Blizzard
        {
            public int X { get; set; }
            public int Y { get; set; }
            public char BlizzardDirection { get; set; }
        }

        [TestMethod]
        public void Day25()
        {
            List<Int64> fuelList = new List<Int64>();
            List<string> inputList = File.ReadAllLines(@"Input\Day25.txt").ToList();
            foreach (string input in inputList)
                fuelList.Add(ParseSNAFUToDecimal(input));
            string SNAFU = ParseDecimalToSNAFU(fuelList.Sum());
            Debug.WriteLine("Total fuel: " + SNAFU);
        }

        private Int64 ParseSNAFUToDecimal(string snafu)
        {
            Int64 result = 0;
            int i = 0;
            Int64 number;
            foreach (char c in snafu.Reverse())
            {
                number = c == '1' ? 1 : c == '2' ? 2 : c == '-' ? -1 : c == '=' ? -2 : 0;
                result += (Int64)Math.Pow(5, i) * number;
                i++;
            }
            return result;
        }

        private string ParseDecimalToSNAFU(Int64 number)
        {
            //4890 / 3125 = 2
            //4890 - 3125 * 2 = -1360
            //- 1360 / 625 = -2
            //- 1360 - -2 * 625 = -110
            //- 110 / 125 = -1
            //- 110 - -1 * 125 = 15
            //15 / 25 = 1
            //15 - 1 * 25 = -10
            //- 10 / 5 = -2
            //- 10 - -2 * 5 = 0
            //0 / 1 = 0
            string snafu = string.Empty;
            Int64 result = number;
            int i = 0;
            while (result > 0)
                result = number / (Int64)Math.Pow(5, ++i);
            for (int x = i - 1; x >= 0; x--)
            {
                Int64 val = (Int64)Math.Round((double)number / Math.Pow(5, x));
                snafu += val == 1 ? "1" : val == 2 ? "2" : val == -2 ? "=" : val == -1 ? "-" : "0";
                number = number - val * (Int64)Math.Pow(5, x);
            }
            return snafu;
        }
    }
}


