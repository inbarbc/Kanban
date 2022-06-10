namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class TaskDTO : DTO
    {
        public const string titleColumnName = "Title";
        public const string descriptionColumnName = "Description";
        public const string assigneeColumnName = "Assignee";
        public const string creationDateColumnName = "CreationDate";
        public const string dueDateColumnName = "DueDate";
        public const string ColumnColumnName = "Column";
        public const string boardIDColumnName = "BoardID";


        private readonly int _taskID;
        public int TaskID { get => _taskID; }
        private string _creationDate;
        public string CreationDate { get => _creationDate; }
        private string _dueDate;
        public string DueDate { get => _dueDate; set { _dueDate = value; mapper.Update(DTO.IDColumnName, TaskID.ToString(), dueDateColumnName, value); } }
        private string _title;
        public string Title { get => _title; set { _title = value; mapper.Update(DTO.IDColumnName, TaskID.ToString(), titleColumnName, value); } }
        private string _description;
        public string Description { get => _description; set { _description = value; mapper.Update(DTO.IDColumnName, TaskID.ToString(), descriptionColumnName, value); } }
        private string _assignee;
        public string Assignee { get => _assignee; set { _assignee = value; mapper.Update(DTO.IDColumnName, TaskID.ToString(), assigneeColumnName, value); } }
        private string _column;
        public string Column { get => _column; set { _column = value; mapper.Update(DTO.IDColumnName, TaskID.ToString(), ColumnColumnName, value); } }
        private int _boardID;
        public int BoardID { get => _boardID; }

        internal TaskDTO(int taskID, string creationDate, string dueDate, string title,
            string description, string assignee, string column, int BoardID) : base(new TaskMapper())
        {
            this._taskID = taskID;
            this._creationDate = creationDate;
            this._dueDate = dueDate;
            this._title = title;
            this._description = description;
            this._assignee = assignee;
        }

        public void Save()
        {
            TaskMapper map = new TaskMapper();
            map.Insert(this);
        }
    }
}