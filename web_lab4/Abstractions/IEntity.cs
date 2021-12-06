namespace web_lab4.Abstractions
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}