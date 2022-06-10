using System.Data.SQLite;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class ColumnMapper : AbstractMapper
    {
        private const string ColumnTableName = "ColumnDTO";

        internal ColumnMapper() : base(ColumnTableName) { }

        public bool Insert(ColumnDTO column)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {ColumnTableName} ({ColumnDTO.ColumnNameColumnName} ,{ColumnDTO.LimitColumnName} ,{ColumnDTO.BoardIDColumnName}) " +
                        $"VALUES (@columnNameVal,@nameVal,@ownerVal);";

                    SQLiteParameter columnNameParam = new SQLiteParameter(@"columnNameVal", column.columnName);
                    SQLiteParameter limitParam = new SQLiteParameter(@"limitVal", column.limit);
                    SQLiteParameter boardIDParam = new SQLiteParameter(@"BoardIDVal", column.boardID);

                    command.Parameters.Add(columnNameParam);
                    command.Parameters.Add(limitParam);
                    command.Parameters.Add(boardIDParam);
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
            ColumnDTO result = new ColumnDTO(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2));
            return result;
        }
    }
}