using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Organizer
{
    public class Program
    {
        public static void Main(string[] _)
        {

            Console.WriteLine("How many elements should the list have?");
            string amountOfListElementsString = Console.ReadLine();

            if (int.TryParse(amountOfListElementsString, out int amountOfListElementsInt))
            {
                // Parsing successful:
                Console.WriteLine($"You entered: {amountOfListElementsInt} elements");

                List<int> randomNumbers = GenerateRandomIntegers(amountOfListElementsInt, -99, 99);

                bool randomNumbersAreSorted = IsSorted(randomNumbers);
                Console.Write("Random Numbers are sorted: ");
                Console.WriteLine(randomNumbersAreSorted);

                ShiftHighestSort shiftHighestSort = new ShiftHighestSort();
                RotateSort rotateSort = new RotateSort();

                Stopwatch stopwatch = new Stopwatch();

                //

                stopwatch.Start();

                List<int> shiftSortedList = shiftHighestSort.Sort(randomNumbers);

                stopwatch.Stop();

                bool shiftSortedListIsSorted = IsSorted(shiftSortedList);

                long elapsedShiftSort = stopwatch.ElapsedMilliseconds;

                Console.Write("Shiftsorted Numbers are sorted: " + shiftSortedListIsSorted + ". Duration: ");
                Console.WriteLine(elapsedShiftSort + " ms.");

                int maxNumbersToPrint = 5;
                int count = 0;

                Console.WriteLine("Random Numbers:");
                foreach (int number in shiftSortedList)
                {
                    if (count >= maxNumbersToPrint)
                    {
                        break;
                    }

                    Console.WriteLine(number);
                    count++;
                }

                //

                stopwatch.Restart();

                List<int> rotateSortedList = rotateSort.Sort(randomNumbers);

                stopwatch.Stop();

                long elapsedRotateSort = stopwatch.ElapsedMilliseconds;

                bool rotateSortedListIsSorted = IsSorted(rotateSortedList);

                Console.Write("Rotatesorted Numbers is sorted: " + rotateSortedListIsSorted + ". Duration: ");
                Console.WriteLine(elapsedRotateSort + " ms.");

                maxNumbersToPrint = 5;
                count = 0;

                Console.WriteLine("Random Numbers:");
                foreach (int number in rotateSortedList)
                {
                    if (count >= maxNumbersToPrint)
                    {
                        break;
                    }

                    Console.WriteLine(number);
                    count++;
                }
            }
            else
            {
                // Parsing failed
                Console.WriteLine("Invalid format for integer. Sorting not excecuted.");
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

        private static bool IsSorted(List<int> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i] > list[i + 1]) {
                    return false;
                }
            }
            return true;
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
