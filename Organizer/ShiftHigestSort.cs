using System;
using System.Collections.Generic;

namespace Organizer
{
	public class ShiftHighestSort
    {
        private List<int> list = new List<int>();

        /// <summary>
        /// Sort an array using the functions below
        /// </summary>
        /// <param name="unsortedList">The unsorted array</param>
        /// <returns>The sorted array</returns>
        public List<int> Sort(List<int> unsortedList)
        {
            list = new List<int>(unsortedList);

            SortFunction(0, list.Count - 1);
            return list;
        }

        /// <summary>
        /// Sort the array from low to high
        /// </summary>
        /// <param name="low">The index within this.list to start with</param>
        /// <param name="high">The index within this.list to end with</param>
        private void SortFunction(int low, int high)
        {
            for (int h = high; h > low; h--)
            {
                for (int i = low; i < h; i++)
                {
                    if (list[i + 1] < list[i])
                    {
                        //als dit true is, staan ze in de verkeerde volgorde
                        int temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
                    }
                }
            }
        }
    }
}
