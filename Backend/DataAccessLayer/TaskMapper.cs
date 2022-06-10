using System.Data.SQLite;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class TaskMapper : AbstractMapper
    {
        private const string TaskTableName = "TaskDTO";

        internal TaskMapper() : base(TaskTableName) { }

        public bool Insert(TaskDTO task)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {TaskTableName} ({DTO.IDColumnName} ,{TaskDTO.assigneeColumnName} , {TaskDTO.titleColumnName} , " +
                        $"{TaskDTO.descriptionColumnName} ,{TaskDTO.creationDateColumnName} ,{TaskDTO.dueDateColumnName} ,{TaskDTO.ColumnColumnName} ,{TaskDTO.boardIDColumnName}) " +
                        $"VALUES (@idVal,@assigneeVal,@titleVal,@desriptionVal,@creationVal,@dueVal,@columnVal,@boardIDVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", task.TaskID);
                    SQLiteParameter assigneeParam = new SQLiteParameter(@"assigneeVal", task.Assignee);
                    SQLiteParameter titleParam = new SQLiteParameter(@"titleVal", task.Title);
                    SQLiteParameter descriptionParam = new SQLiteParameter(@"desriptionVal", task.Description);
                    SQLiteParameter creationParam = new SQLiteParameter(@"creationVal", task.CreationDate);
                    SQLiteParameter dueParam = new SQLiteParameter(@"dueVal", task.DueDate);
                    SQLiteParameter columnParam = new SQLiteParameter(@"columnVal", task.Column);
                    SQLiteParameter boardIDParam = new SQLiteParameter(@"boardIDVal", task.BoardID);


                    command.Parameters.Add(idParam);
                    command.Parameters.Add(assigneeParam);
                    command.Parameters.Add(titleParam);
                    command.Parameters.Add(descriptionParam);
                    command.Parameters.Add(creationParam);
                    command.Parameters.Add(dueParam);
                    command.Parameters.Add(columnParam);
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
            TaskDTO result = new TaskDTO(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                reader.GetString(4), reader.GetString(5), reader.GetString(6), (int)reader.GetInt32(7));
            return result;

        }
    }
}