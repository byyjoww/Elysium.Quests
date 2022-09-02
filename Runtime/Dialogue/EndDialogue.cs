using System.Collections.Generic;

namespace Elysium.Dialogue
{
    public class EndDialogue : DialogueBase, IDialogue
    {
        public string Text { get; } = string.Empty;
        public IEnumerable<IDialogueOption> Options { get; } = new IDialogueOption[0];
        public bool IsComplete { get; } = true;
    }
}