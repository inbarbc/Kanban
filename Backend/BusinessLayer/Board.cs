using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
namespace IntroSE.Backend.Fronted.BusinessLayer
{
    public class Board
    {
        private readonly UserController uc;
        public string name { get; private set; }

        public int boardID { get; private set; }

        public string owner { get; internal set; }

        public int counterIDtask { get; private set; }

        public const int backlog = 0;
        public const int inProgress = 1;
        public const int done =2;

        private List<Column> columns;
        public Board(string name,string ownerEmail,int counterIDboards)
        {
            this.name = name;
            columns = new List<Column>();
            columns.Add(new Column("backlog",boardID));
            columns.Add(new Column("in progress",boardID));
            columns.Add(new Column("done",boardID));
            this.owner = ownerEmail;
            this.boardID = counterIDboards;
            this.counterIDtask = 0;
        }

        /// <summary>
        /// This method edit the name of the board.
        /// </summary>
        /// <param name="name">.the name of the board</param>
        /// <returns>void</returns>*/
        internal void EditName(string name) {
            if (string.IsNullOrWhiteSpace(name))
                this.name = name;
            else
                throw new ArgumentException("the name received is invalid");
        }

        /// <summary>
        /// This method move task from a list to another list.
        /// </summary>
        /// <param name="useremail">The user email address.</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>void</returns>*/
        internal void AdvanceTask(string useremail, int columnOrdinal, int taskId)
        {
            if(!uc.IsLogIn(useremail))
                throw new Exception("The user is not log in in the system");
            if (columnOrdinal == done)
                throw new Exception("the task is in the last column and cannot continue to move forward");
            if (columnOrdinal >= done + 1 || columnOrdinal < backlog)
                throw new IndexOutOfRangeException("there is no column exist with this Column ID");
            if (!columns[columnOrdinal].IsExist(taskId))
                throw new ArgumentException("there is no task exist with this task ID in this column ID");
            if (columns[columnOrdinal + 1].Tasks.Count == columns[columnOrdinal + 1].limit)
                throw new ArgumentException("the task can not be advanced due limition on tasks number");
            columns[columnOrdinal + 1].Tasks.Add(taskId, columns[columnOrdinal].GetTask(taskId));
            columns[columnOrdinal].Tasks.Remove(taskId);
        }

        /// <summary>
        /// This method adds a new task.
        /// </summary>
        /// <param name="useremail">The user email address.</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>void</returns>
        public void AddTask(string useremail, string title, string description, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("the title is invalid");
            if (columns[0].limit == columns[0].Tasks.Count)
                throw new ArgumentException("the backLog column is full");
            Task t = new Task(title, description, dueDate, counterIDtask, useremail);
            columns[0].Tasks.Add(t.Id, t);
            counterIDtask = counterIDtask + 1;
        }

        /// <summary>
        /// This method returns a requested column.
        /// </summary>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>column, unless an error occurs </returns>
        public Column GetColumn(int columnOrdinal)
        {
            if (columnOrdinal < backlog || columnOrdinal >= done)
                throw new IndexOutOfRangeException("there is no Column with this Column ID");
            return columns[columnOrdinal];
        }
    }
}

