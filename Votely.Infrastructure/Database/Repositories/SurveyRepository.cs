using Microsoft.EntityFrameworkCore;
using Votely.Application.Surveys;
using Votely.Domain;
using Votely.Infrastructure.Database.Models;

namespace Votely.Infrastructure.Database.Repositories;

public class SurveyRepository : ISurveyRepository
{
    private readonly VotelyDbContext _context;

    public SurveyRepository(VotelyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Survey>> GetAllSurveysAsync()
    {
        var models = await _context.Surveys
            .Include(s => s.Questions)
            .ThenInclude(q => q.Options)
            .ToListAsync();
            
        return models.Select(MapToDomain).ToList();
    }

    public async Task<Survey> GetSurveyByIdAsync(Guid surveyId)
    {
        var model = await _context.Surveys
            .Include(s => s.Questions)
            .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(s => s.SurveyId == surveyId);
            
        if (model == null)
            return null;
            
        return MapToDomain(model);
    }

    public async Task<Survey> AddSurveyAsync(Survey survey)
    {
        var model = MapToModel(survey);
        await _context.Surveys.AddAsync(model);
        await _context.SaveChangesAsync();

        return MapToDomain(model);
    }

    public async Task<Survey> UpdateSurveyAsync(Survey survey)
    {
        var model = MapToModel(survey);
        var existingSurvey = await _context.Surveys
            .Include(s => s.Questions)
            .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(s => s.SurveyId == model.SurveyId);

        if (existingSurvey == null)
            return null;

        _context.Entry(existingSurvey).CurrentValues.SetValues(model);

        foreach (var existingQuestion in existingSurvey.Questions.ToList())
        {
            if (!model.Questions.Any(q => q.QuestionId == existingQuestion.QuestionId))
                _context.Questions.Remove(existingQuestion);
        }

        foreach (var question in model.Questions)
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
        return MapToDomain(existingSurvey);
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
    
    private static SurveyModel MapToModel(Survey survey)
    {
        return new SurveyModel
        {
            SurveyId = survey.SurveyId,
            Title = survey.Title,
            Questions = survey.Questions.Select(q => new QuestionModel
            {
                QuestionId = q.QuestionId,
                Title = q.Title,
                SurveyId = survey.SurveyId,
                Options = q.Options.Select(o => new OptionModel
                {
                    OptionId = o.OptionId,
                    Text = o.Text,
                    Votes = o.Votes,
                    QuestionId = q.QuestionId
                }).ToList()
            }).ToList()
        };
    }
    
    private static Survey MapToDomain(SurveyModel model)
    {
        var questions = model.Questions.Select(q => 
            new Question(
                q.QuestionId,
                q.Title,
                q.Options.Select(o => new Option(
                    o.OptionId,
                    o.Text,
                    o.Votes
                )).ToList()
            )).ToList();
            
        return new Survey(model.SurveyId, model.Title, questions);
    }
}
