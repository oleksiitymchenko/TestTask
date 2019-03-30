namespace TestTask.DataAccess.Models
{
    public class UserLikedPage:BaseEntity
    {
        public string Username { get; set; }
        public string Pagename { get; set; }
        public override int Id { get; set; }

        public override BaseEntity MapFrom(BaseEntity entity)
        {
            var NewEntity = (UserLikedPage)entity;
            NewEntity.Username = this.Username;
            NewEntity.Pagename = this.Pagename;
            return NewEntity;
        }
    }
}
