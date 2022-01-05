using UnityEngine;

public interface Item
{
    int ID { get; }

    string Description { get; }

    bool Use();
    void PickUp();
    Sprite Sprite { get; }
}
