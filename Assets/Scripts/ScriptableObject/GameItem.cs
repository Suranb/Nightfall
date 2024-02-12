using UnityEngine;

[CreateAssetMenu(fileName = "New GameItem", menuName = "Inventory/GameItem")]
public class GameItem : ScriptableObject
{
  public string ItemName = "New Item";
  public Sprite icon = null;
  public AudioClip interactionSound;
  public ParticleSystem destructionEffect;
}