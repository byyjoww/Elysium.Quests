using Elysium.Dialogue;
using UnityEngine.Events;

namespace Elysium.Quests
{
    public class CustomQuestOption : IDialogueOption
    {
        public string Title { get; set; }
        public UnityAction OnChoose { get; set; }

        public IDialogue Choose()
        {
            OnChoose?.Invoke();
            return new EndDialogue();
        }
    }
}