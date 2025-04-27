using System.ComponentModel.DataAnnotations;

namespace Votely.Infrastructure.Database.Models;
    
public class QuestionModel
{
    [Key]
    public Guid QuestionId { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid SurveyId { get; set; }
    public SurveyModel Survey { get; set; }
    public List<OptionModel> Options { get; set; } = new();
}
