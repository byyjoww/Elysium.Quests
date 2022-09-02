using UnityEngine.Events;

namespace Elysium.Dialogue
{
    public abstract class DialogueBase
    {
        public event UnityAction OnCancel;

        public virtual void Cancel()
        {
            OnCancel?.Invoke();
        }
    }
}