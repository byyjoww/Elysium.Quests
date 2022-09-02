using Elysium.Dialogue;
using UnityEngine;

namespace Elysium.Quests
{
    public interface IQuestOption : IDialogueOption
    {
        Sprite Icon { get; }
    }
}
