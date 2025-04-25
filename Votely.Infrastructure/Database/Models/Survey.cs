public class Survey
{
    public Guid SurveyId { get; set; }
    public string Title { get; set; }
    public ICollection<Question> Questions { get; set; }
}