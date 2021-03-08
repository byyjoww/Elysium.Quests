using Elysium.Core;
using Elysium.Utils.Attributes;

namespace Elysium.Quests
{
    public class CollectCurrencyObjective : QuestObjective
    {
        [Separator("Settings", true)]
        public LongValueSO currency;
        public int quantity;

        public override float GetObjectiveProgress() => currency.Value / quantity;

        public override void CheckForObjectiveComplete()
        {
            if (currency.Value >= quantity) { SetObjectiveAsComplete(); }
        }

        public override void StartListeningForObjectives()
        {
            currency.OnValueChanged += CheckForObjectiveComplete;
        }

        public override void StopListeningForObjectives()
        {
            currency.OnValueChanged -= CheckForObjectiveComplete;
        }
    }
}