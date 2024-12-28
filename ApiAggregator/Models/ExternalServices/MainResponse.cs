namespace ApiAggregator.Models.ExternalServices;

public class MainResponse
{
    public string Message { get; set; }
    public int SuccessfullyExecutedTasks { get; set; }
    public int ExecutedTasks { get; set; } = 3;
    public DateTime ExecutionDate { get; set; } = DateTime.UtcNow;
}