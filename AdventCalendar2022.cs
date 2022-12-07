using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

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

        //[TestMethod]
        //public void Day7()
        //{
        //    List<string> inputList = File.ReadAllLines(@"Input\Day7.txt").ToList();
        //    List<Directory> directoryList = new List<Directory>();
        //    Directory? currentDirectory = null;

        //    foreach (string input in inputList)
        //    {
        //        List<string> inputSplit = input.Split(' ').ToList();
        //        if (input.StartsWith('$')) // Command
        //        {
        //            string command = inputSplit[1];
        //            if (command == "cd") // change directory
        //            {
        //                string folder = inputSplit[2];
        //                if (folder == "..")
        //                {
        //                    if (currentDirectory?.Parent != null)
        //                        currentDirectory = currentDirectory.Parent;
        //                    else
        //                        throw new Exception("No parent exists");
        //                }
        //                else
        //                {
        //                    Directory? directory = directoryList.FirstOrDefault(w => w.Path == currentDirectory?.Path + folder);
        //                    if (directory == null)
        //                    {
        //                        directory = new Directory { Name = folder, Path = currentDirectory?.Path + folder, Parent = currentDirectory };
        //                        directoryList.Add(directory);
        //                    }
        //                    currentDirectory = directory;
        //                }
        //            }
        //            else if (command == "ls") // list directory
        //                continue;
        //        }
        //        else if (input.StartsWith("dir")) // directory
        //        {
        //            string folder = inputSplit[1];
        //            Directory? directory = directoryList.FirstOrDefault(w => w.Path == currentDirectory?.Path + folder);
        //            if (directory == null)
        //            {
        //                directory = new Directory { Name = folder, Path = currentDirectory?.Path + folder, Parent = currentDirectory };
        //                directoryList.Add(directory);
        //            }
        //        }
        //        else // file
        //        {
        //            int size = int.Parse(inputSplit[0]);
        //            string name = inputSplit[1];
        //            SystemFile? systemFile = currentDirectory?.FileList.FirstOrDefault(w => w.Name == name);
        //            if (systemFile == null && currentDirectory != null)
        //            {
        //                systemFile = new SystemFile { Size = size, Name = name };
        //                currentDirectory.FileList.Add(systemFile);
        //            }
        //        }
        //    }

        //}

        //private class Directory
        //{
        //    public Directory()
        //    {
        //        FileList = new List<SystemFile>();
        //    }

        //    public string? Name { get; set; }
        //    public string? Path { get; set; }
        //    public Directory? Parent { get; set; }
        //    public List<SystemFile> FileList { get; set; }
        //}

        //private class SystemFile
        //{
        //    public int Size { get; set; }
        //    public string? Name { get; set; }
        //}

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
    }
}