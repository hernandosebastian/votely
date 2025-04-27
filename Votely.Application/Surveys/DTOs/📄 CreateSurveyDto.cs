public class CreateOptionDto { public string Text { get; set; } = ""; }
public class CreateQuestionDto
{
    public string Title { get; set; } = "";
    public List<CreateOptionDto> Options { get; set; } = new();
}

public class CreateSurveyDto
{
    public string Title { get; set; } = "";
    public List<CreateQuestionDto> Questions { get; set; } = new();
}
