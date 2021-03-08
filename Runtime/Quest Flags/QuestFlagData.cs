using UnityEngine;

namespace Elysium.Quests.Flags
{
    [System.Serializable]
    public class QuestFlagData
    {
        [SerializeField] private QuestFlag flag = default;
        [SerializeField] private int quantity = default;

        public QuestFlag Flag
        {
            get => flag;
            set => flag = value;
        }

        public int Quantity
        {
            get => quantity;
            set => quantity = value;
        }

        public QuestFlagData(QuestFlag _flag, int _quantity)
        {
            flag = _flag;
            quantity = _quantity;
        }
    }
}