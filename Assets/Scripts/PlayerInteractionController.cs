using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
  public Transform player;
  public float interactionRange = 2.0f;
  private OutlineController _outlineController;

  void Start()
  {
    _outlineController = FindObjectOfType<OutlineController>();
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
    {
      GameObject hitObject = _outlineController.GetCurrentlyOutlinedObject();
      if (hitObject != null && IsPlayerWithinInteractionRange(hitObject))
      {
        InteractWithObject(hitObject);
      }
    }
  }

  bool IsPlayerWithinInteractionRange(GameObject obj)
  {
    return Vector3.Distance(obj.transform.position, player.position) <= interactionRange;
  }

  void InteractWithObject(GameObject obj)
  {
    // Interaction logic here
    Debug.Log("Interacting with " + obj.name);
  }
}
