namespace Votely.Domain;

public class Survey
{
    public Guid SurveyId { get; set; }
    public string Title { get; set; }
    public List<Question> Questions { get; set; }

    public Survey(string title, List<Question> questions)
    {
        SurveyId = Guid.NewGuid();
        Title = title;
        Questions = questions ?? new List<Question>();
    }
}
