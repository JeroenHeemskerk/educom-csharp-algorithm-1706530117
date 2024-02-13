using System;
using System.Collections.Generic;

namespace Organizer
{
	public class RotateSort<T>
    {
        private List<T> list;
        private IComparer<T> comparer;

    
        
        public List<T> Sort<T>(List<T> input, IComparer<T> comparer)
        {
            this.list = input;
            this.comparer = comparer;

            SortFunction(0, list.Count - 1);
            return this.list;
        }

      
        private void SortFunction(int low, int high)
        {
           //the quicksort function that uses recursion!
            if (low < high)
            {
                int pivotIndex = Partitioning(low, high);

                SortFunction(low, pivotIndex - 1);
                SortFunction(pivotIndex + 1, high);
            }
        }

        private int Partitioning(int low, int high)
        {
            //Produces the pivot index and moves the items that are less than or equal to
            // the pivot to one side, and the items that are greater than to the other side. 

            int randomIndex = new Random().Next(low, high + 1);
            Swap(randomIndex, high);

            T pivot = list[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (comparer.Compare(list[j], pivot) < 0)
                {
                    i++;
                    Swap(i, j);
                }
            }

            Swap(i + 1, high);

            return i + 1;

            //this should return the pivot index (where do we split the array)
        }

        private void Swap(int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
