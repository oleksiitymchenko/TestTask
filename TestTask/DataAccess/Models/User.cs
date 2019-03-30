namespace TestTask.DataAccess.Models
{
    public class User:BaseEntity
    {
        public override int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public override BaseEntity MapFrom(BaseEntity entity)
        {
            var NewEntity = (User)entity;
            NewEntity.Email = this.Email;
            NewEntity.Password = this.Password;
            return NewEntity;
        }
    }
}
