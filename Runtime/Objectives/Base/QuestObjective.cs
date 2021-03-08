using Elysium.Utils.Attributes;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Quests
{
    public abstract class QuestObjective : ScriptableObject
    {
        [Separator("Status", true)]
        [SerializeField, ReadOnly] private bool isComplete = false;

        public event UnityAction OnValueChanged;

        public virtual float Progress
        {
            get
            {
                if (isComplete) { return 1; }
                return GetObjectiveProgress();
            }
        }

        public bool IsObjectiveComplete
        {
            get => isComplete;
            set => isComplete = value;
        }

        public abstract float GetObjectiveProgress();

        public abstract void CheckForObjectiveComplete();

        public void SetObjectiveAsComplete()
        {
            isComplete = true;
            StopListeningForObjectives();
            OnValueChanged?.Invoke();
        }

        public void SetObjectiveAsIncomplete()
        {
            isComplete = false;
            StartListeningForObjectives();
            OnValueChanged?.Invoke();
        }

        public virtual void ForceResetObjective()
        {
            isComplete = false;
        }

        public abstract void StartListeningForObjectives();
        public abstract void StopListeningForObjectives();
    }
}