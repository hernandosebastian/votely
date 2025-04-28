using System.Threading.Tasks;
using Votely.Infrastructure.Database.Models;

namespace Votely.Infrastructure.Surveys
{
    public interface ISurveyRepository
    {
        Task<List<SurveyModel>> GetAllSurveysAsync();
        Task<SurveyModel> GetSurveyByIdAsync(Guid surveyId);
        Task<SurveyModel> AddSurveyAsync(SurveyModel survey);
        Task<SurveyModel> UpdateSurveyAsync(SurveyModel survey);
        Task<bool> DeleteSurveyByIdAsync(Guid surveyId);
    }
}