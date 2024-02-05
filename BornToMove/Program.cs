
using System.Data;
using Microsoft.Data.SqlClient;


namespace BornToMove
{
    public class Program
    {
        private static List<int> ids = new List<int>();
        private List<Move> moves = new List<Move>();


        private static void Main(string[] args)
        {
            Console.WriteLine("You should get moving!");
            Console.WriteLine("Do you want a suggestion (type: 'suggestion') or do you want to choose a move from the list (type: 'list')?");
            string userChoice = Console.ReadLine();
            Console.WriteLine($"You chose: {userChoice}");

            if (userChoice == "suggestion")
            {
                GetMoveIds();
                foreach (int id in ids)
                {
                    Console.WriteLine($"Id: {id}");
                }
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



    }

}


