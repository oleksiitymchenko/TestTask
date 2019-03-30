namespace TestTask.DataAccess.Models
{
    public class Page : BaseEntity
    {
        public override int Id { get; set; }
        public string PageName { get; set; }
        public int Likes { get; set; }

        public override BaseEntity MapFrom(BaseEntity entity)
        {
            var NewEntity = (Page)entity;
            NewEntity.PageName = this.PageName;
            NewEntity.Likes = this.Likes;
            return NewEntity;
        }
    }
}
