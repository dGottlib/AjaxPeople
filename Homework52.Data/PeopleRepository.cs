using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework52.Data
{
    public class PeopleRepository
    {
        private string _connectionString;

        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Person> GetPeople()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            connection.Open();
            var reader = cmd.ExecuteReader();
            List<Person> people = new();
            while (reader.Read())
            {
                people.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }
            return people;
        }
        public int AddPerson(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO People(FirstName, LastName, Age)" +
                "VALUES(@firstName, @lastName, @age)" +
                "SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@firstName", person.FirstName);
            cmd.Parameters.AddWithValue("@lastName", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            return (int)(decimal)cmd.ExecuteScalar();
        }
        public void EditPerson(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE People SET FirstName = @firstName, LastName = @lastName, Age = @age" +
                "WHERE Id = @id";
            cmd.Parameters.AddWithValue("@firstName", person.FirstName);
            cmd.Parameters.AddWithValue("@lastName", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            cmd.Parameters.AddWithValue("@id", person.Id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public void DeletePerson(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM People WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
