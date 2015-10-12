using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Euler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Problem1().ToString());
            Console.ReadKey();
        }

        static int Problem1()
            //If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9.
            //The sum of these multiples is 23. Find the sum of all the multiples of 3 or 5 below 1000.
        {
            HashSet<int> threes = new HashSet<int>();
            HashSet<int> fives = new HashSet<int>();
            for (int i=0; i<1000; i++)
            {
                if ((i % 3) == 0) threes.Add(i);
                if ((i % 5) == 0) fives.Add(i);
            }
            return threes.Union<int>(fives).Sum();
        }
    }
}
