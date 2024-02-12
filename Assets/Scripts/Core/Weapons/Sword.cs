using UnityEngine;

public class Sword : Item, IInteractable
{
  [SerializeField] private int Damage = 10;

  public void Interact(PlayerInventory playerInventory)
  {
    if (playerInventory != null)
    {
      playerInventory.AddItem(item);
      Destroy(gameObject);
    }
  }
}
