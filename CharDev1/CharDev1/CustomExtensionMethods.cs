using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CharDev1
{
    static public class CustomExtensionMethods
    {
        public static float Truncate(this float value, int digits)
        {
            double mult = Math.Pow(10.0, digits);
            double result = Math.Truncate(mult * value) / mult;
            return (float)result;
        }

        public static float Truncate(this float value)
        {            
            return (float)Math.Truncate(value);
            
        }
        public static int ToInt(this float me)
        {
            return Convert.ToInt32(me);
        }

    }
}
