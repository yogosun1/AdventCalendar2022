using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Diagnostics.Metrics;
using System.Collections;
using System.Xml;

namespace AdventCalendar2022
{
    [TestClass]
    public class WorkInProgress
    {
        //[TestMethod]
        //public void Day24_1()
        //{
        //    List<string> inputList = File.ReadAllLines(@"Input\Day24.txt").ToList();
        //    List<List<Day24Blizzard>> blizzardTimeList = new List<List<Day24Blizzard>>();
        //    List<Day24Blizzard> blizzardList = new List<Day24Blizzard>();
        //    int y = 0;
        //    foreach (string input in inputList)
        //    {
        //        int x = 0;
        //        foreach (char c in input)
        //        {
        //            if (c != '.' && c != '#')
        //                blizzardList.Add(new Day24Blizzard { X = x, Y = y, BlizzardDirection = c });
        //            x++;
        //        }
        //        y++;
        //    }
        //    blizzardTimeList.Add(blizzardList);
        //    int yMax = blizzardList.Max(m => m.Y) + 1;
        //    int xMax = blizzardList.Max(m => m.X) + 1;
        //    int maxMinutesAllowed = 500;
        //    for (int minute = 1; minute < maxMinutesAllowed; minute++)
        //    {
        //        List<Day24Blizzard> nextBlizzardList = new List<Day24Blizzard>();
        //        foreach (Day24Blizzard blizzard in blizzardList)
        //        {
        //            Day24Blizzard movedBlizzard = new Day24Blizzard { BlizzardDirection = blizzard.BlizzardDirection, X = blizzard.X, Y = blizzard.Y };
        //            if (movedBlizzard.BlizzardDirection == '>')
        //            {
        //                movedBlizzard.X++;
        //                if (movedBlizzard.X == xMax)
        //                    movedBlizzard.X = 1;
        //            }
        //            else if (movedBlizzard.BlizzardDirection == '<')
        //            {
        //                movedBlizzard.X--;
        //                if (movedBlizzard.X == 0)
        //                    movedBlizzard.X = xMax - 1;
        //            }
        //            else if (movedBlizzard.BlizzardDirection == '^')
        //            {
        //                movedBlizzard.Y--;
        //                if (movedBlizzard.Y == 0)
        //                    movedBlizzard.Y = yMax - 1;
        //            }
        //            else if (blizzard.BlizzardDirection == 'v')
        //            {
        //                movedBlizzard.Y++;
        //                if (movedBlizzard.Y == yMax)
        //                    movedBlizzard.Y = 1;
        //            }
        //            nextBlizzardList.Add(movedBlizzard);
        //        }
        //        blizzardTimeList.Add(nextBlizzardList);
        //        blizzardList = nextBlizzardList;
        //    }
        //    RecordMove recordMove = new RecordMove { MinMove = maxMinutesAllowed };
        //    SimulateBlizzardMove(0, 1, 0, yMax, xMax, string.Empty, maxMinutesAllowed, blizzardTimeList[1], blizzardTimeList, recordMove);
        //    Debug.WriteLine("Record move: " + recordMove.MinMove);
        //}

        //[TestMethod]
        //public void Day24_2()
        //{
        //    List<string> inputList = File.ReadAllLines(@"Input\Day24.txt").ToList();
        //    foreach (string input in inputList)
        //    {
        //    }
        //}

        //private void SimulateBlizzardMove(int minutes, int currentX, int currentY, int yMax, int xMax, string moves, int minDistanceToTarget, List<Day24Blizzard> blizzardList, List<List<Day24Blizzard>> blizzardTimeList, RecordMove recordMove)
        //{
        //    minutes++;
        //    int distanceToTarget = yMax - currentY + xMax - 1 - currentX;
        //    if (minutes >= recordMove.MinMove 
        //        || (minutes + distanceToTarget) >= recordMove.MinMove
        //        || distanceToTarget - minDistanceToTarget > 5)
        //        return;
        //    if (distanceToTarget < minDistanceToTarget)
        //        minDistanceToTarget = distanceToTarget;
        //    if (currentY == yMax && currentX == xMax - 1)
        //    {
        //        if (minutes <= recordMove.MinMove)
        //        {
        //            recordMove.MinMove = minutes - 1;
        //            recordMove.Moves = moves;
        //            Debug.WriteLine("New record found: " + recordMove.MinMove + " Moves: " + moves);
        //            return;
        //        }
        //    }
        //    List<Day24Blizzard> neighbourList = blizzardList.Where(w => Math.Abs(w.X - currentX) + Math.Abs(w.Y - currentY) == 1).ToList();
        //    if ((currentY < yMax - 1 || currentX == xMax - 1) && !neighbourList.Any(a => currentX == a.X && a.Y == currentY + 1))
        //        SimulateBlizzardMove(minutes, currentX, currentY + 1, yMax, xMax, moves + 'S', minDistanceToTarget, blizzardTimeList[minutes + 1], blizzardTimeList, recordMove);
        //    if (currentX < xMax - 1 && currentY > 0 && !neighbourList.Any(a => a.X == currentX + 1 && currentY == a.Y))
        //        SimulateBlizzardMove(minutes, currentX + 1, currentY, yMax, xMax, moves + 'E', minDistanceToTarget, blizzardTimeList[minutes + 1], blizzardTimeList, recordMove);
        //    if (currentY > 1 && !neighbourList.Any(a => currentX == a.X && a.Y == currentY - 1))
        //        SimulateBlizzardMove(minutes, currentX, currentY - 1, yMax, xMax, moves + 'N', minDistanceToTarget, blizzardTimeList[minutes + 1], blizzardTimeList, recordMove);
        //    if (currentX > 1 && currentY > 0 && !neighbourList.Any(a => a.X == currentX - 1 && currentY == a.Y))
        //        SimulateBlizzardMove(minutes, currentX - 1, currentY, yMax, xMax, moves + 'W', minDistanceToTarget, blizzardTimeList[minutes + 1], blizzardTimeList, recordMove);
        //    if (!neighbourList.Any(a => a.X == currentX && a.Y == currentY))
        //        SimulateBlizzardMove(minutes, currentX, currentY, yMax, xMax, moves + "-", minDistanceToTarget, blizzardTimeList[minutes + 1], blizzardTimeList, recordMove);
        //}

        //private void Day24PrintGrid(List<Day24Blizzard> blizzardList, int? currentX, int? currentY)
        //{
        //    int maxX = blizzardList.Max(m => m.X) + 1;
        //    int maxY = blizzardList.Max(m => m.Y) + 1;
        //    int minX = blizzardList.Min(m => m.X) - 1;
        //    int minY = blizzardList.Min(m => m.Y) - 1;
        //    string grid = string.Empty;
        //    for (int y = 0; y <= maxY; y++)
        //    {
        //        for (int x = 0; x <= maxX; x++)
        //        {
        //            List<Day24Blizzard> tileList = blizzardList.Where(w => w.X == x && w.Y == y).ToList();
        //            if (x == currentX && y == currentY)
        //                grid += 'E';
        //            else if (tileList.Count() > 1)
        //                grid += tileList.Count();
        //            else if (tileList.Count() == 1)
        //                grid += tileList.First().BlizzardDirection;
        //            else
        //                grid += ".";
        //        }
        //        grid += Environment.NewLine;
        //    }
        //    Debug.WriteLine(grid);
        //}

        //private class Day24Blizzard
        //{
        //    public int X { get; set; }
        //    public int Y { get; set; }
        //    public char BlizzardDirection { get; set; }
        //}

        //private class RecordMove
        //{
        //    public int MinMove { get; set; }
        //    public string Moves { get; set; }
        //}

        [TestMethod]
        public void Day24_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day24Test.txt").ToList();
            List<List<Day24Blizzard>> blizzardTimeList = new List<List<Day24Blizzard>>();
            List<Day24Blizzard> blizzardList = new List<Day24Blizzard>();
            //List<Day24Location> locationList = new List<Day24Location>();
            int y = 0;
            int x = 0;
            foreach (string input in inputList)
            {
                x = 0;
                foreach (char c in input)
                {
                    //locationList.Add(new Day24Location { X = x, Y = y, IsWall = c == '#', IsVisited = false, IsEnd = false, MinDistance = int.MaxValue });
                    if (c != '.' && c != '#')
                        blizzardList.Add(new Day24Blizzard { X = x, Y = y, BlizzardDirection = c });
                    x++;
                }
                y++;
            }
            //Day24Location endLocation = locationList.Last(l => !l.IsWall);
            //endLocation.IsEnd = true;
            //locationList.Where(w => !w.IsWall && !w.IsEnd).ToList().ForEach(e => e.TargetDistance = endLocation.X - e.X + endLocation.Y + e.Y);
            blizzardTimeList.Add(blizzardList);
            int yMax = y;
            int xMax = x;
            int maxMinutesAllowed = 1000;
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
            Day24Location endLocation = new Day24Location { X = xMax - 1, Y = yMax };
            locationQueue.Add(new Day24Location { X = 1, Y = 0 });

            while (current == null || !(current.X == xMax - 1 && current.Y == yMax))
            {
                current = locationQueue.OrderBy(o => o.MinDistance + o.TargetDistance).First();
                List<Day24Blizzard> nextBlizzardList = blizzardTimeList[current.MinDistance + 1];
                List<Day24Location> nextLocationList = new List<Day24Location> {
                    new Day24Location { X = current.X - 1, Y = current.Y }
                    ,new Day24Location { X = current.X + 1, Y = current.Y }
                    ,new Day24Location { X = current.X, Y = current.Y - 1 }
                    ,new Day24Location { X = current.X, Y = current.Y + 1 }
                    ,new Day24Location { X = current.X, Y = current.Y }
                };
                foreach (Day24Location location in nextLocationList.Where(w => 
                    ((w.X > 0 && w.Y > 0 && w.X < xMax && w.Y < yMax) || (w.X == endLocation.X && w.Y == endLocation.Y))
                    && !nextBlizzardList.Any(a => a.X == w.X && a.Y == w.Y)))
                {
                    location.MinDistance = current.MinDistance + 1;
                    location.TargetDistance = endLocation.X - location.X + endLocation.Y - location.Y;
                    locationQueue.Add(location);
                }
                locationQueue.Remove(current);
            }
            Debug.WriteLine("Distance: " + current.MinDistance);
        }

        private class Day24Location
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool IsEnd { get; set; }
            public bool IsWall { get; set; }
            public bool IsVisited { get; set; }
            public int MinDistance { get; set; }
            public int TargetDistance { get; set; }
        }

        private class Day24Blizzard
        {
            public int X { get; set; }
            public int Y { get; set; }
            public char BlizzardDirection { get; set; }
        }

        [TestMethod]
        public void Day25_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day25Test.txt").ToList();
            foreach (string input in inputList)
            {
            }
        }

        [TestMethod]
        public void Day25_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day25Test.txt").ToList();
            foreach (string input in inputList)
            {
            }
        }
    }
}
