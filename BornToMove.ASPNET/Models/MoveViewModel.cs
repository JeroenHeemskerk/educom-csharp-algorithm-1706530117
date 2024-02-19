using BornToMove.DAL;

namespace BornToMove.ASPNET.Models
{
    public class MoveViewModel
    {
        public required List<(Move move, float avg)> MoveWithAverageRatingTuples { get; set; }
    }
}
