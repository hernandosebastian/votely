namespace Votely.Application.Surveys.Services;

public class SurveyService : ISurveyService
{
    private readonly List<Survey> _surveys = new();

    public async Task<SurveyDto> GetSurveyByIdAsync(Guid id)
    {
        var survey = _surveys.FirstOrDefault(s => s.Id == id);
        if (survey == null) throw new Exception("Survey not found");

        return new SurveyDto
        {
            Id = survey.Id,
            Title = survey.Title,
            Questions = survey.Questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Title = q.Title,
                Options = q.Options.Select(o => new OptionDto
                {
                    Id = o.Id,
                    Text = o.Text,
                    Votes = o.Votes
                }).ToList()
            }).ToList()
        };
    }

    public async Task<SurveyDto> CreateSurveyAsync(CreateSurveyDto dto)
    {
        var survey = new Survey(dto.Title, new List<Question>());
        _surveys.Add(survey);

        return new SurveyDto
        {
            Id = survey.Id,
            Title = survey.Title,
            Questions = new List<QuestionDto>()
        };
    }

    public async Task<QuestionDto> AddQuestionAsync(Guid surveyId, CreateQuestionDto dto)
    {
        var survey = _surveys.FirstOrDefault(s => s.Id == surveyId);
        if (survey == null) throw new Exception("Survey not found");

        var question = new Question(dto.Title, new List<Option>());
        survey.Questions.Add(question);

        return new QuestionDto
        {
            Id = question.Id,
            Title = question.Title,
            Options = new List<OptionDto>()
        };
    }

    public async Task<OptionDto> AddOptionAsync(Guid surveyId, Guid questionId, CreateOptionDto dto)
    {
        var survey = _surveys.FirstOrDefault(s => s.Id == surveyId);
        if (survey == null) throw new Exception("Survey not found");

        var question = survey.Questions.FirstOrDefault(q => q.Id == questionId);
        if (question == null) throw new Exception("Question not found");

        var option = new Option(dto.Text);
        question.Options.Add(option);

        return new OptionDto
        {
            Id = option.Id,
            Text = option.Text,
            Votes = option.Votes
        };
    }
}
