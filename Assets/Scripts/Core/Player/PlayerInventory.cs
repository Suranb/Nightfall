using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
  [SerializeField] private List<GameItem> inventory = new();
  [SerializeField] private Transform weaponPlaceholder;
  public bool isCurrentlyEquippedMeleWeapon = false;

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

  public Transform GetWeaponPlaceholder()
  {
    return weaponPlaceholder;
  }

  public bool HasWeaponEquipped()
  {
    return weaponPlaceholder.childCount > 0;
  }

  // DisplayInventory,
  // CheckForItem
}
