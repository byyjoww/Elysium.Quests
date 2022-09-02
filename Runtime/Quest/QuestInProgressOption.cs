using Elysium.Dialogue;
using UnityEngine;

namespace Elysium.Quests
{
    public class QuestInProgressOption : IQuestOption
    {
        private IQuestFactory factory = default;

        public string Title { get; private set; }
        public Sprite Icon { get; private set; }

        public QuestInProgressOption(IQuestFactory factory)
        {
            this.factory = factory;
            this.Title = $"{factory.Title} (In Progress)";
            this.Icon = factory.Icon;
        }

        public IDialogue Choose()
        {
            return factory.Dialogue.GetInProgressDialogue();
        }
    }
}
