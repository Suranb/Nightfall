using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
  public Transform player;
  public float interactionRange = 2.0f;
  [SerializeField] private PlayerInventory _playerInventory;

  void Start()
  {
    _playerInventory = GetComponent<PlayerInventory>();
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
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
    if (obj == null) return;

    var interactable = obj.GetComponent<IInteractable>();

    if (interactable != null)
    {
      interactable.Interact(_playerInventory);
    }
    else
    {
      Debug.Log($"{obj.name} is not interactable.");
    }
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