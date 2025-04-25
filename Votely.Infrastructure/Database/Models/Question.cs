public class Question
{
    public Guid QuestionId { get; set; }
    public string Title { get; set; }
    public ICollection<Option> Options { get; set; }

    public Guid SurveyId { get; set; }
    public Survey Survey { get; set; }
}