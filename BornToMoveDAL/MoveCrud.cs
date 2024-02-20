using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BornToMove;
using Microsoft.EntityFrameworkCore;
using Organizer;
using System.Collections;


namespace BornToMove.DAL
{
    public class MoveCrud
    {
        private MoveContext MoveContext;


        public MoveCrud()
        {
            this.MoveContext = new MoveContext();
        }

        public void CreateMove(Move move)
        {
            try
            {
                MoveContext.Move.Add(move);
                MoveContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong, exception: {e.Message}");
            }
        }

        public List<MoveRating>? ReadAllRatings()
        {
            try
            {
                var moves = MoveContext.MoveRating
                    .Include(m => m.Move)
                    .ToList();

                RatingsConverter converter = new RatingsConverter();
                RotateSort<MoveRating> rotateSort = new RotateSort<MoveRating>();

                var movesSorted = rotateSort.Sort(moves, converter);

                return movesSorted;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong, exception: {e.Message}");
                return null;
            }
        }

        //public List<(Move move, float avg)>? ReadAllMoves()
        //{
        //    try
        //    {
        //        var moves = MoveContext.Move
        //            .Include(m => m.Ratings)
        //            .ToList();

        //        List<(Move, float)> movesWithAvgRating = moves
        //            .Select(move => (move, getAverageRating(move)))
        //            .ToList();

        //        return movesWithAvgRating;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine($"Something went wrong, exception: {e.Message}");
        //        return null;
        //    }
        //}



        public List<MoveRating> ReadAllMoves()
        {
            List<MoveRating> allMoves = MoveContext.Move
                .Include(m => m.Ratings)
                .Select(move => new MoveRating()
                {
                    Move = move,
                    Rating = move.Ratings != null && move.Ratings.Any() ? move.Ratings.Average(r => r.Rating) : 0,
                    Vote = move.Ratings != null && move.Ratings.Any() ? move.Ratings.Average(r => r.Vote) : 0
                })
                .ToList();
            allMoves.Sort(new RatingsConverter());
            return allMoves;
        }

        public MoveRating ReadRandomMove()
        {
            Move randomMove = MoveContext.Move
                .Include(m => m.Ratings)
                .OrderBy(m => Guid.NewGuid())
                .FirstOrDefault();
            if (randomMove != null)
            {
                MoveRating moveRating = new MoveRating()
                {
                    Move = randomMove,
                    Rating = randomMove.Ratings != null && randomMove.Ratings.Any() ? randomMove.Ratings.Average(r => r.Rating) : 0,
                    Vote = randomMove.Ratings != null && randomMove.Ratings.Any() ? randomMove.Ratings.Average(r => r.Vote) : 0
                };
                return moveRating;
            }
            return null;
        }

        public MoveRating ReadMoveById(int id)
        {
            MoveRating moveById = MoveContext.Move
                .Include(m => m.Ratings)
                .Where(m => m.Id == id)
                .Select(move => new MoveRating()
                {
                    Move = move,
                    Rating = move.Ratings != null && move.Ratings.Any() ? move.Ratings.Average(r => r.Rating) : 0,
                    Vote = move.Ratings != null && move.Ratings.Any() ? move.Ratings.Average(r => r.Vote) : 0
                })
                .FirstOrDefault();
            return moveById;
        }


        public void UpdateMove(Move move)
        {
            Move? moveToUpdate = MoveContext.Move?.FirstOrDefault(m => m.Id == move.Id);
            if (moveToUpdate != null)
            {
                moveToUpdate.Name = move.Name;
                moveToUpdate.Description = move.Description;

                MoveContext.SaveChanges();
            }
        }

        public void DeleteMove(int id)
        {
            try
            {
                var selectedMove = MoveContext.Move
                .Where(move => move.Id == id)
                .FirstOrDefault();

                if (selectedMove != null)
                {
                    MoveContext.Move.Remove(selectedMove);
                    MoveContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong, exception: {e.Message}");
            }
        }


        public List<int> GetAllIds()
        {
            List<int> listOfIds = MoveContext.Move.Select(m => m.Id).ToList();
            return listOfIds;
        }

        public bool DoesMoveExist(string moveName)
        {
            bool moveExists = MoveContext.Move.Any(m => m.Name == moveName);
            return moveExists;
        }


        public void addRating(Move move, double ratingIntensity, double ratingMove)
        {
            try
            {
                var moveRating = new MoveRating { Rating = ratingIntensity, Vote = ratingMove };
                move.Ratings.Add(moveRating);
                MoveContext.SaveChanges();
                Console.WriteLine("Successfully added your ratings to the database.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong, exception: {e.Message}");
            }

        }


        public float getAverageRating(Move move)
        {
            try
            {
                if (move != null)
                {
                    float averageRating = (float)move.Ratings.Select(r => r.Rating).DefaultIfEmpty(0).Average();
                    
                    return averageRating;
                }
                else
                {
                    Console.WriteLine("Error: Move object is null.");
                    return 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error calculating average rating: {e.Message}");
                return 0;
            }

        }

    }
}
