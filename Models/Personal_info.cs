using System.Data.SQLite;

namespace tp3_dotnet.Models;

public class Personal_info
{
    private readonly IConfiguration _configuration;

    public Personal_info(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<Person> GetAllPerson()
    {
        var list = new List<Person>();
        
        using (var connection = new SQLiteConnection(_configuration.GetConnectionString("SQLite")))
        {
            connection.Open();
            const string query = @"
                    SELECT *
                    FROM personal_info
                ";
            var command = new SQLiteCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string firstName = (string)reader["first_name"];
                    string lastName = (string)reader["last_name"];
                    string email = (string)reader["email"];
                    string image = (string)reader["image"];
                    string country = (string)reader["country"];
                    list.Add(new Person(id, firstName, lastName, email, image, country));
                }
            }
        }

        return list;
    }

    public Person? GetPerson(int id)
    {
        var persons = GetAllPerson();
        return persons.Find((person) => person.Id == id);
    }
}