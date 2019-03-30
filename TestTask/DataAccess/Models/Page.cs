namespace TestTask.DataAccess.Models
{
    public class Page : BaseEntity
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }

        public override BaseEntity MapFrom(BaseEntity entity)
        {
            var NewEntity = (Page)entity;
            NewEntity.Name = this.Name;
            NewEntity.Likes = this.Likes;
            NewEntity.DisLikes = this.DisLikes;
            return NewEntity;
        }
    }
}
