using System;
using System.Collections.Generic;
using System.Linq;
using MarsRover.Data;

namespace MarsRover.Helper
{
    public class RoverDirectionProcessor
    {
        private List<Rover> RoverDirectionList { get; set; }

        public RoverDirectionProcessor()
        {
            RoverDirectionList = new List<Rover>()
            {
                new Rover(0, 1, "N"),
                new Rover(1, 0, "E"),
                new Rover(0, -1, "S"),
                new Rover(-1, 0, "W")
            };
        }

        internal void MoveForward(Rover marsRover)
        {
            var correspondingDirection = RoverDirectionList
                .FirstOrDefault(r => string.Equals(r.Direction, 
                                                        marsRover.Direction, 
                                                        StringComparison.InvariantCultureIgnoreCase));

            if (correspondingDirection != null)
            {
                marsRover.MoveAxis(correspondingDirection.XAxis, correspondingDirection.YAxis);
            }
        }

        internal void LeftTurn(Rover marsRover)
        {
            var index = RoverDirectionList
                .FindIndex(r => string.Equals(r.Direction, 
                                                marsRover.Direction, 
                                                StringComparison.InvariantCultureIgnoreCase)) - 1;

            marsRover.Direction = RoverDirectionList
                .ElementAt((index + RoverDirectionList.Count) % RoverDirectionList.Count).Direction;
        }

        internal void RightTurn(Rover marsRover)
        {
            var index = RoverDirectionList
                .FindIndex(r => string.Equals(r.Direction, 
                                                marsRover.Direction, 
                                                StringComparison.InvariantCultureIgnoreCase)) + 1;

            marsRover.Direction = RoverDirectionList.ElementAt(index % RoverDirectionList.Count).Direction;
        }
    }
}
