using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDragAndDrop : MonoBehaviour
{
    Slot donorSlot;
    Slot recipientSlot;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))   // should be moved to mouse handler so only goes off when UI is open
        {
            donorSlot = FindSlot();
        } else if (Input.GetMouseButton(0))
        {
            // place image below mouse
        } else if (Input.GetMouseButtonUp(0)) // code still running when its just a click
        {
            recipientSlot = FindSlot();

            if (!recipientSlot)
            {
                PerformDrop();
            } else if(!recipientSlot.GetItem())
            {
                PerformMove();
            } else if(recipientSlot.GetItem().ID != donorSlot.GetItem().ID)
            {
                PerformSwap();
            }
        }
    }

    private void PerformDrop()
    {
        Debug.Log("dropped");
    }

    private void PerformMove()
    {
        recipientSlot.SetItem(donorSlot.GetItem());
        recipientSlot.SetItemCount(donorSlot.GetItemCount());
        recipientSlot.SetItemContainer(donorSlot.GetItemContainer());

        donorSlot.SetItem(null);
        donorSlot.SetItemCount(0);
        donorSlot.SetItemContainer(null);
    }

    private void PerformSwap()
    {
        Consumable recipientConsumable = recipientSlot.GetItem();
        int recipientItemCount = recipientSlot.GetItemCount();
        GameObject recipientItemCountainer = recipientSlot.GetItemContainer();

        recipientSlot.SetItem(donorSlot.GetItem());
        recipientSlot.SetItemCount(donorSlot.GetItemCount());
        recipientSlot.SetItemContainer(donorSlot.GetItemContainer());

        donorSlot.SetItem(recipientConsumable);
        donorSlot.SetItemCount(recipientItemCount);
        donorSlot.SetItemContainer(recipientItemCountainer);
    }

    private Slot FindSlot()
    {
        GraphicRaycaster graphicRaycaster = GetComponent<GraphicRaycaster>();
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);

        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();

        graphicRaycaster.Raycast(pointerEventData, raycastResults);

        foreach(RaycastResult result in raycastResults)
        {
            if(result.gameObject.tag == "Slot")
            {
                return result.gameObject.GetComponent<Slot>(); ;
            }
        }
        return null;
    }
}
