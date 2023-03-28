using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryConditions : Condition
{
	[SerializeField] ItemData itemData;

    public override bool IsTrue(GameObject interact)
    {
        if (interact.TryGetComponent<Inventory>(out Inventory inventory))
        {
            if (inventory.Contains(itemData)) return true;
        }

        return false;
    }
}
