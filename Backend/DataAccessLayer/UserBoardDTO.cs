namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class UserBoardDTO : DTO
    {
        public const string userColumnName = "User";
        public const string BoardColumnName = "BoardID";

        private readonly int _boardID;
        private readonly string _userEmail;
        public int boardID { get => _boardID; }
        public string userEmail { get => _userEmail; }


        internal UserBoardDTO(string userEmail, int BoardID) : base(new UserBoardMapper())
        {
            this._boardID = BoardID;
            this._userEmail = userEmail;
        }

        public void Save()
        {
            UserBoardMapper map = new UserBoardMapper();
            map.Insert(this);
        }
    }
}