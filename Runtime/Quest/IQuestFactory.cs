using System.Collections.Generic;

namespace Elysium.Quests
{
    public interface IQuestFactory : IQuestInfo
    {
        IQuestDialogueFactory Dialogue { get; }

        bool CanStart(ISet<string> completedQuests);
        IQuest Create();
    }
}