
public interface Item
{
    int ID { get; }

    string Description { get; }

    void PickUp();
    void Use();
}
