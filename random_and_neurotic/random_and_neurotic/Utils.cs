using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace random_and_neurotic
{
    class Utils
    {
        static Random random = new Random();

        public static float rand()
        {
            return (float)random.Next() / (float)int.MaxValue;
        }
    }
}
