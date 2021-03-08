using System.Linq;
using UnityEngine;

namespace Elysium.Quests
{
    public class QuestRewards : MonoBehaviour
    {
        [SerializeField] private QuestSystemSO questSystem = default;
        [SerializeField] private QuestRewardData[] questRewardTable = new QuestRewardData[0];

        private void Awake()
        {
            questSystem.OnQuestCollected += HandleQuestRewards;
            questSystem.Init();
        }

        private void HandleQuestRewards(QuestSO _collectedQuest)
        {
            QuestRewardData rewardTable = questRewardTable.SingleOrDefault(x => x.Quest == _collectedQuest);
            if (rewardTable == null)
            {
                Debug.LogError($"no reward set for quest {_collectedQuest.Title}");
                return;
            }

            Debug.Log($"Delivering Rewards: CURRENCY={rewardTable.CurrencyReward} | EXPERIENCE={rewardTable.ExperienceReward}");
        }
    }

    [System.Serializable]
    public class QuestRewardData
    {
        [SerializeField] private QuestSO quest = default;
        [SerializeField] [Range(0, 10000)] private int currencyReward = default;
        [SerializeField] [Range(0, 1000)] private int experienceReward = default;

        public QuestSO Quest
        {
            get => quest;
            set => quest = value;
        }

        public int CurrencyReward
        {
            get => currencyReward;
            set => currencyReward = value;
        }

        public int ExperienceReward
        {
            get => experienceReward;
            set => experienceReward = value;
        }
    }
}
