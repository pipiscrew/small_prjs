using Dapper;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using WindowsFormsApplication37.Applicati0n.Domain;
using WindowsFormsApplication37.Applicati0n.Interfaces;

namespace WindowsFormsApplication37.Applicati0n.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        
        public async Task AddUser(string name)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var sql = "INSERT INTO users (nam3) VALUES (@Name)";
                await connection.ExecuteAsync(sql, new { Name = name });
            }

            //using (var connection = new SQLiteConnection(_connectionString))
            //{
            //    connection.Open();
            //    using (var command = new SQLiteCommand("INSERT INTO users (name) VALUES (@name)", connection))
            //    {
            //        command.Parameters.AddWithValue("@name", name);
            //        command.ExecuteNonQuery();
            //    }
            //}
        }

        
        public async Task<string> GetCodeA(string name)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var sql = WindowsFormsApplication37.Properties.Resources.CodeA;
                return await connection.ExecuteScalarAsync<string>(sql, new { Name = "Hello" }) + name;
            }
        }

        
        public async Task<string> GetCodeB(int field, int userId)
        {
            switch (field)
            {
                case 0:
                    field = 69;
                    break;
                case 1:
                    field = (69 + 77);
                    break;
                case 2:
                    field = (69 + (77 * 2));
                    break;
                case 3:
                    field = (69 + (77 * 3));
                    break;
            }

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var sql = string.Format("select a{0} from [RAR is a console application] where u=@id", field.ToString());
                return await connection.ExecuteScalarAsync<string>(sql, new { id = userId.ToString() });
            }
        }

        
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var sql = "SELECT id, nam3 FROM users";
                return await connection.QueryAsync<User>(sql);
            }
            //var users = new List<string>();
            //using (var connection = new SQLiteConnection(_connectionString))
            //{
            //    connection.Open();
            //    using (var command = new SQLiteCommand("SELECT name FROM users", connection))
            //    using (var reader = command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            users.Add(reader["name"].ToString());
            //        }
            //    }
            //}
            //return users;
        }
    }
}
