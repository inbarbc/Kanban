namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class BoardDTO : DTO
    {
        private readonly int _boardId;

        public const string BoardNameColumnName = "BoardName";
        public const string BoardOwnerColumnName = "Owner";

        public int boardID { get => _boardId; }
        private string _name;
        private string _owner;
        public string Name { get => _name; set { _name = value; mapper.Update(DTO.IDColumnName, boardID.ToString(), BoardNameColumnName, value); } }
        public string Owner { get => _owner; set { _name = value; mapper.Update(DTO.IDColumnName, boardID.ToString(), BoardOwnerColumnName, value); } }

        internal BoardDTO(int boardID, string name, string owner) : base(new BoardMapper())
        {
            this._boardId = boardID;
            this._name = name;
            this._owner = owner;
        }

        public void Save()
        {
            BoardMapper map = new BoardMapper();
            map.Insert(this);
        }
    }
}