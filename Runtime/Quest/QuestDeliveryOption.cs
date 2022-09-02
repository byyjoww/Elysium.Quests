using Elysium.Dialogue;
using UnityEngine;

namespace Elysium.Quests
{
    public class QuestDeliveryOption : IQuestOption
    {
        private IQuest quest = default;
        private IQuestFactory factory = default;
        private IQuestReceiver receiver = default;

        public string Title { get; private set; }
        public Sprite Icon { get; private set; }

        public QuestDeliveryOption(IQuest quest, IQuestFactory factory, IQuestReceiver receiver)
        {
            this.quest = quest;
            this.factory = factory;
            this.receiver = receiver;
            this.Title = $"{factory.Title} (Complete)";
            this.Icon = factory.Icon;
        }

        public IDialogue Choose()
        {
            return factory.Dialogue.GetDeliveryDialogue(quest, receiver);
        }
    }
}
