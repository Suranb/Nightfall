using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
  [SerializeField] private List<GameItem> inventory = new List<GameItem>();

  public void AddItem(GameItem item)
  {
    if (!inventory.Contains(item))
    {
      inventory.Add(item);
      Debug.Log("Added item: " + item.name);
    }
  }

  public void RemoveItem(GameItem item)
  {
    if (inventory.Contains(item))
    {
      inventory.Remove(item);
      Debug.Log("Removed item: " + item.name);
    }
  }

  // Additional methods like DisplayInventory, CheckForItem, etc., can be added here.
}
