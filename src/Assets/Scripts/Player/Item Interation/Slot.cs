using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    private Consumable item;
    private int itemCount = 0;
    private Text slotDisplayAmount;
    private Image slotImage;
    private GameObject itemsContainer;
    private float onPointerDownTime = 0;

    private void Awake()
    {
        slotDisplayAmount = transform.GetChild(0).GetComponent<Text>();
        itemsContainer = transform.GetChild(1).gameObject;
        slotImage = transform.GetComponent<Image>();
    }

    public bool AddItem(Consumable consumable)
    {
        if (item == null)
        {
            item = consumable;
            itemCount++;
            SetDescripionAndCount();
            SetSprite(consumable);
            ContainItem(consumable);
            return true;
        } else if (item.ID == consumable.ID)
        {
            itemCount++;
            SetDescripionAndCount();
            ContainItem(consumable);
            return true;
        } else
        {
            return false;
        }
    }

    public Consumable GetItem()
    {
        return item;
    }

    public int GetItemCount()
    {
        return itemCount;
    }

    public GameObject GetItemContainer()
    {
        return itemsContainer;
    }

    public void SetItem(Consumable itemToSet)
    {
        item = itemToSet;
        SetSprite(item);
        SetDescripionAndCount();
    }

    public void SetItemCount(int countToSet)
    {
        itemCount = countToSet;
        SetDescripionAndCount();
    }

    public void SetItemContainer(GameObject newItemContainer)
    {
        itemsContainer = newItemContainer;
        if (itemsContainer)
        {
            newItemContainer.transform.SetParent(transform);
        }
    }

    private void SetSprite(Consumable consumable)
    {
        if (consumable)
        {
            Sprite sprite = consumable.Sprite;
            slotImage.sprite = sprite;
        } else
        {
            slotImage.sprite = null;
        }
    }

    private void SetDescripionAndCount()
    {
        if (item)
        {
            slotDisplayAmount.text = item.Description + " - " + itemCount.ToString();
        }
        else
        {
            slotDisplayAmount.text = "0";
        }
    }

    private void ContainItem(Consumable consumable)
    {
        consumable.transform.parent = itemsContainer.transform;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        onPointerDownTime = Time.time;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        float clickFinishTime = Time.time;
        float clickTime = clickFinishTime - onPointerDownTime;

        bool isClicked = clickTime < 0.1;
        bool isSlotTaken = item != null;

        if (isClicked && isSlotTaken)
        {
            item.Use();
        }
    }
}
