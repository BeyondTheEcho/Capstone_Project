using UnityEngine;

public interface IDropable
{
    GameObject gameObject { get; }

    void OnDrop(Player player);
    void AddDropableToList();
    void RemoveDropableFromList();
}
