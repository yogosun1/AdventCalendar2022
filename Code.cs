using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCalendar2022
{
    [TestClass]
    public class Code
    {
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
                if (currentDirection == 'E' || currentDirection == 'W')
                {
                    List<Day22Coordinate> row = coordinateList.Where(w => w.Y == currentPosition.Y).ToList();
                    if (currentDirection == 'E')
                    {
                        for (int i = 0; i < distance; i++)
                        {
                            Day22Coordinate coordinate = row.FirstOrDefault(w => w.X == currentPosition.X + 1);
                            if (coordinate == null)
                                coordinate = row.OrderBy(o => o.X).First();
                            if (coordinate.IsRock)
                                break;
                            else
                            {
                                currentPosition = coordinate;
                                coordinate.VisitedSign = ">";
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < distance; i++)
                        {
                            Day22Coordinate coordinate = row.FirstOrDefault(w => w.X == currentPosition.X - 1);
                            if (coordinate == null)
                                coordinate = row.OrderByDescending(o => o.X).First();
                            if (coordinate.IsRock)
                                break;
                            else
                            {
                                currentPosition = coordinate;
                                coordinate.VisitedSign = "<";
                            }
                        }
                    }
                }
                else
                {
                    List<Day22Coordinate> column = coordinateList.Where(w => w.X == currentPosition.X).ToList();
                    if (currentDirection == 'N')
                    {
                        for (int i = 0; i < distance; i++)
                        {
                            Day22Coordinate coordinate = column.FirstOrDefault(w => w.Y == currentPosition.Y - 1);
                            if (coordinate == null)
                                coordinate = column.OrderByDescending(o => o.Y).First();
                            if (coordinate.IsRock)
                                break;
                            else
                            {
                                currentPosition = coordinate;
                                coordinate.VisitedSign = "A";
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < distance; i++)
                        {
                            Day22Coordinate coordinate = column.FirstOrDefault(w => w.Y == currentPosition.Y + 1);
                            if (coordinate == null)
                                coordinate = column.OrderBy(o => o.Y).First();
                            if (coordinate.IsRock)
                                break;
                            else
                            {
                                currentPosition = coordinate;
                                coordinate.VisitedSign = "v";
                            }
                        }
                    }
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
            //Day22PrintMap2(coordinateList, true);
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
                for (int i = 0; i < distance; i++)
                {
                    //Day22PrintMap2(coordinateList, false);
                    if (currentDirection == 'E' || currentDirection == 'W')
                    {
                        if (currentDirection == 'E')
                        {
                            char newDirection = currentDirection;
                            Day22Coordinate coordinate = coordinateList.FirstOrDefault(w => w.X == currentPosition.X + 1 && w.Y == currentPosition.Y && w.SideX == currentPosition.SideX && w.SideY == currentPosition.SideY);
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
                        else
                        {
                            char newDirection = currentDirection;
                            Day22Coordinate coordinate = coordinateList.FirstOrDefault(w => w.X == currentPosition.X - 1 && w.Y == currentPosition.Y && w.SideX == currentPosition.SideX && w.SideY == currentPosition.SideY);
                            if (coordinate == null) // west
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
                    else
                    {
                        if (currentDirection == 'N')
                        {
                            char newDirection = currentDirection;
                            Day22Coordinate coordinate = coordinateList.FirstOrDefault(w => w.X == currentPosition.X && w.Y == currentPosition.Y - 1 && w.SideX == currentPosition.SideX && w.SideY == currentPosition.SideY);
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
                        else
                        {
                            char newDirection = currentDirection;
                            Day22Coordinate coordinate = coordinateList.FirstOrDefault(w => w.X == currentPosition.X && w.Y == currentPosition.Y + 1 && w.SideX == currentPosition.SideX && w.SideY == currentPosition.SideY);
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
                }
            }
            //Day22PrintMap2(coordinateList, false);
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
    }
}
