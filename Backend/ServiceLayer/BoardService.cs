using IntroSE.Backend.Fronted.BusinessLayer;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Text.Json;
using log4net;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IntroSE.Backend.Fronted.ServiceLayer
{
    public class BoardService
    {
        private static readonly ILog log = LogManager.GetLogger("BoardService");
        private readonly UserController uc;
        private readonly BoardController bc;

        public BoardService(UserController uc, BoardController bc)
        {
            this.uc = uc;
            this.bc = bc;
        }

        /// <summary>
        /// This method add a board to the current user.
        /// </summary>
        /// <param name="email">The user email address.</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>The string in json format that represent a Response object with an
        /// error messeage or empty string</returns>
        public string AddBoard(string email, string boardName)
        {
            Response res = new Response();
            try
            {
                var user = uc.GetUser(email);
                bc.AddBoard(user, boardName);
                log.Debug("new Board created with the name: " + boardName);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return returnJson(res);
        }

        ///<summary>
        /// This method receives a name, and remove the board with this name from the user's boards.
        /// </summary>
        /// <param name="email">The user email address.</param>
        /// <param name="boardName">The name of the board</param>
        ///<returns>The string in json format that represent a Response object with an
        /// error messeage or empty string</returns>
        public string RemoveBoard(string email, string boardName)
        {
            Response res = new Response();
            try
            {
                var user = uc.getUser(email);
                bc.RemoveBoard(user, boardName);
                log.Debug("Board " + boardName + " deleted");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return returnJson(res);
        }

        ///<summary>
        /// This method receives 2 emails and board ID, and changes the owner of this board.
        /// </summary>
        /// <param name="ownerEmail">The user email address that owns now the board.</param>
        /// <param name="newOwnerEmail">The user email address that will owns the board.</param>
        /// <param name="boardID">The ID of the board</param>
        ///<returns>The string in json format that represent a Response object with an
        /// error messeage or empty string</returns>
        public string ChangeOwner(string ownerEmail, string newOwnerEmail, string boardName)
        {
            Response res = new Response();
            try
            {
                bc.ChangeOwner(ownerEmail,newOwnerEmail,boardName);
                log.Info("Ownership transfered succesfully");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return returnJson(res);
        }

        /// <summary>
        /// This method adds a user as member to an existing board.
        /// </summary>
        /// <param name="email">The email of the user that joins the board. Must be logged in</param>
        /// <param name="boardID">The board's ID</param>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string JoinBoard(string email, int boardID)
        {
            Response res = new Response();
            try
            {
                bc.JoinBoard(email, boardID);
                log.Info("Board added to user Boards succesfully");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return returnJson(res);
        }


        ///<summary>
        /// This method receives a user email and board ID, and detech the user from the board.
        /// </summary>
        /// <param name="email">The user email address.</param>
        /// <param name="boardID">The ID of the board</param>
        ///<returns>The string in json format that represent a Response object with an
        /// error messeage or empty string</returns>
        public string LeaveBoard(string email, int boardID)
        {
            Response res = new Response();
            try
            {
                bc.LeaveBoard(email, boardID);
                log.Info("Board removed from user Boards succesfully");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                res.ErrorMessage = ex.Message;
            }
            return returnJson(res);
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

        /// <summary>
        /// This method returns a board's name
        /// </summary>
        /// <param name="boardId">The board's ID</param>
        /// <returns>A response with the board's name, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetBoardName(int boardId)
        {
            throw new NotImplementedException();
        }

        ///<summary>This method loads all persisted data.
        ///<para>
        ///<b>IMPORTANT:</b> When starting the system via the GradingService - do not load the data automatically, only through this method. 
        ///In some cases we will call LoadData when the program starts and in other cases we will call DeleteData. Make sure you support both options.
        ///</para>
        /// </summary>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string LoadData()
        {
            throw new NotImplementedException();
        }

        ///<summary>This method deletes all persisted data.
        ///<para>
        ///<b>IMPORTANT:</b> 
        ///In some cases we will call LoadData when the program starts and in other cases we will call DeleteData. Make sure you support both options.
        ///</para>
        /// </summary>
        ///<returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string DeleteData()
        {
            throw new NotImplementedException();
        }
    }
}
