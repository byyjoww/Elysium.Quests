using UnityEngine;
using Elysium.Core.Utils;
using UnityEngine.Events;

namespace Elysium.Quests
{
    public class Quest : IQuest
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Sprite Icon { get; set; }

        public bool IsComplete { get; private set; }
        public Percentage Progress => Percentage.Full;

        public event UnityAction<IQuest> OnComplete;

        void IQuest.Accept()
        {
            Debug.Log($"Quest {Title} was started");
        }

        void IQuest.Complete()
        {
            if (IsComplete) { return; }
            Debug.Log($"Quest {Title} was completed");
            IsComplete = true;
            OnComplete?.Invoke(this);
        }

        void IQuest.Deliver()
        {
            if (!IsComplete) { return; }
            Debug.Log($"Quest {Title} was delivered");
        }

        void IQuest.Forfeit()
        {
            Debug.Log($"Quest {Title} was forfeited");
        }
    }
}
