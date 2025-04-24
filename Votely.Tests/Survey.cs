using Xunit;
using Votely.Domain;

namespace Votely.Tests;

public class SurveyTests
{
    [Fact]
    public void Survey_ShouldInitializeWithCorrectTitle()
    {
        const string surveyTitle = "Survey about languages";

        var survey = new Survey(surveyTitle, new List<Question>());

        Assert.Equal(surveyTitle, survey.Title);
    }

    [Fact]
    public void Survey_ShouldInitializeWithQuestions()
    {
        const string surveyTitle = "Survey about languages";
        const string questionTitle = "What is your favorite programming language?";
        var questionOptions = new List<Option>
        {
            new Option("Option 1"),
            new Option("Option 2")
        };
        var question = new Question(questionTitle, questionOptions);

        var survey = new Survey(surveyTitle, new List<Question> { question });

        var addedQuestion = survey.Questions[0];

        Assert.Equal(surveyTitle, survey.Title);
        Assert.Single(survey.Questions);
        Assert.Contains(question, survey.Questions);
        Assert.Equal(questionTitle, addedQuestion.Title);
    }
}