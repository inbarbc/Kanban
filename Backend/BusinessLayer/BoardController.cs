using System;
using System.Collections.Generic;
using log4net;
using IntroSE.Backend.Fronted.BusinessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class BoardController
    {
        private Dictionary<int, Board> boards; //Contains all boards by IDboard key
        private readonly UserController uc;
        private static readonly ILog log = LogManager.GetLogger("BoardController");
        private static int counterIDboards = 0;

        public BoardController(UserController uc)
        {
            this.uc = uc;
        }

        /// <summary>
        /// This method add a board to the user's boards.
        /// </summary>
        /// <param name="name">.the name of the board that we want to add</param>
        /// <returns>void, unless an error occurs</returns>
        internal void AddBoard(User user, string nameBoard)
        {
            if (uc.isLogIn(user.UserEmail) == false)
            {
                log.Warn("User is not logged in");
                throw new Exception("User is not logged in");
            }
            if (!IsBoardExists(user.UserEmail, nameBoard))
            {
                log.Warn("The name is already in the system");
                throw new ArgumentException("board with this name is already exist");
            }
            if (string.IsNullOrWhiteSpace(nameBoard))
            {
                log.Warn("board with this empty name cannot be created");
                throw new ArgumentException("board with this empty name cannot be created");
            }

            boards.Add(counterIDboards, new Board(nameBoard, user.UserEmail, counterIDboards));
            UserBoardDTO user_boardDTO = new UserBoardDTO(user.UserEmail, counterIDboards);
            user_boardDTO.Save();
            counterIDboards++;
        }

        /// <summary>
        /// This method returns a board's name
        /// </summary>
        /// <param name="boardId">The board's ID</param>
        /// <returns>A response with the board's name, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetBoardName(int boardId)
        {
            if (!boards.ContainsKey(boardId))
                throw new ArgumentException("there is no Board with this ID");
            else
                return boards[boardId].name;
        }

        /// <summary>
        /// This method adds a user as member to an existing board.
        /// </summary>
        /// <param name="email">The email of the user that joins the board. Must be logged in</param>
        /// <param name="boardID">The board's ID</param>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public void JoinBoard(string email, int boardID)
        {
            try
            {
                if (!uc.isExists(email))
                    throw new ArgumentException("there is no user with this ID");
                else if (!uc.isLogIn(email))
                    throw new Exception("User is not logged in");
                else
                {
                    uc.getUser(email).BoardIDs.Add(boardID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// This method removes a user from the members list of a board.
        /// </summary>
        /// <param name="email">The email of the user. Must be logged in</param>
        /// <param name="boardID">The board's ID</param>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public void LeaveBoard(string email, int boardID)
        {
            try
            {
                if (!uc.isExists(email))
                    throw new ArgumentException("there is no user with this ID");
                else if (!uc.isLogIn(email))
                    throw new Exception("User is not logged in");
                else
                {
                    uc.getUser(email).BoardIDs.Remove(boardID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// This method return the board with the input name.
        /// </summary>
        /// <param name="name">.the name of the board</param>
        /// <returns>the board with the input name, unless an error occurs </returns>
        /*internal Board getBoard(string name)
        {
            if (Status == false)
            {
                log.Warn("User is not logged in");
                throw new Exception("User is not logged in");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                log.Debug("The name of the board is invalid");
                throw new Exception("The name of the board is invalid");
            }
            else if (!boards.ContainsKey(name))
            {
                log.Debug("A board with such a name does not exists");
                throw new Exception("A board with such a name does not exists");
            }
            return boards[name];
        }
        */

        ///<summary>
        /// This method receives 2 emails and board ID, and changes the owner of this board.
        /// </summary>
        /// <param name="ownerEmail">The user email address that owns now the board.</param>
        /// <param name="newOwnerEmail">The user email address that will owns the board.</param>
        /// <param name="boardID">The ID of the board</param>
        ///<returns>The string in json format that represent a Response object with an
        /// error messeage or empty string</returns>
        public void ChangeOwner(string ownerEmail, string newOwnerEmail, int boardID)      //wrong signature??
        {
            try
            {
                if (!uc.isExists(ownerEmail) || !uc.isExists(newOwnerEmail))
                    throw new ArgumentException("there is no user with this ID");
                else if (!boards.ContainsKey(boardID))
                    throw new ArgumentException("there is no Board with this ID");
                else if (boards[boardID].owner != ownerEmail)
                    throw new ArgumentException(ownerEmail + " is not the owner of the board");
                else
                {
                    boards[boardID].owner = newOwnerEmail;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// This method remove the board from the user's boards .
        /// </summary>
        /// <param name="name">.the name of the board that we want to add</param>
        /// <returns>void, unless an error occurs</returns>
        internal void removeBoard(string email, string Boardname)                                                     
        {
            try
            {
                int ID = BoardID(email, Boardname);
                if (!uc.isExists(email))
                    throw new ArgumentException("there is no user with this email");
                else if (!uc.isLogIn(email))
                    throw new Exception("User is not logged in");
                else if (boards[ID].owner != email)
                    throw new ArgumentException(email + " is not the owner of the board");
                else
                {
                    boards.Remove(ID);
                    uc.getUser(email).BoardIDs.Remove(ID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Board GetBoard(string email, string nameBoard)
        {
            try
            {
                foreach (var b in uc.getUser(email).BoardIDs)
                {
                    if (boards[b].name.Equals(nameBoard))
                        return boards[b];
                }
                throw new Exception("there is no Board with this ID for this User");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsBoardExists(string email, string nameBoard)

        {
            try
            {
                foreach (var b in uc.getUser(email).BoardIDs)
                {
                    if (boards[b].name.Equals(nameBoard))
                        return true;

                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
