using System;
using System.Collections.Generic;

namespace Organizer
{
	public class RotateSort
	{

        private List<int> list = new List<int>();

        /// <summary>
        /// Sort an array using the functions below
        /// </summary>
        /// <param name="input">The unsorted array</param>
        /// <returns>The sorted array</returns>
        public List<int> Sort(List<int> input)
        {
            list = new List<int>(input);

            SortFunction(0, list.Count - 1);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="low">De index within this.array to start with</param>
        /// <param name="high">De index within this.array to stop with</param>
        private void SortFunction(int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partitioning(low, high);

                SortFunction(low, pivotIndex - 1);
                SortFunction(pivotIndex + 1, high);
            }
        }

        /// 
        /// Partition the array in a group 'low' digits (e.a. lower than a chosen pivot) and a group 'high' digits
        /// </summary>
        /// <param name="low">De index within this.array to start with</param>
        /// <param name="high">De index within this.array to stop with</param>
        /// <returns>The index in the array of the first of the 'high' digits</returns>
        private int Partitioning(int low, int high)
        {
            int randomIndex = new Random().Next(low, high + 1);
            Swap(randomIndex, high);

            int pivot = list[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (list[j] < pivot)
                {
                    i++;
                    Swap(i, j);
                }
            }

            Swap(i + 1, high);

            return i + 1;
        }

        private void Swap(int i, int j)
        {
            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
