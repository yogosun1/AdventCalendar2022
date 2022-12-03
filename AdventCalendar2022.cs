using System.Linq;
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
                string secondRucksack = inputList[i+1];
                string thidRucksack = inputList[i+2];
                foreach (char item in firstRucksack)
                {
                    if (secondRucksack.Contains(item) && thidRucksack.Contains(item))
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
    }
}