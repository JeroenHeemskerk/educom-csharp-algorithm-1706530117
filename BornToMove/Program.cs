
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;


namespace BornToMove
{
    public class Program
    {
        private static List<int> ids = new List<int>();
        private static int randomId;
        private List<int> moves = new List<int>();


        private static void Main(string[] args)
        {
            Console.WriteLine("You should get moving!");
            Console.WriteLine("Do you want a suggestion (type: 'suggestion') or do you want to choose a move from the list (type: 'list')?");
            string userChoice = Console.ReadLine();
            Console.WriteLine($"You chose: {userChoice}");

            if (userChoice == "suggestion")
            {
                GetMoveIds();
                GetRandomId();
                Console.WriteLine($"Id: {randomId}");
                
                // de lijst met id's wordt opgehaald. Er wordt er random 1 gekozen.
                // De move met gekozen id, wordt opgehaald en de data wordt getoond.
                // Na afloop wordt er om een beoordeling en intensiteit gevraagd (1-5) 

                Console.WriteLine("On a scale of 1 to 5, how would you rate this move?");
                string ratingMove = Console.ReadLine();
                Console.WriteLine("On a scale of 1 to 5, how would you rate the intensity of this move?");
                string ratingIntensity = Console.ReadLine();
            } else { }
        }



        private static void GetMoveIds()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = BornToMove;Integrated Security=True;";
            string sqlQuery = "SELECT *" +
                              "FROM dbo.move;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // access columns: reader["ColumnName"]
                                // In first call : only fetch the id's 
                                int id = (int)reader["id"];
                                ids.Add(id);
                            }
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

        static void GetRandomId()
        {
            if (ids == null || ids.Count == 0)
            {
                throw new ArgumentException("The list is either null or empty.");
            }

            Random random = new Random();
            int randomIndex = random.Next(0, ids.Count);

            randomId = ids[randomIndex];
        }

    }

}


