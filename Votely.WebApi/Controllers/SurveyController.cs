using Votely.Application.Survey.Services;
using Microsoft.AspNetCore.Mvc;

namespace Votely.WebApi.Controllers;

[ApiController]
[Route("api/survey")]
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
            return Problem(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateSurvey([FromBody] CreateSurveyDto dto)
    {
        try
        {
            var result = await _surveyService.CreateSurveyAsync(dto);
            return Ok(result);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost("{surveyId}/question")]
    public async Task<IActionResult> AddQuestion(Guid surveyId, [FromBody] CreateQuestionDto dto)
    {
        try
        {
            var result = await _surveyService.AddQuestionAsync(surveyId, dto);
            return Ok(result);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost("{surveyId}/question/{questionId}/option")]
    public async Task<IActionResult> AddOption(Guid surveyId, Guid questionId, [FromBody] CreateOptionDto dto)
    {
        try
        {
            var result = await _surveyService.AddOptionAsync(surveyId, questionId, dto);
            return Ok(result);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}
