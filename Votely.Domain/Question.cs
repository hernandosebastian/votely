namespace Votely.Domain;

public class Question
{
    public Guid QuestionId { get; set; }
    public string Title { get; set; }
    public List<Option> Options { get; set; }

    public Question(string title, List<Option> options)
        : this(Guid.NewGuid(), title, options)
    {
    }

    public Question(Guid questionId, string title, List<Option> options)
    {
        QuestionId = questionId;
        Title = title;
        Options = options ?? new List<Option>();
    }
}
