using UnityEngine;

public class Sword : Item, IInteractable
{
  [SerializeField] private int Damage = 10;

  public void Interact(PlayerInventory playerInventory)
  {
    if (playerInventory != null)
    {
      playerInventory.AddItem(item);

      if (!playerInventory.isCurrentlyEquippedMeleWeapon)
      {
        EquipWeapon(playerInventory);
        return;
      }
      Destroy(gameObject);
    }
  }

  public void EquipWeapon(PlayerInventory playerInventory)
  {
    Transform weaponPlaceholder = playerInventory.GetWeaponPlaceholder();
    if (weaponPlaceholder != null)
    {
      transform.SetParent(weaponPlaceholder);
      transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
      playerInventory.isCurrentlyEquippedMeleWeapon = true;
    }
  }
}
