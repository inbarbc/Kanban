namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class UserDTO : DTO
    {
        public const string emailColumnName = "Email";
        public const string passwordColumnName = "Password";
        public const string statusColumnName = "Status";

        private readonly string _email;
        public string Email { get => _email; }
        private string _password;
        public string Password { get => _password; set { _password = value; mapper.Update(emailColumnName, Email, passwordColumnName, Password.ToString()); } }
        private bool _status;
        public bool Status { get => _status; set { _status = value; mapper.Update(emailColumnName, Email, statusColumnName, value.ToString()); } }

        internal UserDTO(string email, string password, bool status) : base(new UserMapper())
        {
            this._email = email;
            this._password = password;
            this._status = status;
        }

        /// <summary>
        /// This method write the new information in the database.
        /// </summary>
        /// <returns> void </returns>
        public void Save()
        {
            UserMapper map = new UserMapper();
            map.Insert(this);
        }

    }
}