using System;
using System.Collections.Generic;

namespace Organizer
{
    public class Program
    {
        public static void Main(string[] _)
        {
            List<int> randomNumbers = GenerateRandomIntegers(10, -99, 99);


            // Prints (10) generated random numbers, unsorted
            Console.WriteLine("Random Numbers:");
            foreach (int number in randomNumbers)
            {
                Console.WriteLine(number);
            }


            ShiftHighestSort shiftHighestSort = new ShiftHighestSort();
            List<int> sortedList = shiftHighestSort.Sort(randomNumbers);

            // Prints (10) generated random numbers, sorted
            Console.WriteLine("Sorted Random Numbers:");
            foreach (int number in sortedList)
            {
                Console.WriteLine(number);
            }

        }

        static List<int> GenerateRandomIntegers(int N, int minValue, int maxValue)
        {
            Random random = new Random();
            List<int> result = new List<int>();

            for (int i = 0; i < N; i++)
            {
                int randomNumber = random.Next(minValue, maxValue + 1);
                result.Add(randomNumber);
            }

            return result;
        }

        /* Example of a static function */

        /// <summary>
        /// Show the list in lines of 20 numbers each
        /// </summary>
        /// <param name="label">The label for this list</param>
        /// <param name="theList">The list to show</param>
        public static void ShowList(string label, List<int> theList)
        {
            int count = theList.Count;
            if (count > 100)
            {
                count = 300; // Do not show more than 300 numbers
            }
            Console.WriteLine();
            Console.Write(label);
            Console.Write(':');
            for (int index = 0; index < count; index++)
            {
                if (index % 20 == 0) // when index can be divided by 20 exactly, start a new line
                {
                    Console.WriteLine();
                }
                Console.Write(string.Format("{0,3}, ", theList[index]));  // Show each number right aligned within 3 characters, with a comma and a space
            }
            Console.WriteLine();
        }
    }
}
