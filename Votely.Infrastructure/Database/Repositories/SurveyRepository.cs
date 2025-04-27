using Microsoft.EntityFrameworkCore;
using Votely.Domain;
using Votely.Infrastructure.Database;
using Votely.Infrastructure.Database.Models;
using Votely.Infrastructure.Surveys;

namespace Votely.Infrastructure.Database.Repositories;

public class SurveyRepository : ISurveyRepository
{
    private readonly VotelyDbContext _context;

    public SurveyRepository(VotelyDbContext context)
    {
        _context = context;
    }

    public async Task<List<SurveyModel>> GetAllSurveysAsync()
    {
        return await _context.Surveys
            .Include(s => s.Questions)
            .ThenInclude(q => q.Options)
            .ToListAsync();
    }

    public async Task<SurveyModel> GetSurveyByIdAsync(Guid surveyId)
    {
        return await _context.Surveys
            .Include(s => s.Questions)
            .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(s => s.SurveyId == surveyId);
    }

    public async Task<SurveyModel> AddSurveyAsync(SurveyModel survey)
    {
        await _context.Surveys.AddAsync(survey);
        await _context.SaveChangesAsync();

        return survey;
    }

    public async Task<SurveyModel> UpdateSurveyAsync(SurveyModel survey)
    {
        var existingSurvey = await _context.Surveys
            .Include(s => s.Questions)
            .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(s => s.SurveyId == survey.SurveyId);

        if (existingSurvey == null)
            return null;

        _context.Entry(existingSurvey).CurrentValues.SetValues(survey);

        foreach (var existingQuestion in existingSurvey.Questions.ToList())
        {
            if (!survey.Questions.Any(q => q.QuestionId == existingQuestion.QuestionId))
                _context.Questions.Remove(existingQuestion);
        }

        foreach (var question in survey.Questions)
        {
            var existingQuestion = existingSurvey.Questions
                .FirstOrDefault(q => q.QuestionId == question.QuestionId);

            if (existingQuestion == null)
            {
                existingSurvey.Questions.Add(question);
            }
            else
            {
                _context.Entry(existingQuestion).CurrentValues.SetValues(question);

                foreach (var existingOption in existingQuestion.Options.ToList())
                {
                    if (!question.Options.Any(o => o.OptionId == existingOption.OptionId))
                        _context.Options.Remove(existingOption);
                }

                foreach (var option in question.Options)
                {
                    var existingOption = existingQuestion.Options
                        .FirstOrDefault(o => o.OptionId == option.OptionId);

                    if (existingOption == null)
                    {
                        existingQuestion.Options.Add(option);
                    }
                    else
                    {
                        _context.Entry(existingOption).CurrentValues.SetValues(option);
                    }
                }
            }
        }

        await _context.SaveChangesAsync();
        return existingSurvey;
    }

    public async Task<bool> DeleteSurveyByIdAsync(Guid surveyId)
    {
        var survey = await _context.Surveys.FindAsync(surveyId);
        
        if (survey == null)
            return false;

        _context.Surveys.Remove(survey);
        await _context.SaveChangesAsync();
        return true;
    }
}
