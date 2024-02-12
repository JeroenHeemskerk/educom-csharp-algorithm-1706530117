
using System.Collections.Generic;
using System.Data;
using BornToMove.Business;
using BornToMove.DAL;
using Microsoft.Data.SqlClient;



namespace BornToMove
{
    public class Program
    {
        private static List<string> moveNames = new List<string>();

        private static void Main(string[] args)
        {
            BuMove buMove = new BuMove();
            MoveContext moveContext = new MoveContext();

            moveContext.CheckAndAddMovesIfTableEmpty();


            Console.WriteLine("You should get moving!");
            Console.WriteLine("Do you want a suggestion (type: 'suggestion') or do you want to choose a move from the list (type: 'list')?");
            string userChoice = Console.ReadLine();
            Console.WriteLine($"You chose: {userChoice}");

            if (userChoice != "suggestion" && userChoice != "list")
            {
                Console.WriteLine("That is not a valid option. Please start over and choose 'suggestion' or 'list.'");
            }
            if (userChoice == "suggestion")
            {
                buMove.GenerateRandomMove();

                Console.WriteLine("On a scale of 1 to 5, how would you rate this move?");
                string ratingMove = Console.ReadLine();
                Console.WriteLine("On a scale of 1 to 5, how would you rate the intensity of this move?");
                string ratingIntensity = Console.ReadLine();
            }
            else if (userChoice == "list")
            {
                Console.WriteLine("Choose a number from the list or type 'new' if you want to add a new move. " +
                    "If instead you would like to delete or modify a move, type 'delete' or 'modify'.");
                buMove.GiveListOfMoves();
                var listChoice = Console.ReadLine();

                if (listChoice != "new" && listChoice != "delete" && listChoice != "modify")
                {
                    int listChoiceInt = int.Parse(listChoice);
                    buMove.GiveMoveBasedOnId(listChoiceInt);

                    Console.WriteLine("On a scale of 1 to 5, how would you rate this move?");
                    string ratingMove = Console.ReadLine();
                    Console.WriteLine("On a scale of 1 to 5, how would you rate the intensity of this move?");
                    string ratingIntensity = Console.ReadLine();
                }
                else if (listChoice == "new")
                {
                    buMove.SaveMoveIfNotExists();
                } else if (listChoice == "delete")
                {
                    Console.WriteLine("Write down the id of the move that you want to delete.");
                    var idOfMoveToBeDeleted = int.Parse(Console.ReadLine());
                    buMove.deleteMove(idOfMoveToBeDeleted);
                } else if (listChoice == "modify")
                {
                    buMove.modifyMove();
                }

            }
        }


    }
     

}


