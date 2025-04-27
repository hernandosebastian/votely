using Votely.Infrastructure.Database.Models;
using Votely.Application.Surveys.DTOs;

namespace Votely.Application.Mappers
{
    public static class SurveyMapper
    {
        public static SurveyModel MapToModel(CreateSurveyDto dto)
        {
            var surveyId = Guid.NewGuid();
            var questionModels = dto.Questions.Select(q =>
            {
                var questionId = Guid.NewGuid();
                var optionModels = q.Options.Select(o => new OptionModel
                {
                    OptionId   = Guid.NewGuid(),
                    Text       = o.Text,
                    Votes      = 0,
                    QuestionId = questionId
                }).ToList();

                return new QuestionModel
                {
                    QuestionId = questionId,
                    Title      = q.Title,
                    SurveyId   = surveyId,
                    Options    = optionModels
                };
            }).ToList();

            return new SurveyModel
            {
                SurveyId  = surveyId,
                Title     = dto.Title,
                Questions = questionModels
            };
        }

        public static SurveyModel MapUpdateDtoToModel(UpdateSurveyDto dto)
        {
            var questionModels = dto.Questions.Select(q => new QuestionModel
            {
                QuestionId = q.Id,
                Title = q.Title,
                SurveyId = dto.Id,
                Options = q.Options.Select(o => new OptionModel
                {
                    OptionId = o.Id,
                    Text = o.Text,
                    Votes = o.Votes,
                    QuestionId = q.Id
                }).ToList()
            }).ToList();

            return new SurveyModel
            {
                SurveyId = dto.Id,
                Title = dto.Title,
                Questions = questionModels
            };
        }

        public static SurveyDto MapToDto(SurveyModel model)
        {
            return new SurveyDto
            {
                Id        = model.SurveyId,
                Title     = model.Title,
                Questions = model.Questions.Select(qm => new QuestionDto
                {
                    Id      = qm.QuestionId,
                    Title   = qm.Title,
                    Options = qm.Options.Select(om => new OptionDto
                    {
                        Id    = om.OptionId,
                        Text  = om.Text,
                        Votes = om.Votes
                    }).ToList()
                }).ToList()
            };
        }
    }
}
