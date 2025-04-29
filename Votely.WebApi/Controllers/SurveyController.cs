using Microsoft.AspNetCore.Mvc;
using Votely.Application.Surveys;
using Votely.Application.Surveys.DTOs;

namespace Votely.WebApi.Controllers;

[ApiController]
[Route("api/surveys")]
public class SurveyController : ControllerBase
{
    private readonly ISurveyService _surveyService;
    private readonly ILogger<SurveyController> _logger;

    public SurveyController(ISurveyService surveyService, ILogger<SurveyController> logger)
    {
        _surveyService = surveyService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSurveys()
    {
        try
        {
            var surveys = await _surveyService.GetAllSurveysAsync();
            return Ok(surveys);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all surveys");
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSurveyById(Guid id)
    {
        try
        {
            var survey = await _surveyService.GetSurveyByIdAsync(id);

            if (survey == null)
                return NotFound(new { error = $"Survey with ID {id} not found" });

            return Ok(survey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving survey with ID {SurveyId}", id);
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateSurvey([FromBody] CreateSurveyDto createSurveyDto)
    {
        try
        {
            var surveyDto = await _surveyService.CreateSurveyAsync(createSurveyDto);
            return CreatedAtAction(nameof(GetSurveyById), new { id = surveyDto.Id }, surveyDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating survey");
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSurveyDto updateSurveyDto)
    {
        try
        {
            var updatedSurvey = await _surveyService.UpdateSurveyByIdAsync(id, updateSurveyDto);
            
            if (updatedSurvey == null)
                return NotFound(new { error = $"Survey with ID {id} not found" });
                
            return Ok(updatedSurvey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating survey with ID {SurveyId}", id);
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSurvey(Guid id)
    {
        try
        {
            var result = await _surveyService.DeleteSurveyByIdAsync(id);

            if (!result)
                return NotFound(new { error = $"Survey with ID {id} not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting survey with ID {SurveyId}", id);
            return StatusCode(500, new { error = ex.Message });
        }
    }
}