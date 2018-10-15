using System;

namespace MarsRover.Helper
{
    public static class StringExtension
    {
        public static int ToInt(this string s)
        {
            int intValue = 0;

            try
            {
                intValue = int.Parse(s);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }

            return intValue;
        }
    }
}
