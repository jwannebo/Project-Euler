﻿using System;
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
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine(Problem12());
            stopwatch.Stop();
            Console.WriteLine("Problem solved in {0}", stopwatch.Elapsed);
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

        static int Problem2()
            //Each new term in the Fibonacci sequence is generated by adding the previous two terms.
            //By starting with 1 and 2, the first 10 terms will be:
            //    1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...
            //By considering the terms in the Fibonacci sequence whose values do not exceed four million,
            //find the sum of the even-valued terms.
        {
            int runningSum = 0;
            for (int last = 0, current = 1; current < 4000000; current = last + (last = current))
            {
                //Fibonacci for loop =D
                if(current % 2 == 0) { runningSum += current; };
            }
            return runningSum;
        }

        static long Problem3()
            //The prime factors of 13195 are 5, 7, 13 and 29.
            //What is the largest prime factor of the number 600851475143 ?
        {
            long sqrt = (long)Math.Sqrt(600851475143); // A prime fator of X can never be greater than sqrt(X)
            long remainder = 600851475143;
            long leastPrimeFactor = 0;
            for (long i = 2; i < sqrt; i++)
            {
                while (remainder % i == 0)
                {
                    remainder /= i;
                }
                if (remainder == 1)
                {
                    leastPrimeFactor = i;
                    break;
                }
            }
            return leastPrimeFactor;
        }

        static int Problem4()
        //A palindromic number reads the same both ways.
        //The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 × 99.
        //Find the largest palindrome made from the product of two 3-digit numbers.
        {
            SortedSet<int> palindromes = new SortedSet<int>();
            for ( int i = 100; i < 1000; i++)
                for (int j = 100; j <1000; j++)
                {
                    int product = i * j;
                    string prodStr = product.ToString();
                    bool isPalindrome = true;

                    for (int k = 0; k < prodStr.Count(); k++)
                    {
                        if (prodStr[k] != prodStr[prodStr.Count() -k -1]) isPalindrome = false;
                    }
                    if (isPalindrome) palindromes.Add(product);
                }
            return palindromes.Last();
        }

        static int Problem5()
        //2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
        //What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
        {
            int i = 20;
            while (true)
            {
                bool isAnswer = true;
                for (int j = 2; j < 21; j++)
                {
                    if (i % j != 0)
                    {
                        isAnswer = false;
                        break;
                    }
                }
                if (isAnswer) return i;
                i++;
            }
        }

        static int Problem6()
        //The sum of the squares of the first ten natural numbers is,
        //    1^2 + 2^2 + ... + 10^2 = 385
        //The square of the sum of the first ten natural numbers is,
        //    (1 + 2 + ... + 10)^2 = 552 = 3025
        //Hence the difference between the sum of the squares of the first ten
        //natural numbers and the square of the sum is 3025 − 385 = 2640.
        //Find the difference between the sum of the squares of the first one
        //hundred natural numbers and the square of the sum.
        {
            var list = new List<int>(Enumerable.Range(1,100));
            var squaredList = list.Select(x => x * x);
            return (list.Sum() * list.Sum()) - squaredList.Sum();
        }

        static long Problem7()
        //By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
        //What is the 10 001st prime number?
        {
            List<long> primes = new List<long>();
            int i = 2;
            while (primes.Count() != 10001)
            {
                bool isPrime = true;
                foreach (long prime in primes)
                {
                    if (i % prime == 0) isPrime = false;
                }
                if (isPrime) primes.Add(i);
                i++;
            }
            return primes.Last();
        }

        static long Problem8()
        // The four adjacent digits in the 1000-digit number that have the greatest product are 9 × 9 × 8 × 9 = 5832.
        // Find the thirteen adjacent digits in the 1000-digit number that have the greatest product. What is the value of this product?
        {
            string thousandDrigitNumber = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";
            long maxProduct = 0;
            for (int i = 0; i + 12 < thousandDrigitNumber.Length; i++)
            {
                long product = 1;
                for (int j = 0; j < 13; j++)
                {
                    //Why C# why
                    product *= (int) char.GetNumericValue(thousandDrigitNumber[i + j]);
                }
                if (maxProduct < product) maxProduct = product;
            }
            return maxProduct;
        }

        static int Problem9()
        //There exists exactly one Pythagorean triplet for which a + b + c = 1000.
        //Find the product abc, were a<b<c.
        {
            for (int a = 1; a < 999; a++)
            {
                for (int b = a + 1; b < 1000; b++)
                {
                    int c = 1000 - (a + b);
                    if ((a * a + b * b) == (c * c)) return a*b*c; 
                }
            }
            return -1;
        }

        static long Problem10()
        //Find the sum of all the primes below two million.
        {
            List<long> primes = new List<long>();
            for (int i = 3; i < 2000000; i += 2) // Skipping evens for speed
            {
                bool isPrime = true;
                foreach (long prime in primes)
                {
                    if (i % prime == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime) primes.Add(i);
            }
            return primes.Sum() + 2; // Account for 2 not being in primes
        }

        static int Problem11()
        //What is the greatest product of four adjacent numbers in the same direction (up, down, left, right, or diagonally) in the 20×20 grid?
        {
            string gridStr = 
@"08 02 22 97 38 15 00 40 00 75 04 05 07 78 52 12 50 77 91 08
49 49 99 40 17 81 18 57 60 87 17 40 98 43 69 48 04 56 62 00
81 49 31 73 55 79 14 29 93 71 40 67 53 88 30 03 49 13 36 65
52 70 95 23 04 60 11 42 69 24 68 56 01 32 56 71 37 02 36 91
22 31 16 71 51 67 63 89 41 92 36 54 22 40 40 28 66 33 13 80
24 47 32 60 99 03 45 02 44 75 33 53 78 36 84 20 35 17 12 50
32 98 81 28 64 23 67 10 26 38 40 67 59 54 70 66 18 38 64 70
67 26 20 68 02 62 12 20 95 63 94 39 63 08 40 91 66 49 94 21
24 55 58 05 66 73 99 26 97 17 78 78 96 83 14 88 34 89 63 72
21 36 23 09 75 00 76 44 20 45 35 14 00 61 33 97 34 31 33 95
78 17 53 28 22 75 31 67 15 94 03 80 04 62 16 14 09 53 56 92
16 39 05 42 96 35 31 47 55 58 88 24 00 17 54 24 36 29 85 57
86 56 00 48 35 71 89 07 05 44 44 37 44 60 21 58 51 54 17 58
19 80 81 68 05 94 47 69 28 73 92 13 86 52 17 77 04 89 55 40
04 52 08 83 97 35 99 16 07 97 57 32 16 26 26 79 33 27 98 66
88 36 68 87 57 62 20 72 03 46 33 67 46 55 12 32 63 93 53 69
04 42 16 73 38 25 39 11 24 94 72 18 08 46 29 32 40 62 76 36
20 69 36 41 72 30 23 88 34 62 99 69 82 67 59 85 74 04 36 16
20 73 35 29 78 31 90 01 74 31 49 71 48 86 81 16 23 57 05 54
01 70 54 71 83 51 54 69 16 92 33 48 61 43 52 01 89 19 67 48";

            int rows = 20, cols = 20;

            int[][] grid = new int[rows][];
            int iter = 0;

            foreach (string row in gridStr.Split(Environment.NewLine[0]))
            {
                grid[iter++] = Array.ConvertAll(row.Split(' '), int.Parse);
            }

            int maxProduct = -1;

            // Rows
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols - 3; j++)
                {
                    int product = grid[i][j] * grid[i][j + 1] * grid[i][j + 2] * grid[i][j + 3];
                    if (product > maxProduct) maxProduct = product;
                }
            }

            // Cols
            for (int i = 0; i < rows - 3; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int product = grid[i][j] * grid[i + 1][j] * grid[i + 2][j] * grid[i + 3][j];
                    if (product > maxProduct) maxProduct = product;
                }
            }

            // NW/SE Diagonals
            for (int i = 0; i < rows - 3; i++)
            {
                for (int j = 0; j < cols-3; j++)
                {
                    int product = grid[i][j] * grid[i + 1][j +1] * grid[i + 2][j +2] * grid[i + 3][j +3];
                    if (product > maxProduct) maxProduct = product;
                }
            }

            // NE/SW Diagonals
            for (int i = 3; i < rows; i++)
            {
                for (int j = 0; j < cols - 3; j++)
                {
                    int product = grid[i][j] * grid[i - 1][j + 1] * grid[i - 2][j + 2] * grid[i - 3][j + 3];
                    if (product > maxProduct) maxProduct = product;
                }
            }

            return maxProduct;
        }

        static long Problem12()
        // What is the value of the first triangle number to have over five hundred divisors? (including 1 and itself)
        {
            Func<long, int> NumDividers = x =>
              {
                  int dividers = 0;
                  for (int i = 1; i <= x; i++)
                  {
                      if (x % i == 0) dividers++;
                  }
                  return dividers;
              };

            int j = 2;
            long triangle = 1;
            while (NumDividers(triangle) < 501)
            {
                triangle += j++;
            }
            return triangle;
        }
    }
}
