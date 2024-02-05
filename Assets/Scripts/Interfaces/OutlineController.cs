using UnityEngine;

public class OutlineController : MonoBehaviour
{
  private GameObject _lastOutlined = null;
  private MaterialPropertyBlock _propBlock;

  void Start()
  {
    _propBlock = new MaterialPropertyBlock();
  }

  void Update()
  {
    GameObject hitObject = PerformRaycast();
    UpdateOutline(hitObject);
  }

  public void UpdateOutline(GameObject currentObject)
  {
    if (currentObject != _lastOutlined)
    {
      if (_lastOutlined != null)
      {
        ApplyOutline(_lastOutlined, false);
      }

      if (currentObject != null)
      {
        Item item = currentObject.GetComponent<Item>();
        if (item != null && item.item != null && item.item.isInteractable)
        {
          ApplyOutline(currentObject, true);
          _lastOutlined = currentObject;
          return;
        }
      }
      _lastOutlined = null;
    }
  }

  public GameObject GetCurrentlyOutlinedObject()
  {
    return _lastOutlined;
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

  private void ApplyOutline(GameObject obj, bool shouldOutline)
  {
    Renderer renderer = obj.GetComponent<Renderer>();
    if (renderer != null)
    {
      renderer.GetPropertyBlock(_propBlock);
      _propBlock.SetFloat("_Scale", shouldOutline ? 1.1f : 0f);
      renderer.SetPropertyBlock(_propBlock);
    }
  }
}