using MySql.Data.MySqlClient;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class TodoRepository
    {
        private readonly string _connectionString = string.Empty;
        public TodoRepository(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public List<Todo> GetAll()
        {
            List<Todo> todos = new List<Todo>();

            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT * FROM todo";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    todos.Add(new Todo() { 
                        Id = reader.GetInt32("Id"),
                        Title = reader.GetString("Title"),
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();

            return todos;
        }

        public Todo Create(Todo item)
        {
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "INSERT INTO Todo (Title) VALUES (@Title)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Title", item.Title);
                cmd.ExecuteNonQuery();

                item.Id = (int)cmd.LastInsertedId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return item;
        }

        public Todo Update(Todo item)
        {
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "UPDATE Todo SET title=@Title WHERE id=@Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Title", item.Title);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                int rowsAffected = cmd.ExecuteNonQuery();

                if(rowsAffected < 1)
                {
                    //fail
                    throw new Exception("Failed to Update");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return item;
        }

        public bool Delete(int id)
        {
            MySqlConnection conn = new MySqlConnection(_connectionString);
            try
            {
                conn.Open();

                string sql = "DELETE FROM todo WHERE id=@Id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                return cmd.ExecuteNonQuery() > 0;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();

            return true;
        }
    }
}
