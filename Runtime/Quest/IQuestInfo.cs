using UnityEngine;

namespace Elysium.Quests
{
    public interface IQuestInfo
    {
        string ID { get; }
        string Title { get; }
        string Description { get; }
        Sprite Icon { get; }
    }
}