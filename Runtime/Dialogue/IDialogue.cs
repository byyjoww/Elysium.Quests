using System.Collections.Generic;
using UnityEngine.Events;

namespace Elysium.Dialogue
{
    public interface IDialogue
    {
        string Text { get; }
        bool IsComplete { get; }
        IEnumerable<IDialogueOption> Options { get; }
        event UnityAction OnCancel;

        void Cancel();
    }
}