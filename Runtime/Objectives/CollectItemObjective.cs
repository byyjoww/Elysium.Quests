using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Elysium.Utils.Attributes;

namespace Elysium.Quests
{
    //public class CollectItemObjective : QuestObjective
    //{
    //    [Separator("Settings", true)]
    //    [SerializeField] private Inventory inventory;

    //    [RequireInterface(typeof(IInventoryElement))] 
    //    public ScriptableObject requiredItem;

    //    public IInventoryElement RequiredItem => requiredItem as IInventoryElement;
    //    public int requiredQuantity;

    //    private void CheckRequiredItems()
    //    {
    //        var itemQuantity = inventory.CheckItemAmount(requiredItem as IInventoryElement);
    //        if (itemQuantity >= requiredQuantity) { CompleteObjective(); }
    //    }

    //    public override void CheckObjective()
    //    {
    //        CheckRequiredItems();
    //    }

    //    public void OnEnable()
    //    {
    //        inventory.OnValueChanged += CheckRequiredItems;        
    //        // Debug.Log("CheckRequiredItems subscribed to OnInventoryChanged.");        
    //    }

    //    public void OnDisable()
    //    {
    //        inventory.OnValueChanged -= CheckRequiredItems;
    //        // Debug.Log("CheckRequiredItems unsubscribed from OnInventoryChanged.");
    //    }
    //}
}