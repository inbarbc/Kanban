using System;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Backend.Fronted.BusinessLayer
{
    public class Task
    {
        public int Id { get; private set; }
        public DateTime CreationTime { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime DueDate { get; private set; }

        private TaskDTO taskDTO;

        public string EmailAssignee { get; private set; }

        public Task(string title,string description,DateTime dueDate, int taskID, string useremail)
        {
            this.Title = title;     
            this.Description = description;
            this.DueDate = dueDate;
            this.CreationTime = DateTime.Now;
            this.Id = taskID;
            this.EmailAssignee = useremail;
            taskDTO = new TaskDTO(Id, CreationTime, DueDate, Title, Description, useremail);
            taskDTO.Save();
        }

        /// <summary>
        /// This method updates the due date of a task
        /// </summary>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>void</returns>*/
        public void UpdateTaskDueDate(DateTime dueDate)
        {
            if (dueDate.CompareTo(DateTime.Now) >= 0)
                this.DueDate = dueDate;
            else
                throw new ArgumentException("this due date is not valid");
            taskDTO.DueDate = dueDate;
        }

        /// <summary>
        /// This method updates task title.
        /// </summary>
        /// <param name="title">New title for the task</param>
        /// <returns>void</returns>*/
        public void UpdateTaskTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("the title is invalid");
            if(title.Length>50)
                throw new ArgumentException("this title length is over 50 chars");
            this.Title = title;
            taskDTO.Title = title;
        }

        /// <summary>
        /// This method updates the description of a task.
        /// </summary>
        /// <param name="description">New description for the task</param>
        /// <returns>void</returns>*/
        public void UpdateTaskDescription(string description)
        {
            if(description != null && description.Length > 300)
                throw new ArgumentException("this description length is over 300 chars");
            this.Description = description;
            taskDTO.Description = description;
        }

    }
}
