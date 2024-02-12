using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BornToMove;
using Microsoft.EntityFrameworkCore;


namespace BornToMove.DAL
{
    public class MoveCrud
    {
        private MoveContext MoveContext;


        public MoveCrud()
        {
            this.MoveContext = new MoveContext();
        }

        public void CreateMove(Move move) {
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

        public List<Move>? ReadAllMoves()
        {
            try
            {
                var moves = MoveContext.Move
                    .Include(m => m.Ratings)
                    .ToList();

                return moves;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong, exception: {e.Message}");
                return null;
            }
        }

        public Move? ReadMoveById(int id)
        {
            try
            {
                var selectedMove = MoveContext.Move
                    .Include(m => m.Ratings)
                    .FirstOrDefault(move => move.Id == id);

                return selectedMove;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong, exception: {e.Message}");
                return null;
            }
        }

        public void UpdateMove(Move move)
        {
            try
            {
                MoveContext.Move.Update(move);
                MoveContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong, exception: {e.Message}");
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


        public double getAverageRating(Move move)
        {
            try
            {
                if (move != null)
                {  
                    double averageRating = move.Ratings.Select(r => r.Rating).DefaultIfEmpty(0).Average();
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
