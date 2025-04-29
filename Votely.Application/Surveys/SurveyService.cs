using Votely.Application.Surveys.DTOs;
using Votely.Domain;

namespace Votely.Application.Surveys;

public class SurveyService : ISurveyService
{
    private readonly ISurveyRepository _surveyRepository;

    public SurveyService(ISurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }

    public async Task<List<SurveyDto>> GetAllSurveysAsync()
    {
        var surveys = await _surveyRepository.GetAllSurveysAsync();
        return surveys.Select(s => MapToDto(s)).ToList();
    }

    public async Task<SurveyDto> GetSurveyByIdAsync(Guid surveyId)
    {
        var survey = await _surveyRepository.GetSurveyByIdAsync(surveyId);
        if (survey == null)
            return null;

        return MapToDto(survey);
    }

    public async Task<SurveyDto> CreateSurveyAsync(CreateSurveyDto createSurveyDto)
    {
        var survey = MapCreateDtoToDomain(createSurveyDto);
        var saved = await _surveyRepository.AddSurveyAsync(survey);
        return MapToDto(saved);
    }

    public async Task<SurveyDto?> UpdateSurveyByIdAsync(Guid id, UpdateSurveyDto updateSurveyDto)
    {
        updateSurveyDto.Id = id;
        var survey = MapUpdateDtoToDomain(updateSurveyDto);
        var updatedSurvey = await _surveyRepository.UpdateSurveyAsync(survey);
        
        if (updatedSurvey == null)
            return null;
            
        return MapToDto(updatedSurvey);
    }

    public async Task<bool> DeleteSurveyByIdAsync(Guid surveyId)
    {
        return await _surveyRepository.DeleteSurveyByIdAsync(surveyId);
    }

    private static Survey MapCreateDtoToDomain(CreateSurveyDto dto)
    {
        var questions = dto.Questions.Select(q => 
            new Question(
                q.Title,
                q.Options.Select(o => new Option(o.Text)).ToList()
            )).ToList();
            
        return new Survey(dto.Title, questions);
    }

    private static Survey MapUpdateDtoToDomain(UpdateSurveyDto dto)
    {
        var questions = dto.Questions.Select(q => 
            new Question(
                q.Id,
                q.Title,
                q.Options.Select(o => new Option(o.Id, o.Text, o.Votes)).ToList()
            )).ToList();
            
        return new Survey(dto.Id, dto.Title, questions);
    }

    private static SurveyDto MapToDto(Survey survey)
    {
        return new SurveyDto
        {
            Id = survey.SurveyId,
            Title = survey.Title,
            Questions = survey.Questions.Select(q => new QuestionDto
            {
                Id = q.QuestionId,
                Title = q.Title,
                Options = q.Options.Select(o => new OptionDto
                {
                    Id = o.OptionId,
                    Text = o.Text,
                    Votes = o.Votes
                }).ToList()
            }).ToList()
        };
    }
}
