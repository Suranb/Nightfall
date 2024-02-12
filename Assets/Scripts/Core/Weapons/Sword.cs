using Assets.Scripts.Interfaces;
using UnityEngine;

public class Sword : Item, IWeapon
{
  [SerializeField] private int Damage = 10;
  [SerializeField] private float attackDelay = 0.5f;
  [SerializeField] private float lastEquipTime;


  public void Interact(PlayerInventory playerInventory)
  {
    if (playerInventory != null)
    {
      playerInventory.AddItem(item);

      if (!playerInventory.isCurrentlyEquippedWeapon)
      {
        EquipWeapon(playerInventory);
        return;
      }
      Destroy(gameObject);
    }
  }
  public bool CanAttack()
  {
    return Time.time > lastEquipTime + attackDelay;
  }

  public void EquipWeapon(PlayerInventory playerInventory)
  {
    Transform weaponPlaceholder = playerInventory.GetWeaponPlaceholder();
    if (weaponPlaceholder != null)
    {
      transform.SetParent(weaponPlaceholder);
      transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
      playerInventory.currentEquippedWeapon = this;
      lastEquipTime = Time.time;
    }
  }

  public void Attack(Animator playerAnimator)
  {
    playerAnimator.SetTrigger("Attack");
  }
}
