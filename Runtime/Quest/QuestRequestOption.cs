using Elysium.Dialogue;
using UnityEngine;

namespace Elysium.Quests
{
    public class QuestRequestOption : IQuestOption
    {
        private IQuestFactory factory = default;
        private IQuestReceiver receiver = default;

        public string Title { get; private set; }
        public Sprite Icon { get; private set; }

        public QuestRequestOption(IQuestFactory factory, IQuestReceiver receiver)
        {
            this.factory = factory;
            this.receiver = receiver;
            this.Title = $"{factory.Title}";
            this.Icon = factory.Icon;
        }

        public IDialogue Choose()
        {
            return factory.Dialogue.GetAcceptDialogue(factory, receiver);
        }
    }
}
