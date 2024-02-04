using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
  public Transform player;
  public float interactionRange = 2.0f;

  void Start()
  {
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
    {
      GameObject hitObject = PerformRaycast();
      if (hitObject != null) { Debug.Log($"HitObject: {hitObject.name}"); }

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

  GameObject PerformRaycast()
  {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit))
    {
      return hit.collider.gameObject;
    }
    return null;
  }
}