using System;
using System.Data.SQLite;

class Program
{
    private const string ConnectionString = "Data Source=school.db;Version=3;";

    static void Main()
    {
        CreateTables();
        Menu();
    }

    static void CreateTables()
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS Students (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                );
            ";

            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    static void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n=== STUDENT DATABASE ===");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View Students");
            Console.WriteLine("3. Exit");
            Console.Write("Choice: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                AddStudent();
            }
            else if (choice == "2")
            {
                ViewStudents();
            }
            else if (choice == "3")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
    }

    static void AddStudent()
    {
        Console.Write("Enter student name: ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be empty.");
            return;
        }

        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string sql = "INSERT INTO Students (Name) VALUES (@name)";
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Student added.");
    }

    static void ViewStudents()
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string sql = "SELECT Id, Name FROM Students";
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("\n--- Students ---");

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["Id"]);
                    string name = reader["Name"].ToString();
                    Console.WriteLine(id + ": " + name);
                }
            }
        }
    }
}
//Well that's finally done...
/*I made this project to practice C# and SQL concepts
 including CRUD operations and relational databases.*/

