using Elysium.Utils;
using Elysium.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Quests
{
    [CreateAssetMenu(fileName = "QuestSO", menuName = "Scriptable Objects/Quests/Quest")]
    public class QuestSO : AssetContainer<QuestObjective>
    {
        [Separator("Settings", true)]
        [SerializeField] private string title = default;
        [SerializeField] private string description = default;
        [SerializeField] private bool isAutoComplete = default;
        [SerializeField] private bool isObtainedByDefault = default;

        [Separator("State", true)]
        [SerializeField] private State currentState = State.INACTIVE;

        [Separator("Events", true)]
        public QuestStateChangeUnityEvent OnQuestStateChanged;
        public event UnityAction OnValueChanged;

        public string Title => title;
        public string Description => description;
        public bool IsActive => (int)currentState == 1;
        public State CurrentState => currentState;
        public List<QuestObjective> Objectives => Nodes;

        public enum State
        {
            INACTIVE = 0,
            IN_PROGRESS = 1,
            COMPLETE = 2,
            COLLECTED = 3,
        }

        public void StartQuest() 
        {
            if (currentState != State.INACTIVE)
            {
                Debug.Log($"Trying to start non-inactive quest {title} | State: {currentState}.");
                return;
            }

            TransitionToState(State.IN_PROGRESS);
            StartListening();
        }

        public void ForceCompleteQuest()
        {
            if (!IsActive)
            {
                Debug.Log($"Trying to force complete unstarted quest {title} | State: {currentState}.");
                return; 
            }

            foreach (var objective in Objectives)
            {
                objective.SetObjectiveAsComplete();
            }
        }

        public void CompleteQuest() 
        {
            if (currentState != State.IN_PROGRESS)
            {
                Debug.Log($"Trying to complete unstarted quest {title} | State: {currentState}.");
                return;
            }

            StopListening();
            TransitionToState(State.COMPLETE);

            if (isAutoComplete) { CollectQuest(); }
        }

        public void CollectQuest()
        {
            if (currentState != State.COMPLETE)
            {
                Debug.Log($"Trying to collect uncomplete quest {title} | State: {currentState}.");
                return;
            }

            TransitionToState(State.COLLECTED);
        }

        public void ResetQuest()
        {
            if (currentState != State.COLLECTED)
            {
                Debug.Log($"Trying to reset uncollected quest {title} | State: {currentState}.");
                return;
            }

            foreach (var objective in Objectives)
            {
                objective.ForceResetObjective();
            }

            TransitionToState(State.INACTIVE);    
        }

        public void StartListening()
        {
            if ((int)currentState > 1) 
            {
                Debug.LogError($"Trying to listen to completed or collected quest");
                return; 
            }

            Debug.Log($"Quest {title} has started listening for objectives.");
            foreach (var objective in Objectives)
            {
                objective.OnValueChanged += TriggerOnValueChanged;
                objective.OnValueChanged += CheckForAllObjectivesComplete;

                objective.StartListeningForObjectives();
                objective.CheckForObjectiveComplete();
            }
        }

        public void StopListening()
        {
            foreach (var objective in Objectives)
            {
                objective.OnValueChanged -= TriggerOnValueChanged;
                objective.OnValueChanged -= CheckForAllObjectivesComplete;

                objective.StopListeningForObjectives();
            }

            Debug.Log($"Quest {title} has stopped listening for objectives.");
        }

        private void TransitionToState(State _new)
        {
            var prev = currentState;
            if (prev == _new) { return; }
            currentState = _new;
            Debug.Log($"Transitioning quest {Title} from state {prev} to {_new}.");
            OnQuestStateChanged?.Invoke(this, _new);
            TriggerOnValueChanged();
        }

        private void CheckForAllObjectivesComplete()
        {
            foreach (var objective in Objectives)
            {
                if (!objective.IsObjectiveComplete)
                {
                    return;
                }
            }

            Debug.Log($"All conditions for quest \"{title}\" have been met.");
            CompleteQuest();
        }

        private void TriggerOnValueChanged() => OnValueChanged?.Invoke();

        // ----------------------------------- ISAVABLE  ----------------------------------- //
        public ushort Size
        {
            get
            {
                int s = 0;
                foreach (var o in Objectives) { s++; }
                return (ushort)(sizeof(int) + sizeof(bool) * s);
            }
        }

        public void Load(BinaryReader reader)
        {
            int state = reader.ReadInt32();
            currentState = (State)state;

            foreach (var o in Objectives)
            {
                o.IsObjectiveComplete = reader.ReadBoolean();
            }
        }

        public void LoadDefault()
        {
            ResetQuest();

            foreach (var o in Objectives)
            {
                o.IsObjectiveComplete = false;
                // Debug.LogError($"{name} loading default objective status as {o.IsObjectiveComplete}");
            }

            if (isObtainedByDefault) { StartQuest(); }
            // Debug.LogError($"{name} loading default obtained status as {isObtained}");
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write((int)currentState);
            // Debug.LogError($"{name} obtained status saved as {isObtained}");

            foreach (var o in Objectives)
            {
                writer.Write(o.IsObjectiveComplete);
                // Debug.LogError($"{name} objective status saved as {o.IsObjectiveComplete}");
            }
        }
    }

    [System.Serializable]
    public class QuestDatabase : Database<QuestSO> { }

    [System.Serializable]
    public class QuestStateChangeUnityEvent : UnityEvent<QuestSO, QuestSO.State> { }
}

#if UNITY_EDITOR
namespace Elysium.Quests
{
    using Elysium.Utils;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(QuestSO))]
    public class ScriptableQuestEditor : AssetContainerEditor<QuestObjective>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            QuestSO scriptableQuest = (QuestSO)target;

            serializedObject.Update();

            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Complete Quest"))
            {
                scriptableQuest.ForceCompleteQuest();
            }

            if (GUILayout.Button("Turn In Quest"))
            {
                scriptableQuest.CollectQuest();
            }

            if (GUILayout.Button("Reset Quest"))
            {
                scriptableQuest.ResetQuest();
            }

            GUILayout.EndHorizontal();
        }
    }
}
#endif