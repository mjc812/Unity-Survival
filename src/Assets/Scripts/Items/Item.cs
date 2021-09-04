using UnityEngine;

public interface Item
{
    int ID { get; }

    string Description { get; }

    void Use();
    void PickUp();
    Sprite Sprite { get; }
}
