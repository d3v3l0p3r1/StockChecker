
public interface IRedditProcessor
{
    Task ProcessAsync(DateOnly startDate, DateOnly endDate);
}