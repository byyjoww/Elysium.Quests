using Elysium.Core;
using Elysium.Utils;
using Elysium.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Elysium.Quests.Flags;

namespace Elysium.Quests
{
    [CreateAssetMenu(fileName = "QuestSystemSO", menuName = "Scriptable Objects/Quests/Quest System")]
    public class QuestSystemSO : ScriptableObject, IInitializable
    {
        [Separator("Quest Database", true)]
        [SerializeField] private QuestDatabase questDatabase;

        [Separator("Quest Flags Database", true)]
        [SerializeField] private QuestFlagContainer questFlagDatabase;

        public event UnityAction<QuestSO> OnQuestStarted;
        public event UnityAction<QuestSO> OnQuestCompleted;
        public event UnityAction<QuestSO> OnQuestCollected;
        public event UnityAction OnValueChanged;

        public bool Initialized { get; set; }
        public QuestSO[] AllQuests => questDatabase.Elements;
        public QuestFlagContainer QuestFlagDatabase => questFlagDatabase;

        public void Init()
        {
            if (Initialized) 
            { 
                Debug.LogError("Trying to initialize quest system more than once");
                return; 
            }

            foreach (var quest in questDatabase.Elements)
            {
                quest.OnQuestStateChanged.AddListener(OnQuestStateChanged);
                if (quest.IsActive) { SilentlyStartQuest(quest); }                
            }

            Initialized = true;
        }

        public void End()
        {
            if (!Initialized)
            {
                Debug.LogError("Trying to end non-initialized quest system");
                return;
            }

            foreach (var quest in questDatabase.Elements)
            {
                quest.StopListening();
            }

            Initialized = false;
        }

        public void StartQuest(QuestSO _quest)
        {
            Debug.Log($"Player received the quest: {_quest.Title}.");
            _quest.StartQuest();
            OnQuestStarted?.Invoke(_quest);
        }

        public void CollectQuest(QuestSO _quest)
        {
            Debug.Log($"Player is trying to collect the quest: {_quest.Title}.");
            _quest.CollectQuest();
        }

        public bool IsActive(QuestSO _quest)
        {
            return _quest.IsActive;
        }

        public QuestSO[] GetAllQuestsInState(QuestSO.State _state)
        {
            return questDatabase.Elements.Where(x => x.CurrentState == _state).ToArray();
        }

        private void SilentlyStartQuest(QuestSO _quest)
        {            
            _quest.StartListening();
        }

        private void OnQuestStateChanged(QuestSO _quest, QuestSO.State _state)
        {
            // Don't add the IN_PROGRESS state here so that the SilentlyStartQuest() method
            // doesn't raise the event upon loading an already active quest between sessions

            if (_state == QuestSO.State.COMPLETE) { OnQuestCompleted?.Invoke(_quest); }
            if (_state == QuestSO.State.COLLECTED) { OnQuestCollected?.Invoke(_quest); }

            OnValueChanged?.Invoke();
        }

        private void OnValidate()
        {
            questDatabase.Refresh();
        }
    }
}