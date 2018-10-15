using System.Collections.Generic;

namespace MarsRover.Repo
{
    public class Service
    {

        //Fake Datebase
        public static IList<string> GetInput()
        {
            return new List<string>
            {
                "5 5",
                "1 2 N",
                "LMLMLMLMM",
                "3 3 E",
                "MMRMMRMRRM"
            };
        }
    }
}
