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
            Console.WriteLine(Problem10());
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
    }
}
