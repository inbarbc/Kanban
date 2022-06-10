using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using IntroSE.Backend.Fronted.BusinessLayer;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace IntroSE.Backend.Fronted.ServiceLayer
{
    public class UserService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("UserService");
        private readonly UserController uc;

        public UserService(UserController uc)
        {
            this.uc = uc;
        }

        /// <summary>
        /// This method registers a new user to the system.
        /// </summary>
        /// <param name="email">The user email address, used as the username for logging the system.</param>
        /// <param name="password">The user password.</param>
        /// <returns>The string in json format that represent a Response object with an
        /// error messeage or empty string</returns>
        public string Register(string email, string password)
        {
            Response res = new Response();
            try
            {
                uc.createUser(email, password);
                log.Info("Register done succesfully");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return ReturnJson(res);
        }

        /// <summary>
        /// This method perform login for the user in the system.
        /// </summary>
        /// <param name="email">The user email address.</param>
        /// <param name="password">The user password.</param>
        /// <returns>The string in json format that represent a Response object with an
        /// error messeage or empty string</returns>
        public string Login(string email, string password)
        {
            Response res = new Response();
            try
            {
                uc.getUser(email).login(password);
                log.Info("Logged in succesfully");
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
        /// This method perform logout from the current user in the system.
        /// </summary>
        /// <param name="email">The user email address.</param>
        /// <returns>The string in json format that represent a Response object with an
        /// error messeage or empty string</returns>
        public string Logout(string email)
        {
            Response res = new Response();
            try
            {
                uc.getUser(email).logout();
                log.Info("Lougout succesfully");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return ReturnJson(res);
        }

        /// <summary>
        /// This method changed the password for the user in the system.
        /// </summary>
        /// <param name="email">The user email address.</param>
        /// <param name="oldP">The user old password.</param>
        /// <param name="newP">The user new password.</param>
        /// <returns>The string in json format that represent a Response object with an
        /// error messeage or empty string</returns>
        public string ChangePassword(string email, string oldP, string newP)
        {
            Response res = new Response();
            try
            {
                uc.getUser(email).changePassword(oldP, newP);
                log.Info("password changed succesfully");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return ReturnJson(res);
        }

        /// <summary>
        /// This method returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">The user email address.</param>
        /// <returns> The string in json format that represent a Response object with an
        /// error messeage or list of all in progress tasks of the user</returns>*/
        public string InProgressTasks(string email)
        {
            List<Task> tasks;
            Response res = new Response();
            try
            {
                tasks = uc.getUser(email).inProgressTasks();
                res.ReturnValue = tasks;
                log.Info("return in progress tasks succesfully");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return ReturnJson(res);
        }

        /// <summary>
        /// This method returns a list of IDs of all user's boards.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A response with a list of IDs of all user's boards, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetUserBoards(string email)
        {
            Response res = new Response();
            try
            {
                List<int> BoardIds = uc.getUser(email).GetUserBoards();
                res.ReturnValue = BoardIds; 
                log.Info("Board Ids of the user returned successfully");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return ReturnJson(res);
        }

        ///<summary>
        /// This method receives a response, and return error if the response failed.
        /// </summary>
        /// <param name="res">The response.</param>
        ///<returns>The string in json format that represent a Response object with an
        /// error messeage or empty string</returns>
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
