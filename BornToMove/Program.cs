
using System.Collections.Generic;
using System.Data;
using BornToMove.Business;
using Microsoft.Data.SqlClient;

// Ik moet in deze klasse de MOveContext gaan instantiëren
// new MoveContext!
// Die instantie gaat dus helemaal doot het po

namespace BornToMove
{
    public class Program
    {
        private static List<int> ids = new List<int>();
        private static List<string> moveNames = new List<string>();

        private static void Main(string[] args)
        {
            BuMove buMove = new BuMove();

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
                Console.WriteLine("Choose a number from the list or type '0' if you want to add a new move.");
                buMove.GiveListOfMoves();

                int listChoice = int.Parse(Console.ReadLine());

                if (listChoice != 0)
                {
                    buMove.GiveMoveBasedOnId(listChoice);
                }
                else if (listChoice == 0)
                {
                    Console.WriteLine("What is the name of the new move?");
                    string nameNewMove = Console.ReadLine();

                    //als deze niet al bestaat:

                    if (moveNames.Contains(nameNewMove))
                    {
                        Console.WriteLine($"{nameNewMove} is already on the list.");
                    }
                    else
                    {
                        Console.WriteLine("What is the description of the new move?");
                        string descriptionNewMove = Console.ReadLine();

                        Console.WriteLine("What is the sweatrate of the new move?");
                        int sweatRateNewMove = int.Parse(Console.ReadLine());

                        WriteNewMove(nameNewMove, descriptionNewMove, sweatRateNewMove);

                    }

                }


                string ratingMove = Console.ReadLine();
                Console.WriteLine("On a scale of 1 to 5, how would you rate the intensity of this move?");
                string ratingIntensity = Console.ReadLine();
            }
        }



        private static void WriteNewMove(string name, string description, int sweatRate)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = BornToMove;Integrated Security=True;";
            string sqlQuery = $"INSERT INTO dbo.move (name, description, sweatRate) VALUES ('{name}', '{description}', {sweatRate})";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Nieuwe move succesvol toegevoegd. Hoeveelheid rijen toegevoegd: {rowsAffected}");
                        }
                        else
                        {
                            Console.WriteLine("Er zijn geen rijen toegevoegd. Er is iets mis gegaan");
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"Exception: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception: {e.Message}");
                }
            }

        }



    }
     

}


