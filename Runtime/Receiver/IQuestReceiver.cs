using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace Elysium.Quests
{
    public interface IQuestReceiver
    {
        IReadOnlyDictionary<string, IQuest> Quests { get; }
        IReadOnlyDictionary<string, List<QuestRecord>> Completed { get; }

        event UnityAction<IQuest> OnQuestAccepted;
        event UnityAction<IQuest> OnQuestCompleted;
        event UnityAction<IQuest> OnQuestDelivered;
        event UnityAction<IQuest> OnQuestForfeited;

        IEnumerable<IQuestOption> GetOptions(params IQuestFactory[] questFactories);
        void Accept(IQuest quest);
        void Deliver(IQuest quest);
    }
}
