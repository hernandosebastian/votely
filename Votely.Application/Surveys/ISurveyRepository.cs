using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Votely.Domain;

namespace Votely.Application.Surveys
{
    public interface ISurveyRepository
    {
        Task<List<Survey>> GetAllSurveysAsync();
        Task<Survey> GetSurveyByIdAsync(Guid surveyId);
        Task<Survey> AddSurveyAsync(Survey survey);
        Task<Survey> UpdateSurveyAsync(Survey survey);
        Task<bool> DeleteSurveyByIdAsync(Guid surveyId);
    }
}