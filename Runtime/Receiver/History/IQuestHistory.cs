using System.Collections.Generic;

namespace Elysium.Quests
{
    public interface IQuestHistory
    {
        IReadOnlyDictionary<string, List<QuestRecord>> Completed { get; }
        ISet<string> CompletedKeys { get; }

        void Add(QuestRecord record);
    }
}
