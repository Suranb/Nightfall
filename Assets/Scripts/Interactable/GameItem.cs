using UnityEngine;

public class GameItem : MonoBehaviour
{
  public string ItemName = "New Item";
  public Sprite icon = null;
  public bool isDestructible = false;
  public bool isInteractable = true;
  public AudioClip interactionSound;
  public ParticleSystem destructionEffect;
}