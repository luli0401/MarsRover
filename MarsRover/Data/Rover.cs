using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Data
{
    public class Rover
    {
        public int XAxis { get; set; }
        public int YAxis { get; set; }
        public string Direction { get; set; }

        public Rover(int x, int y, string d)
        {
            XAxis = x;
            YAxis = y;
            Direction = d;
        }

        public void MoveAxis(int x, int y)
        {
            XAxis += x;
            YAxis += y;
        }

    }
}
