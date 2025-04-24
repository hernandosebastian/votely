namespace Votely.Domain;

public class Option
{
    public const int INITIAL_VOTES = 0;

    public Guid Id { get; set; }
    public string Text { get; set; }
    public int Votes { get; set; }

    public Option(string text)
    {
        Id = Guid.NewGuid();
        Text = text;
        Votes = INITIAL_VOTES;
    }
}
