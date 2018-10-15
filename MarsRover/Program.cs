using MarsRover.Helper;
using MarsRover.Repo;
using MarsRover.Data;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Diagnostics;

namespace MarsRover
{
    public class Program
    {
        public static int XMax { get; set; }
        public static int YMax { get; set; }
        public static Rover MarsRover { get; set; }

        static void Main(string[] args)
        {
            var input = Service.GetInput();

            InitialPlateau(input);
            SetMarsRover(input);

            RunTestCases();
        }

        private static void RunTestCases()
        {
            TestInitialPlateau();
            TestInitialRover();
            TestMoveRover();
            TestSetMarsRover();
        }

        public static void SetMarsRover(IList<string> input)
        {
            MarsRover = null;

            if (input != null && input.Any())
            {
                for (int i = 1; i < input.Count; i++)
                {
                    if (i % 2 == 1)
                    {
                        InitialRover(input[i]);
                    }
                    else
                    {
                        MoveRover(input[i]);
                    }
                }
            }
        }

        private static void MoveRover(string commandString)
        {
            if (MarsRover != null && !string.IsNullOrEmpty(commandString))
            {
                var directionProcessor = new RoverDirectionProcessor();

                foreach (char command in commandString)
                {
                    if (char.ToUpperInvariant(command) == 'M')
                    {
                        directionProcessor.MoveForward(MarsRover);

                        FixRoverPosition();
                    }
                    else if (char.ToUpperInvariant(command) == 'L')
                    {
                        directionProcessor.LeftTurn(MarsRover);
                    }
                    else if (char.ToUpperInvariant(command) == 'R')
                    {
                        directionProcessor.RightTurn(MarsRover);
                    }
                }

                Console.WriteLine("Rover final position: " + MarsRover.XAxis + " " + MarsRover.YAxis + " " + MarsRover.Direction);
            }
        }

        private static void InitialRover(string roverString)
        {
            if (!string.IsNullOrEmpty(roverString))
            {
                var roverPropList = roverString.Split(null);

                if (roverPropList.Length > 2)
                {
                    MarsRover = new Rover(roverPropList[0].ToInt(), roverPropList[1].ToInt(), roverPropList[2]);
                    FixRoverPosition();
                }
            }
        }

        private static void InitialPlateau(IList<string> input)
        {
            XMax = 0;
            YMax = 0;

            if (input != null && input.Any())
            {
                var XYSizes = input.FirstOrDefault().Split(null);

                if (XYSizes.Length > 1)
                {
                    XMax = XYSizes[0].ToInt();
                    YMax = XYSizes[1].ToInt();
                }
            }
        }

        private static void FixRoverPosition()
        {
            if (MarsRover.XAxis > XMax)
            {
                MarsRover.XAxis = XMax;
            }

            if (MarsRover.YAxis > YMax)
            {
                MarsRover.YAxis = YMax;
            }
        }

        //Test Methods

        private static void TestSetMarsRover()
        {
            //test null case
            SetMarsRover(null);
            Debug.Assert(MarsRover == null);

            //test empty case
            SetMarsRover(new List<string>());
            Debug.Assert(MarsRover == null);

            //test wrong input case
            SetMarsRover(new List<string>() { "RandomText" });
            Debug.Assert(MarsRover == null);


            //test wrong format case
            SetMarsRover(new List<string>() { "Random Text" });
            Debug.Assert(MarsRover == null);


            //test normal case
            SetMarsRover(new List<string>() { "5 5", "3 3 N" });
            Debug.Assert(MarsRover.XAxis == 3);
            Debug.Assert(MarsRover.YAxis == 3);
            Debug.Assert(MarsRover.Direction.Equals("N"));

        }

        private static void TestMoveRover()
        {
            //test null case
            MarsRover = new Rover(3, 3, "N");
            MoveRover(null);

            Debug.Assert(MarsRover.XAxis == 3);
            Debug.Assert(MarsRover.YAxis == 3);
            Debug.Assert(MarsRover.Direction.Equals("N"));

            //test empty case
            MarsRover = new Rover(3, 3, "N");
            MoveRover("");

            Debug.Assert(MarsRover.XAxis == 3);
            Debug.Assert(MarsRover.YAxis == 3);
            Debug.Assert(MarsRover.Direction.Equals("N"));

            //test wrong format case
            MarsRover = new Rover(3, 3, "N");
            MoveRover("12345@!#$");

            Debug.Assert(MarsRover.XAxis == 3);
            Debug.Assert(MarsRover.YAxis == 3);
            Debug.Assert(MarsRover.Direction.Equals("N"));

            //test out of bound case
            MarsRover = new Rover(3, 3, "N");
            InitialPlateau(new List<string>() { "3 3" });

            MoveRover("LMMMMMLMMMMMLMMMMMLMMMMM");
            Debug.Assert(MarsRover.XAxis == 3);
            Debug.Assert(MarsRover.YAxis == 3);
            Debug.Assert(MarsRover.Direction.Equals("N"));

            //test normal case
            MarsRover = new Rover(1, 2, "N");
            InitialPlateau(new List<string>() { "5 5" });

            MoveRover("LMLMLMLMM");
            Debug.Assert(MarsRover.XAxis == 1);
            Debug.Assert(MarsRover.YAxis == 3);
            Debug.Assert(MarsRover.Direction.Equals("N"));

            //test normal case
            MarsRover = new Rover(3, 3, "E");
            InitialPlateau(new List<string>() { "5 5" });

            MoveRover("MMrMMrmRRM");
            Debug.Assert(MarsRover.XAxis == 5);
            Debug.Assert(MarsRover.YAxis == 1);
            Debug.Assert(MarsRover.Direction.Equals("E"));
        }

        private static void TestInitialRover()
        {
            //test null case
            MarsRover = null;
            InitialRover(null);
            Debug.Assert(MarsRover == null);

            //test empty case
            MarsRover = null;
            InitialRover("");
            Debug.Assert(MarsRover == null);

            //test wrong input case
            MarsRover = null;
            InitialRover("RandomText");
            Debug.Assert(MarsRover == null);


            //test wrong format case
            MarsRover = null;
            InitialRover("Random Text");
            Debug.Assert(MarsRover == null);


            //test out of bound case
            InitialPlateau(new List<string>() { "3 3" });
            InitialRover("5 4 N");
            Debug.Assert(MarsRover.XAxis == 3);
            Debug.Assert(MarsRover.YAxis == 3);
            Debug.Assert(MarsRover.Direction.Equals("N"));

            //test normal case
            InitialPlateau(new List<string>() { "5 5" });
            InitialRover("3 4 N");
            Debug.Assert(MarsRover.XAxis == 3);
            Debug.Assert(MarsRover.YAxis == 4);
            Debug.Assert(MarsRover.Direction.Equals("N"));
        }

        private static void TestInitialPlateau()
        {
            //test null case
            InitialPlateau(null);
            Debug.Assert(XMax == 0);
            Debug.Assert(YMax == 0);

            //test empty case
            InitialPlateau(new List<string>());
            Debug.Assert(XMax == 0);
            Debug.Assert(YMax == 0);

            //test wrong input case
            InitialPlateau(new List<string>() { "RandomText" });
            Debug.Assert(XMax == 0);
            Debug.Assert(YMax == 0);

            //test wrong format case
            InitialPlateau(new List<string>() { "Random Text" });
            Debug.Assert(XMax == 0);
            Debug.Assert(YMax == 0);

            //test normal case
            InitialPlateau(new List<string>() { "5 5" });
            Debug.Assert(XMax == 5);
            Debug.Assert(YMax == 5);

        }

    }
}
