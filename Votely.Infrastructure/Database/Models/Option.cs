using System.ComponentModel.DataAnnotations;

namespace Votely.Infrastructure.Database.Models;

public class OptionModel
{
    [Key]
    public Guid OptionId { get; set; }
    public string Text { get; set; } = string.Empty;
    public int Votes { get; set; }
    public Guid QuestionId { get; set; }
    public QuestionModel Question { get; set; }
}
