using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Quests.Flags
{
    [Serializable]
    public class QuestFlagContainer
    {
        [SerializeField] private List<QuestFlagData> questFlags = new List<QuestFlagData>();

        public event UnityAction<QuestFlag, int, int> OnFlagChanged;
        public event UnityAction OnValueChanged;

        public bool Contains(QuestFlag _flag)
        {
            return GetData(_flag) != null;
        }

        public int Quantity(QuestFlag _flag)
        {
            QuestFlagData data = GetData(_flag);
            return data == null ? 0 : data.Quantity;
        }

        public void Add(string _flag, int _quantity)
        {
            if (Enum.TryParse(_flag, out QuestFlag questFlag))
            {
                Add(questFlag, _quantity);
                return;
            }

            throw new Exception($"couldn't parse quest flag \"{_flag}\" to enum");
        }

        public void Add(QuestFlag _flag, int _quantity)
        {
            QuestFlagData data = GetData(_flag);
            int prev = 0;
            int current = 0;

            if (data == null)
            {
                questFlags.Add(new QuestFlagData(_flag, _quantity));
                current = _quantity;
            }
            else
            {
                prev = data.Quantity;
                data.Quantity += _quantity;
                current = data.Quantity;
            }

            OnFlagChanged?.Invoke(_flag, prev, current);
            OnValueChanged?.Invoke();
        }

        public void Remove(QuestFlag _flag, int _quantity)
        {
            QuestFlagData data = GetData(_flag);

            if (data == null)
            {
                Debug.LogError("Player doesn't have this flag!");
                return;
            }

            int prev = data.Quantity;
            data.Quantity -= _quantity;
            int current = data.Quantity;

            if (data.Quantity <= 0)
            {
                questFlags.Remove(data);
                current = 0;
            }

            OnFlagChanged?.Invoke(_flag, prev, current);
            OnValueChanged?.Invoke();
        }

        private QuestFlagData GetData(QuestFlag flag)
        {
            return questFlags.SingleOrDefault(x => x.Flag == flag);
        }
    }
}