using System.Collections.Generic;
using UnityEngine;
using Elysium.Core.Utils;

namespace Elysium.Quests
{
    public interface IQuestChain
    {
        string ID { get; }
        string Title { get; }
        Sprite Icon { get; }
        string Description { get; }
        Percentage Progress { get; }
        IEnumerable<IQuest> Quests { get; }
        IEnumerable<IQuestChain> Requirements { get; }
    }
}
