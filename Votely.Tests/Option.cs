using Xunit;
using Votely.Domain;

namespace Votely.Tests;

public class OptionTests
{
    [Fact]
    public void Option_ShouldInitializeWithCorrectTextAndDefaultVotes()
    {
        const string optionText = "C#";

        var option = new Option(optionText);

        Assert.Equal(optionText, option.Text);
        Assert.Equal(0, option.Votes);
    }
}
