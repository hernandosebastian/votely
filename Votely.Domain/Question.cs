namespace Votely.Domain;

public class Question
{
    public Guid QuestionId { get; set; }
    public string Title { get; set; }
    public List<Option> Options { get; set; }

    public Question(string title, List<Option> options)
    {
        QuestionId = Guid.NewGuid();
        Title = title;
        Options = options;
    }
}
