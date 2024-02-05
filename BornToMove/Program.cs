
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;


namespace BornToMove
{
    public class Program
    {
        private static List<int> ids = new List<int>();
        private static List<string> moveNames = new List<string>();

        private static int randomId;
        private List<int> moves = new List<int>();
        private static string? ratingIntensity;
        private static string? ratingMove;



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
                GetMoveBasedOnId(randomId);

                Console.WriteLine("On a scale of 1 to 5, how would you rate this move?");
                ratingMove = Console.ReadLine();
                Console.WriteLine("On a scale of 1 to 5, how would you rate the intensity of this move?");
                ratingIntensity = Console.ReadLine();
            } else if(userChoice == "list")
            {
                Console.WriteLine("Choose a number from the list or type '0' if you want to add a new move.");
                GetAllMoves();
                int listChoice = int.Parse(Console.ReadLine());

                if (listChoice != 0)
                {
                    GetMoveBasedOnId(listChoice);
                } else if (listChoice == 0)
                {
                    Console.WriteLine("What is the name of the new move?");
                    string nameNewMove = Console.ReadLine();

                    //als deze niet al bestaat:

                    if (moveNames.Contains(nameNewMove))
                    {
                        Console.WriteLine($"{nameNewMove} is already on the list.");
                    } else
                    {
                        Console.WriteLine("What is the description of the new move?");
                        string descriptionNewMove = Console.ReadLine();

                        Console.WriteLine("What is the sweatrate of the new move?");
                        int sweatRateNewMove = int.Parse(Console.ReadLine());

                        WriteNewMove(nameNewMove, descriptionNewMove, sweatRateNewMove);

                    }
                    
                }
                

                ratingMove = Console.ReadLine();
                Console.WriteLine("On a scale of 1 to 5, how would you rate the intensity of this move?");
                ratingIntensity = Console.ReadLine();
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

        private static void GetMoveBasedOnId(int id)
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
                                if (Convert.ToInt32(reader["id"]) == id) {

                                    string name = reader["name"].ToString();
                                    string description = reader["description"].ToString();
                                    string sweatRate = reader["sweatRate"].ToString();
                                    Console.WriteLine($"Name: {name}, Description: {description}, Sweat Rate: {sweatRate}");

                                }
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


        private static void GetAllMoves()
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
                                    int id = Convert.ToInt32(reader["id"].ToString());
                                    string name = reader["name"].ToString();
                                    string sweatRate = reader["sweatRate"].ToString();
                                    Console.WriteLine($"{id}. Name: {name}, Sweat Rate: {sweatRate}");

                                    moveNames.Add(name);
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


