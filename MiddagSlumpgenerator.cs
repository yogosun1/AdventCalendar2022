using System.Diagnostics;
using System.Linq;

namespace AdventCalendar2022
{
    [TestClass]
    public class MiddagSlumpgenerator
    {
        [TestMethod]
        public void Slumpgenerator()
        {
            List<string> nameList = new List<string> { "Johan", "Daniel B", "Linus", "Thomas", "Mikaela" };
            Random rnd = new Random();
            string name = nameList[rnd.Next(nameList.Count)];
            Debug.Write(name);
        }
    }
}