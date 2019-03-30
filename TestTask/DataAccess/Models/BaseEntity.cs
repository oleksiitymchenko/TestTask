namespace TestTask.DataAccess.Models
{
    public abstract class BaseEntity
    {
        public abstract int Id { get; set; }
        public abstract BaseEntity MapFrom(BaseEntity entity);
    }
}
