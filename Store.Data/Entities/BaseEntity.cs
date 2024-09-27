namespace Store.Data.Entities
{
    public class BaseEntity<T>
    {
        public T id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
