using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BornToMove.DAL;
using Microsoft.EntityFrameworkCore;


namespace BornToMove.Business
{
    public class BuMove
    {
        public MoveCrud moveCrud;
        private double ratingMove;
        private double ratingIntensity;
        private Move randomMove;
        private Move moveToBeRated;

        public BuMove()
        {
            this.moveCrud = new MoveCrud();
        }

        public MoveRating GenerateRandomMove()
        {
            return moveCrud.ReadRandomMove();
        }

        public List<MoveRating> GetAllMoves()
        {
            return this.moveCrud.ReadAllMoves();
        }

        public MoveRating GiveMoveBasedOnId(int id)
        {
             return this.moveCrud.ReadMoveRatingById(id);
       
        }

        public void SaveMoveIfNotExists()
        {
            Console.WriteLine("What is the name of the new move?");
            var nameMove = Console.ReadLine();

            var moveExists = this.moveCrud.DoesMoveExist(nameMove);

            if (moveExists)
            {
                Console.WriteLine("This move already exists. Please start over and pick a different name.");
            } else if (!moveExists)
            {
                Console.WriteLine("What is the description of the new move?");
                var descriptionNewMove = Console.ReadLine();

                Console.WriteLine("What is the sweatrate of the new move?");
                int sweatRateNewMove = int.Parse(Console.ReadLine());

                Move newMove = new Move(nameMove, descriptionNewMove, sweatRateNewMove);
                this.moveCrud.CreateMove(newMove);

                Console.WriteLine("You have successfully added this move!");
            }
        }


        public void ModifyMove(Move move)
        {
            this.moveCrud.UpdateMove(move);
        }



        public void deleteMove(int idOfMoveToBeDeleted) {

            this.moveCrud.DeleteMove(idOfMoveToBeDeleted);
        }



        public void getRatingFromUser()
        {
            Console.WriteLine("On a scale of 1 to 5, how would you rate this move?");
            this.ratingMove = double.Parse(Console.ReadLine());
            Console.WriteLine("On a scale of 1 to 5, how would you rate the intensity of this move?");
            this.ratingIntensity = double.Parse(Console.ReadLine());

        }

        public void addUserRatingMove()
        {
            this.moveCrud.addRating(this.moveToBeRated, this.ratingIntensity, this.ratingMove);
        }
    }
}
