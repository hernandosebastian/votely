using Votely.Application.Surveys.DTOs;
using Votely.Domain;

namespace Votely.Application.Surveys
{
    public interface ISurveyService
    {
        Task<List<SurveyDto>> GetAllSurveysAsync();
        Task<SurveyDto> GetSurveyByIdAsync(Guid surveyId);
        Task<SurveyDto> CreateSurveyAsync(CreateSurveyDto createSurveyDto);
        Task<SurveyDto?> UpdateSurveyByIdAsync(Guid id, UpdateSurveyDto updateSurveyDto);
        Task<bool> DeleteSurveyByIdAsync(Guid surveyId);
    }
}
