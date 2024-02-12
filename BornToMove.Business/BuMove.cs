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
        private MoveCrud moveCrud;

        public BuMove()
        {
            this.moveCrud = new MoveCrud();
        }

        public void GenerateRandomMove()
        {
            //Haal alle id's op en stop ze in een lijst
            var listOfIds = this.moveCrud.GetAllIds();
            //Selecteer random index in die lijst en pak corresponderende waarde(id)
            Random random = new Random();
            int randomIndex = random.Next(0, listOfIds.Count);
            var randomId = listOfIds[randomIndex];
            //Haal move behorend bij random id op
            var randomMove = this.moveCrud.ReadMoveById(randomId);

            Console.WriteLine(randomMove.Id + ". Name: " + randomMove.Name + ", Description: " + randomMove.Description + ", SweatRate: " + randomMove.SweatRate + ".");
        }

        public void GiveListOfMoves()
        {
            List<Move> moves = this.moveCrud.ReadAllMoves();

            foreach (Move move in moves)
            {
                Console.WriteLine(move.Id + ". " + move.Name);
            }
            
        }

        public void GiveMoveBasedOnId(int id)
        {
            var chosenMove = this.moveCrud.ReadMoveById(id);
            Console.WriteLine(chosenMove.Id + ". Name: " + chosenMove.Name + ", Description: " + chosenMove.Description + ", Sweatrate: " + chosenMove.SweatRate + ".");

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

        public void modifyMove()
        {
            Console.WriteLine("What is the id of the move you want to modify?");
            var idMoveToModify = int.Parse(Console.ReadLine());

            var moveToModify = this.moveCrud.ReadMoveById(idMoveToModify);

            Console.WriteLine("What is the new name of this move?");
            moveToModify.Name = Console.ReadLine();
            Console.WriteLine("What is the new description of this move?");
            moveToModify.Description = Console.ReadLine();
            Console.WriteLine("What is the new sweatrate of this move?");
            moveToModify.SweatRate = int.Parse(Console.ReadLine());

            this.moveCrud.UpdateMove(moveToModify);

            Console.WriteLine("The move has been modified.");
        }


        public void deleteMove(int idOfMoveToBeDeleted) {

            this.moveCrud.DeleteMove(idOfMoveToBeDeleted);
            Console.WriteLine($"'The move with id: {idOfMoveToBeDeleted} has been deleted'"); }
    }
}
