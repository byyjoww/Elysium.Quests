using System.Collections.Generic;
using Elysium.Dialogue;
using UnityEngine;

namespace Elysium.Quests
{
    [System.Serializable]
    public class QuestDialogueFactory : IQuestDialogueFactory
    {
        public IQuestDialogue GetAcceptDialogue(IQuestFactory factory, IQuestReceiver receiver)
        {
            return new CustomQuestDialogue
            {
                Text = "Do you want to accept this quest?",
                IsComplete = false,
                Options = new List<IDialogueOption>()
                {
                    new CustomQuestOption { Title = "Yes", OnChoose = () => receiver.Accept(factory.Create()) },
                    new CustomQuestOption { Title = "No", OnChoose = null },
                }
            };
        }

        public IQuestDialogue GetInProgressDialogue()
        {
            return new CustomQuestDialogue
            {
                Text = "Come back to me when you have completed your quest.",
                IsComplete = false,
                Options = new List<IDialogueOption>()
                {
                    new CustomQuestOption { Title = "Continue", OnChoose = null },
                }
            };
        }

        public IQuestDialogue GetDeliveryDialogue(IQuest quest, IQuestReceiver receiver)
        {
            return new CustomQuestDialogue
            {
                Text = "Thank you for completing this quest!",
                IsComplete = false,
                Options = new List<IDialogueOption>()
                {
                    new CustomQuestOption { Title = "Continue", OnChoose = () => receiver.Deliver(quest) },
                }
            };
        }
    }
}
