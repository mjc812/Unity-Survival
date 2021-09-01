using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    List<Consumable> items = new List<Consumable>();
    Text slotDisplayAmount;
    
    private void Awake()
    {
        slotDisplayAmount = transform.GetChild(0).GetComponent<Text>();
    }

    public bool AddItem(Consumable consumable)
    {
        if (items.Count == 0)
        {
            items.Add(consumable);
            slotDisplayAmount.text = consumable.Description + " - " + items.Count().ToString();
            return true;
        } else if (items[0].ID == consumable.ID)
        {
            items.Add(consumable);
            slotDisplayAmount.text = consumable.Description + " - " + items.Count().ToString();
            return true;
        } else
        {
            return false;
        }
    }

    /*public int GetContent()
    {
        
    }*/
}
