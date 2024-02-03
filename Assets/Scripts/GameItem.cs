using UnityEngine;

public class GameItem : MonoBehaviour, IInteractable
{
  public bool isDestructible;
  public bool isInteractable;
  public AudioClip interactionSound;
  public ParticleSystem destructionEffect;

  public void Interact()
  {

  }
}