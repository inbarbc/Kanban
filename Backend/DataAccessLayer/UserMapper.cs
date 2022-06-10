using System.Data.SQLite;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class UserMapper : AbstractMapper
    {
        private const string UserTableName = "UserDTO";

        internal UserMapper() : base(UserTableName) { }

        public bool Insert(UserDTO user)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {UserTableName} ({UserDTO.emailColumnName} ,{UserDTO.passwordColumnName} ,{UserDTO.statusColumnName}) " +
                        $"VALUES (@emailVal,@passwordVal,@statusVal);";

                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", user.Email);
                    SQLiteParameter passwordParam = new SQLiteParameter(@"passwordVal", user.Password);
                    SQLiteParameter statusParam = new SQLiteParameter(@"statusVal", user.Status);

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(passwordParam);
                    command.Parameters.Add(statusParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch
                {
                    //log error
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }

        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            UserDTO result = new UserDTO(reader.GetString(0), reader.GetString(1), reader.GetBoolean(2));
            return result;
        }
    }
}