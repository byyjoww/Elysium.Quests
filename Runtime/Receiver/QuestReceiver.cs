using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace Elysium.Quests
{
    public class QuestReceiver : IQuestReceiver
    {
        private Dictionary<string, IQuest> quests = new Dictionary<string, IQuest>();
        private IQuestHistory history = new QuestHistory();

        public IReadOnlyDictionary<string, IQuest> Quests => quests;
        public IReadOnlyDictionary<string, List<QuestRecord>> Completed => history.Completed;

        public event UnityAction<IQuest> OnQuestAccepted;
        public event UnityAction<IQuest> OnQuestCompleted;
        public event UnityAction<IQuest> OnQuestDelivered;
        public event UnityAction<IQuest> OnQuestForfeited;

        public IEnumerable<IQuestOption> GetOptions(params IQuestFactory[] questFactories)
        {
            var opts = new List<IQuestOption>();
            foreach (var factory in questFactories)
            {
                bool hasQuest = Quests.TryGetValue(factory.ID, out IQuest quest);

                if (QuestIsComplete(hasQuest, quest))
                {
                    opts.Add(new QuestDeliveryOption(quest, factory, this));
                }

                if (QuestIsInProgress(hasQuest, quest))
                {
                    opts.Add(new QuestInProgressOption(factory));
                }

                if (QuestCanBeAccepted(hasQuest, factory))
                {
                    opts.Add(new QuestRequestOption(factory, this));
                }
            }
            return opts;
        }

        public void Accept(IQuest quest)
        {
            quest.OnComplete += TriggerOnComplete;
            quests.TryAdd(quest.ID, quest);
            quest.Accept();
            OnQuestAccepted?.Invoke(quest);
        }

        public void Deliver(IQuest quest)
        {
            quest.OnComplete -= TriggerOnComplete;
            quests.Remove(quest.ID);
            quest.Deliver();
            AddHistory(quest);
            OnQuestDelivered?.Invoke(quest);
        }

        public void Forfeit(IQuest quest)
        {
            quest.OnComplete -= TriggerOnComplete;
            quests.Remove(quest.ID);
            quest.Forfeit();
            OnQuestForfeited?.Invoke(quest);
        }

        private bool QuestCanBeAccepted(bool hasQuest, IQuestFactory factory)
        {
            return !hasQuest && CanStartQuest(factory);
        }

        private bool QuestIsInProgress(bool hasQuest, IQuest quest)
        {
            return hasQuest && !CanDeliverQuest(quest);
        }

        private bool QuestIsComplete(bool hasQuest, IQuest quest)
        {
            return hasQuest && CanDeliverQuest(quest);
        }

        private bool CanStartQuest(IQuestFactory factory)
        {
            return factory.CanStart(history.CompletedKeys);
        }

        private bool CanDeliverQuest(IQuest quest)
        {
            return quest.IsComplete;
        }

        private void AddHistory(IQuest quest)
        {
            history.Add(new QuestRecord
            {
                Id = quest.ID,
                CompletedAt = System.DateTime.UtcNow
            });
        }

        private void TriggerOnComplete(IQuest quest)
        {
            OnQuestCompleted?.Invoke(quest);
        }
    }
}
