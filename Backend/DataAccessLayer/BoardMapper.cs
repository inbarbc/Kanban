using System.Data.SQLite;


namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class BoardMapper : AbstractMapper
    {
        private const string BoardTableName = "BoardDTO";

        internal BoardMapper() : base(BoardTableName) { }

        public bool Insert(BoardDTO board)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {BoardTableName} ({DTO.IDColumnName} ,{BoardDTO.BoardNameColumnName} ,{BoardDTO.BoardOwnerColumnName}) " +
                        $"VALUES (@idVal,@nameVal,@ownerVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", board.boardID);
                    SQLiteParameter titleParam = new SQLiteParameter(@"nameVal", board.Name);
                    SQLiteParameter ownerParam = new SQLiteParameter(@"ownerVal", board.Owner);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(titleParam);
                    command.Parameters.Add(ownerParam);
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
            BoardDTO result = new BoardDTO((int)reader.GetValue(0), reader.GetString(1), reader.GetString(2));
            return result;

        }
    }
}