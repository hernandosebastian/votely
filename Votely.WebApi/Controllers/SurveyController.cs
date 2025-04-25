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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSurvey(Guid id)
    {
        try
        {
            var result = await _surveyService.GetSurveyByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSurveys()
    {
        var result = await _surveyService.GetAllSurveysAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSurvey([FromBody] CreateSurveyDto dto)
    {
        var result = await _surveyService.CreateSurveyAsync(dto);
        return CreatedAtAction(nameof(GetSurvey), new { id = result.Id }, result);
    }

    [HttpPost("{surveyId}/questions")]
    public async Task<IActionResult> AddQuestion(Guid surveyId, [FromBody] CreateQuestionDto dto)
    {
        var result = await _surveyService.AddQuestionAsync(surveyId, dto);
        return Ok(result);
    }

    [HttpPost("{surveyId}/questions/{questionId}/options")]
    public async Task<IActionResult> AddOption(Guid surveyId, Guid questionId, [FromBody] CreateOptionDto dto)
    {
        var result = await _surveyService.AddOptionAsync(surveyId, questionId, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSurvey(Guid id)
    {
        try
        {
            await _surveyService.DeleteSurveyAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("{surveyId}/questions/{questionId}/options/{optionId}/vote")]
    public async Task<IActionResult> Vote(Guid surveyId, Guid questionId, Guid optionId)
    {
        try
        {
            await _surveyService.VoteOptionAsync(surveyId, questionId, optionId);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}
