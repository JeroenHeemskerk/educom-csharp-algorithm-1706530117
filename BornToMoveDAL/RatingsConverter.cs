using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.DAL
{
    public class RatingsConverter : IComparer<MoveRating>
    {
        public int Compare(MoveRating x, MoveRating y)
        {
            return y.Rating.CompareTo(x.Rating);
            //returns a value that indicates whether the value is less than, equal to
            //or greater than the value it's being compared to. 
        }
    }
}
