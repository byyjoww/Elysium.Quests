using System.Collections.Generic;

namespace Elysium.Dialogue
{
    public class CustomDialogue : DialogueBase, IDialogue
    {
        public string Text { get; set; }
        public IEnumerable<IDialogueOption> Options { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}