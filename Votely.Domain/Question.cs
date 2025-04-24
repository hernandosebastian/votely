namespace Votely.Domain;

public class Question
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public List<Option> Options { get; set; }

    public Question(string title, List<Option> options)
    {
        Title = title;
        Options = options;
    }
}
