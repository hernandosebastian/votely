using Microsoft.AspNetCore.Mvc;
using Votely.Application.Surveys;
using Votely.Application.Surveys.DTOs;

namespace Votely.WebApi.Controllers;

[ApiController]
[Route("api/surveys")]
public class SurveyController : ControllerBase
{
    private readonly ISurveyService _surveyService;

    public SurveyController(ISurveyService surveyService)
    {
        _surveyService = surveyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSurveys()
    {
        var surveys = await _surveyService.GetAllSurveysAsync();
        return Ok(surveys);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSurveyById(Guid id)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(id);

        if (survey == null)
            return NotFound();

        return Ok(survey);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSurvey([FromBody] CreateSurveyDto createSurveyDto)
    {
        var surveyDto = await _surveyService.CreateSurveyAsync(createSurveyDto);

        return CreatedAtAction(nameof(CreateSurvey), new { id = surveyDto.Id }, surveyDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSurveyDto updateSurveyDto)
    {
        var updatedSurvey = await _surveyService.UpdateSurveyByIdAsync(id, updateSurveyDto);
        
        if (updatedSurvey == null)
            return NotFound();
            
        return Ok(updatedSurvey);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSurvey(Guid id)
    {
        var result = await _surveyService.DeleteSurveyByIdAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}