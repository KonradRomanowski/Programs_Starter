using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.Models.Helpers
{
    public static class Int32Extensions
    {
        public static bool IsGreaterThan(this int i, int value)
        {
            return i > value;
        }

        public static bool IsEven(this int i)
        {
            if (i % 2 == 0)
                return true;
            return false;
        }
    }
}
