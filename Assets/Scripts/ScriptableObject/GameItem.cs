using UnityEngine;

[CreateAssetMenu(fileName = "New GameItem", menuName = "Inventory/GameItem")]
public class GameItem : ScriptableObject
{
  public string ItemName = "New Item";
  public Sprite icon = null;
  public bool isDestructible = false;
  public bool isInteractable = true;
  public AudioClip interactionSound;
  public ParticleSystem destructionEffect;
}