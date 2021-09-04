using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    List<Consumable> items = new List<Consumable>();
    private Text slotDisplayAmount;
    private Image slotImage;
    private float onPointerDownTime = 0;

    private void Awake()
    {
        slotDisplayAmount = transform.GetChild(0).GetComponent<Text>();
        slotImage = transform.GetComponent<Image>();
    }

    public bool AddItem(Consumable consumable)
    {
        if (items.Count == 0)
        {
            items.Add(consumable);
            slotDisplayAmount.text = consumable.Description + " - " + items.Count().ToString();
            SetSprite(consumable);
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

    private void SetSprite(Consumable consumable)
    {
        Sprite sprite = consumable.Sprite;
        slotImage.sprite = sprite;
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
        bool isSlotTaken = items.Any();

        if (isClicked && isSlotTaken)
        {
            items.First().Use();
        }
    }
}
