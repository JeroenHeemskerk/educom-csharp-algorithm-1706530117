﻿using Microsoft.Data.SqlClient;
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

        public List<(Move move, float avg)>? ReadAllMoves()
        {
            try
            {
                var moves = MoveContext.Move
                    .Include(m => m.Ratings)
                    .ToList();

                List<(Move, float)> movesWithAvgRating = moves
                    .Select(move => (move, getAverageRating(move)))
                    .ToList();

                return movesWithAvgRating;
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
