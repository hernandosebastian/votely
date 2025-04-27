namespace Votely.Application.Surveys.DTOs;

public class UpdateSurveyDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public List<UpdateQuestionDto> Questions { get; set; } = new();
}

public class UpdateQuestionDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public List<UpdateOptionDto> Options { get; set; } = new();
}

public class UpdateOptionDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
    public int Votes { get; set; }
}
