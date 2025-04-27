using System.ComponentModel.DataAnnotations;

namespace Votely.Infrastructure.Database.Models;

public class SurveyModel
{
    [Key]
    public Guid SurveyId { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<QuestionModel> Questions { get; set; } = new();
}
