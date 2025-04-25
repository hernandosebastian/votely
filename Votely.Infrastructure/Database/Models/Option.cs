namespace Votely.Infrastructure.Database.Models;

public class Option
{
    public Guid OptionId { get; set; }
    public string Text { get; set; }
    public int Votes { get; set; }

    public Guid QuestionId { get; set; }
    public Question Question { get; set; }
}
