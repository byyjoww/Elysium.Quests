namespace Elysium.Dialogue
{
    public interface IDialogueOption
    {
        string Title { get; }

        IDialogue Choose();
    }

}