using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Text.Json;
using log4net;
using IntroSE.Backend.Fronted.BusinessLayer;
using System.Text.Json.Serialization;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class ColumnService
    {
        private static readonly ILog log = LogManager.GetLogger("ColumnService");
        private readonly UserController uc;
        private readonly BoardController bc;


        public ColumnService(UserController uc, BoardController bc)
        {
            this.uc = uc;
            this.bc = bc;
        }

        /// <summary>
        /// This method returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The string in json format that represent a Response object with an
        /// error messeage or Column</returns>
        public string getColumn(string email, string boardName, int columnOrdinal)
        {
            Response res = new Response();
            try
            {
                Board b = uc.getUser(email).getBoard(boardName);
                Column c = b.getColumn(email, columnOrdinal);
                List<Task> list = new List<Task>();
                foreach (Task t in c.Tasks.Values)
                    list.Add(t);
                res.ReturnValue = list;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return returnJson(res);
        }

        /// <summary>
        /// This method limit the the number of tasks in the column.
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The limit number of the tasks in the column c</param>
        /// <returns>The string in json format that represent a Response object with an
        /// error messeage or undefined fields</returns>*/
        public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            Response res = new Response();
            try
            {
                if (!uc.isExists(email))
                    res.ErrorMessage = "There is no user exist with this email";
                else if (!uc.isLogIn(email))
                    res.ErrorMessage = "The user is log out";
                else
                {
                    Column c = bc.GetBoard(email, boardName).getColumn(columnOrdinal);
                    c.limitColumn(limit);
                    log.Debug("column limit updated to " + limit);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
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

        /// <summary>
        /// This method gets the name of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The string in json format that represent a Response object with an
        /// error messeage or column name</returns>
        public string GetColumnName(string email, string boardName, int columnOrdinal)
        {
            Response res = new Response();
            try
            {
                if (!uc.isExists(email))
                    res.ErrorMessage = "There is no user exist with this email";
                else if (!uc.isLogIn(email))
                    res.ErrorMessage = "The user is log out";
                else
                {
                    Column c = bc.GetBoard(email,boardName).GetColumn(columnOrdinal);
                    res.ReturnValue = c.columnName;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }

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

        /// <summary>
        /// This method gets the limit of a specific column.
        /// </summary>
        /// /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The string in json format that represent a Response object with an
        /// error messeage or column limit</returns>
        public string GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            Response res = new Response();
            try
            {
                if (!uc.isExists(email))
                    res.ErrorMessage = "There is no user exist with this email";
                else if (!uc.isLogIn(email))
                    res.ErrorMessage = "The user is log out";
                else
                {
                    Column c = bc.GetBoard(email, boardName).getColumn(columnOrdinal);

                    res.ReturnValue = c.limit;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
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

        ///<summary>
        /// This method receives a response, and return error if the response failed.
        /// </summary>
        /// <param name="res">The response.</param>
        ///<returns>The string in json format that represent a Response object with an
        /// error messeage or empty string</returns>
        /// 
        private string returnJson(Response res)
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