using IntroSE.Kanban.Backend.BusinessLayer;
using log4net;
using System;
using System.Collections.Generic;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Backend.Fronted.BusinessLayer
{
    internal class User
    {
        private static readonly ILog log = LogManager.GetLogger("User");

        //private Dictionary<string, Board> boards = new Dictionary<string, Board>();
        internal List<int> BoardIDs
        {
            get { return BoardIDs; }
            private set { BoardIDs = value; }
        }

        private readonly UserController uc;

        public string UserEmail { get; private set; }
        public string Password { get; private set; }
        public bool Status { get; private set; }
        private UserDTO userDTO;

        /// <summary>
        /// This method registers a new user to the system.
        /// </summary>
        /// <param name="useremail">The user email address, used as the username for logging the system.</param>
        /// <param name="password">The user password.</param>
        /// <returns>void.</returns>
        public User(string useremail, string password)
        {
            //this.boards = new Dictionary<string, Board>();
            this.BoardIDs = new List<int>();
            this.UserEmail = useremail;
            this.Password = password;
            this.Status = false;
            this.userDTO = new UserDTO(useremail, password, Status);
            userDTO.Save();
        }

        /// <summary>
        /// This method perform login for the user in the system.
        /// </summary>
        /// <param name="useremail">The user email address.</param>
        /// <param name="password">The user password.</param>
        /// <returns>void.</returns>
        internal void login(string password)
        {
            if (Status)
            {
                log.Debug("The user is allredy log in");
                throw new Exception($"User already log in");
            }
            if (this.Password != password)
            {
                log.Debug("User with wrong password attempted login");
                throw new Exception($"Password {password} is wrong");
            }
            this.Status = true;
            userDTO.Status = Status;
        }

        /// <summary>
        /// This method perform logout from the current user in the system.
        /// </summary>
        /// <param name="useremail">The user email address.</param>
        /// <returns>void.</returns>
        internal void logout()
        {
            if (Status == false)  //If user is already logged out
            {
                log.Warn("User is not logged in");
                throw new Exception("User is not logged in");
            }
            this.Status = false;
            userDTO.Status = Status;
        }

        /// <summary>
        /// This method changed the password for the user in the system.
        /// </summary>
        /// <param name="oldP">The user old password.</param>
        /// <param name="newP">The user new password.</param>
        /// <returns>void.</returns>
        internal void changePassword(string oldP, string newP)
        {
            if (Status == false)
            {
                log.Warn("User is not logged in");
                throw new Exception("User is not logged in");
            }
            if (!uc.isValidPassword(newP))
            {
                log.Warn("User with invalid new password attempted to change password");
                throw new Exception("User with invalid new password attempted to change password");
            }
            if (oldP != Password) {
                log.Debug("User with incorrect password attempted to change password");
                throw new Exception($"Password {oldP} is not matching to the current one");
            }
            this.Password = newP; // change password successfully
        }

        /// <summary>
        /// This method return a list of the in progress list column in this board.
        /// </summary>
        /// <returns>A list of the in progress tasks, unless an error occurs</returns>
        internal List<Task> inProgressTasks()
        {
            if (Status == false)
            {
                log.Warn("User is not logged in");
                throw new Exception("User is not logged in");
            }
            List<Task> ListTasks  = new List<Task>();
            foreach (var b in boards)
            {
                Column c = b.Value.getColumn(1);
                var tasks = c.Tasks;
                foreach (var t in tasks)
                    ListTasks.Add(t.Value);
            }
            return ListTasks;
        }

        /// <summary>
        /// This method check if the board with the input name is exists.
        /// </summary>
        /// <param name="name">.the name of the board</param>
        /// <returns>boolean, true if the board exists, false otherwise </returns>
        internal bool isExists(int BoardID)
        {
            if (!BoardIDs.Contains(BoardID))
            {
                log.Warn("The board name was not found");
                throw new ArgumentException("board with this name does not exist");
            }
            return true;
        }

        /// <summary>
        /// This method returns a list of IDs of all user's boards.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A response with a list of IDs of all user's boards, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public List<int> GetUserBoards()
        {
            if (Status == false)
            {
                log.Warn("User is not logged in");
                throw new Exception("User is not logged in");
            }
            return BoardIDs;
        }
    }
}
