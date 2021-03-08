using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Elysium.Core;
using Elysium.Quests;

namespace Elysium.Quests
{
    public class RemoveCurrencyObjective : CollectCurrencyObjective
    {
        public override void CheckForObjectiveComplete()
        {
            //if (currency.SpendCurrency(quantity))
            //{
            //    CompleteObjective();
            //}

            if (currency.Value >= quantity)
            {
                currency.Value -= quantity;
                SetObjectiveAsComplete();
            }
        }
    }
}