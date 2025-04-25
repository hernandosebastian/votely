using Votely.Application.Surveys.DTOs;

namespace Votely.Application.Surveys;

public interface ISurveyService
{
    Task<SurveyDto> GetSurveyByIdAsync(Guid id);
    Task<SurveyDto> CreateSurveyAsync(CreateSurveyDto dto);
    Task<QuestionDto> AddQuestionAsync(Guid surveyId, CreateQuestionDto dto);
    Task<OptionDto> AddOptionAsync(Guid surveyId, Guid questionId, CreateOptionDto dto);
}