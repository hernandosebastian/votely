using Xunit;
using Votely.Domain;

public class QuestionTests
{
    [Fact]
    public void Question_ShouldInitializeWithCorrectTitle()
    {
        const string questionTitle = "What is your favorite programming language?";

        var question = new Question(questionTitle, new List<Option>());

        Assert.Equal(questionTitle, question.Title);
    }

    [Fact]
    public void Question_ShouldInitializeWithCorrectOptions()
    {
        const string questionTitle = "What is your favorite programming language?";
        var option1 = new Option("C#");
        var option2 = new Option("Java");
        var options = new List<Option> { option1, option2 };

        var question = new Question(questionTitle, options);

        Assert.Equal(2, question.Options.Count);
        Assert.Contains(option1, question.Options);
        Assert.Contains(option2, question.Options);
    }
}