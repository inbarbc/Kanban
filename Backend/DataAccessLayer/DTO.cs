namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal abstract class DTO
    {
        public const string IDColumnName = "ID";
        protected AbstractMapper mapper;
        //public long Id { get; set; } = -1;
        protected DTO(AbstractMapper mapper)
        {
            this.mapper = mapper;
        }

    }
}