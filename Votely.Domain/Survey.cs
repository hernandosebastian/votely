namespace Votely.Domain;

public class Survey
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<Question> Questions { get; set; }

    public Survey(string title, List<Question> questions)
    {
        Title = title;
        Questions = questions ?? new List<Question>();
    }
}
