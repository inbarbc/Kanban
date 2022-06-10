namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class ColumnDTO : DTO
    {
        public const string ColumnNameColumnName = "ColumnName";
        public const string LimitColumnName = "Limit";
        public const string BoardIDColumnName = "BoardID";

        private readonly int _boardID;
        public int boardID { get => _boardID; }

        private readonly string _columnName;
        public string columnName { get => _columnName; }
        private int _limit;
        public int limit { get => _limit; set { _limit = value; mapper.Update(DTO.IDColumnName, boardID.ToString(), LimitColumnName, value.ToString()); } }

        internal ColumnDTO(string columnName, int limit, int BoardID) : base(new ColumnMapper())
        {
            _columnName = columnName;
            _limit = limit;
            _boardID = BoardID;
        }

        public void Save()
        {
            ColumnMapper map = new ColumnMapper();
            map.Insert(this);
        }
    }
}