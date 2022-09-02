using System.Collections.Generic;

namespace Elysium.Quests
{
    public interface IQuestGiver
    {
        IEnumerable<IQuestOption> Interact(IQuestReceiver receiver);
    }
}
