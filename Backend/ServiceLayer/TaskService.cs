using IntroSE.Backend.Fronted.BusinessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using IntroSE.Kanban.Backend.BusinessLayer;

namespace IntroSE.Backend.Fronted.ServiceLayer
{
    public class TaskService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("TaskService");
        private readonly UserController uc;
        private readonly BoardController bc;

        public TaskService(UserController uc, BoardController bc)
        {
            this.uc = uc;
            this.bc = bc;

        }

        /// <summary>
        /// This method adds a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>The string in json format that represent a Response object with an
        /// error messeage or undefined fields</returns>
        public string AddTask(string email, string boardName, string title, string description, DateTime dueDate) //update function signature
        {
            Response res = new Response();
            try
            {
                if (!uc.IsExists(email))
                    res.ErrorMessage = "There is no user exist with this email";
                else if (!uc.IsLogIn(email))
                    res.ErrorMessage = "The user is log out";
                Board b = bc.GetBoard(email,boardName);
                b.AddTask(email, title, description, dueDate);
                res.ReturnValue = email;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return ReturnJson(res);
        }

        /// <summary>
        /// This method advances a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>The string in json format that represent a Response object with an
        /// error messeage or undefined fields</returns>
        public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            Response res = new Response();
            try
            {
                Board b = uc.GetUser(email).GetBoard(email.boardName);
                b.AdvanceTask(email, columnOrdinal, taskId);
                log.Debug("task " + taskId + " in column " + columnOrdinal + " advanced to the next column");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return ReturnJson(res);
        }

        /// <summary>
        /// This method updates the due date of a task.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="dueDate">The new due date of the task</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskID">The task to be updated identified task ID</param>
        /// <returns>The string in json format that represent a Response object with an
        /// error messeage or undefind fields</returns>
        public string UpdateTaskDueDate(string email, DateTime dueDate, string boardName, int columnOrdinal, int taskID)
        {

            Response res = new Response();
            try
            {
                if (!uc.IsExists(email))
                {
                    res.ErrorMessage = "There is no user exist with this email";
                }
                else if (!uc.IsLogIn(email))
                {
                    res.ErrorMessage = "The user is not logged in";
                }
                else if (columnOrdinal == 2)
                {
                    res.ErrorMessage = "there is no option to update task that is in 'done' column";
                }
                else
                {
                    bc.GetBoard(email, boardName).GetColumn(columnOrdinal).GetTask(taskID).UpdateTaskDueDate(dueDate);
                    log.Debug("The task due date is updated to: " + dueDate.ToString());
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return ReturnJson(res);
        }

        /// <summary>
        /// This method updates task title.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskID">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>The string in json format that represent a Response object with an
        /// error messeage or undefind fields</returns>
        public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskID, string title)
        {
            Response res = new Response();
            try
            {
                if (!uc.IsExists(email))
                {
                    res.ErrorMessage = "There is no user exist with this email";
                }
                else if (!uc.IsLogIn(email))
                {
                    res.ErrorMessage = "The user is not logged in";
                }
                else if (columnOrdinal == 2)
                {
                    res.ErrorMessage = "there is no option to update task that is in 'done' column";
                }
                else
                {
                    bc.GetBoard(email,boardName).GetColumn(columnOrdinal).GetTask(taskID).UpdateTaskTitle(title);
                    log.Debug("The task title is updated to: " + title);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return ReturnJson(res);
        }

        /// <summary>
        /// This method updates the description of a task.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskID">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>The string in json format that represent a Response object with an
        /// error messeage or undefind fields</returns>
        public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskID, string description)
        {
            Response res = new Response();
            try
            {
                if (!uc.IsExists(email))
                {
                    res.ErrorMessage = "There is no user exist with this email";
                }
                else if (!uc.IsLogIn(email))
                {
                    res.ErrorMessage = "The user is not logged in";
                }
                else if (columnOrdinal == 2)
                {
                    res.ErrorMessage = "there is no option to update task that is in 'done' column";
                }
                else
                {
                    bc.GetBoard(email, boardName).GetColumn(columnOrdinal).GetTask(taskID).UpdateTaskDescription(description);
                    log.Debug("The task description is updated to: " + description);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return ReturnJson(res);
        }

        /// <summary>
        /// This method assigns a task to a user
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column number. The first column is 0, the number increases by 1 for each column</param>
        /// <param name="taskID">The task to be updated identified a task ID</param>        
        /// <param name="emailAssignee">Email of the asignee user</param>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string AssignTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee)
        {
            throw new NotImplementedException();
        }

        ///<summary>
        /// This method receives a response, and return error if the response failed.
        /// </summary>
        /// <param name="res">The response.</param>
        ///<returns>The string in json format that represent a Response object with an
        /// error messeage or empty string</returns>
        /// 
        private string ReturnJson(Response res)
        {
            try
            {
                var options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                return JsonSerializer.Serialize(res, res.GetType(), options);
            }
            catch
            {
                log.Error("Failed to serialize Response object");
                return "Error : Failed to serialize Response object";
            }
        }
    }
}
