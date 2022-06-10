using System.Data.SQLite;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class UserBoardMapper : AbstractMapper
    {
        private const string UserBoardTableName = "UserBoardDTO";

        internal UserBoardMapper() : base(UserBoardTableName) { }

        public bool Insert(UserBoardDTO userBoard)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {UserBoardTableName} ({UserBoardDTO.userColumnName} ,{UserBoardDTO.BoardColumnName}) " +
                        $"VALUES (@userVal,@boardVal);";

                    SQLiteParameter userParam = new SQLiteParameter(@"userVal", userBoard.userEmail);
                    SQLiteParameter BoardParam = new SQLiteParameter(@"boardVal", userBoard.boardID);

                    command.Parameters.Add(userParam);
                    command.Parameters.Add(BoardParam);
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
            UserBoardDTO result = new UserBoardDTO(reader.GetString(0), reader.GetInt32(1));
            return result;
        }
    }
}