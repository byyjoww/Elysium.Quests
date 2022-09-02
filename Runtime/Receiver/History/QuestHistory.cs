using System.Collections.Generic;

namespace Elysium.Quests
{
    public class QuestHistory : IQuestHistory
    {
        private Dictionary<string, List<QuestRecord>> completed = new Dictionary<string, List<QuestRecord>>();
        public ISet<string> completedKeys = new HashSet<string>();

        public IReadOnlyDictionary<string, List<QuestRecord>> Completed => completed;
        public ISet<string> CompletedKeys => completedKeys;

        public void Add(QuestRecord record)
        {
            AddToCompletedKeys(record);
            AddToCompleted(record);
        }

        private void AddToCompletedKeys(QuestRecord record)
        {
            if (!completedKeys.Contains(record.Id))
            {
                completedKeys.Add(record.Id);
            }
        }

        private void AddToCompleted(QuestRecord record)
        {
            if (completed.TryGetValue(record.Id, out List<QuestRecord> records))
            {
                records.Add(record);
                return;
            }

            completed.Add(record.Id, new List<QuestRecord>() { record });
        }
    }
}
