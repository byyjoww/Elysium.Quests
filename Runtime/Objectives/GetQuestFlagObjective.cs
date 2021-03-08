using UnityEngine;
using Elysium.Quests;
using Elysium.Utils.Attributes;

namespace Elysium.Quests.Flags
{
    public class GetQuestFlagObjective : QuestObjective
    {
        [Separator("Settings", true)]
        [SerializeField] private QuestSystemSO questSystem = default;

        [SerializeField] private QuestFlag flag = default;
        [SerializeField] private int amount = default;

        public override float GetObjectiveProgress() => questSystem.QuestFlagDatabase.Quantity(flag) / amount;

        public override void CheckForObjectiveComplete()
        {
            int i = questSystem.QuestFlagDatabase.Quantity(flag);

            if (i >= amount)
            {
                SetObjectiveAsComplete();
            }
        }

        public override void StartListeningForObjectives()
        {
            questSystem.QuestFlagDatabase.OnFlagChanged += ProcessEvent;
        }

        private void ProcessEvent(QuestFlag _flag, int _prev, int _current)
        {
            if (_flag != flag) { return; }

            CheckForObjectiveComplete();
        }

        public override void StopListeningForObjectives()
        {
            questSystem.QuestFlagDatabase.OnFlagChanged -= ProcessEvent;
        }
    }
}