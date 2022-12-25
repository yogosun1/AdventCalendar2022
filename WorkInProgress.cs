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
using System.Diagnostics.CodeAnalysis;

namespace AdventCalendar2022
{
    [TestClass]
    public class WorkInProgress
    {
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
    }
}
