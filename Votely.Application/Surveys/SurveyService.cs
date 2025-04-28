using Votely.Application.Surveys.DTOs;
using Votely.Infrastructure;
using Votely.Application.Mappers;
using Votely.Domain;
using Votely.Infrastructure.Surveys;

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
        return surveys.Select(s => SurveyMapper.MapToDto(s)).ToList();
    }

    public async Task<SurveyDto> GetSurveyByIdAsync(Guid surveyId)
    {
        var survey = await _surveyRepository.GetSurveyByIdAsync(surveyId);
        if (survey == null)
            return null;

        return SurveyMapper.MapToDto(survey);
    }

    public async Task<SurveyDto> CreateSurveyAsync(CreateSurveyDto createSurveyDto)
    {
        var model = SurveyMapper.MapToModel(createSurveyDto);
        var saved = await _surveyRepository.AddSurveyAsync(model);
        return SurveyMapper.MapToDto(saved);
    }

    public async Task<SurveyDto?> UpdateSurveyByIdAsync(Guid id, UpdateSurveyDto updateSurveyDto)
    {
        updateSurveyDto.Id = id;
        var model = SurveyMapper.MapUpdateDtoToModel(updateSurveyDto);
        var updatedModel = await _surveyRepository.UpdateSurveyAsync(model);
        
        if (updatedModel == null)
            return null;
            
        return SurveyMapper.MapToDto(updatedModel);
    }

    public async Task<bool> DeleteSurveyByIdAsync(Guid surveyId)
    {
        return await _surveyRepository.DeleteSurveyByIdAsync(surveyId);
    }
}
