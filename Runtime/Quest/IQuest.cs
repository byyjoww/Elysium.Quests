using Elysium.Core.Utils;
using UnityEngine.Events;

namespace Elysium.Quests
{   
    public interface IQuest : IQuestInfo
    {
        Percentage Progress { get; }
        bool IsComplete { get; }

        event UnityAction<IQuest> OnComplete;

        void Accept();
        void Complete();
        void Deliver();
        void Forfeit();
    }
}