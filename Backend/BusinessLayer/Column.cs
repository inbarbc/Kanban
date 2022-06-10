using IntroSE.Backend.Fronted.BusinessLayer;
using System;
using System.Collections.Generic;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class Column
    {
        public int limit { get; private set; }
        public string columnName { get; private set; }

        public Dictionary<int, Task> Tasks = new Dictionary<int, Task>();

        private ColumnDTO columnDTO;
        public Column(string name,int boardID)
        {
            this.columnName = name;
            limit = -1;
            this.columnDTO = new ColumnDTO(columnName,limit,boardID);
            columnDTO.Save();
        }
        public Column(int limit, string name)
        {
            LimitColumn(limit);
            this.columnName = name; 
        }

        public void LimitColumn(int limit)                                                        
        {
            if (limit <= 0)
                throw new ArgumentException("Limit have to be a positive number");

            if (limit <= this.limit && limit < Tasks.Count)
                throw new ArgumentException("the new limit has to be bigger then number of tasks in the column");
            this.limit = limit;

        }

        public Task GetTask(int taskId)                                                       
        {
            if (!Tasks.ContainsKey(taskId))
                throw new ArgumentException("There is no task exist with this ID in this column");
            return Tasks[taskId];
        }

        public bool IsExist(int taskID)                                                  
        {
            return Tasks.ContainsKey(taskID);

        }
    }
}
