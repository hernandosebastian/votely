namespace Votely.Domain;

public class Option
{
    public Guid OptionId { get; private set; }
    public string Text { get; private set; }
    public int Votes { get; private set; }

    public Option(string text, int votes = 0)
        : this(Guid.NewGuid(), text, votes)
    {
    }

    public Option(Guid optionId, string text, int votes = 0)
    {
        OptionId = optionId;
        Text = text;
        Votes = votes;
    }

    public void Vote()
    {
        Votes++;
    }
}
