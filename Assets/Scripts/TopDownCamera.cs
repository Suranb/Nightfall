using UnityEngine;

public class TopDown_Camera : MonoBehaviour
{
  public Transform target;
  public float height = 10f;
  public float distance = 20f;
  public float angle = 45f;
  public float smoothSpeed = 0.5f;
  private GameObject _lastOutlined = null;
  private MaterialPropertyBlock _propBlock;


  private Vector3 refVelocity;

  // Start is called before the first frame update
  void Start()
  {
    HandleCamera();
    _propBlock = new MaterialPropertyBlock();
  }

  // Update is called once per frame
  void Update()
  {
    HandleCamera();
    GameObject hitObject = PerformRaycast();
    UpdateOutline(hitObject);
  }

  private void HandleCamera()
  {
    if (!target)
    {
      return;
    }

    Vector3 worldPos = (Vector3.forward * distance) + (Vector3.up * height);
    //Debug.DrawLine(target.position, worldPos, Color.red);

    Vector3 rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPos;
    //Debug.DrawLine(target.position, rotatedVector, Color.green);

    Vector3 flatTargetPos = target.position;
    flatTargetPos.y = 0f;
    Vector3 finalPosition = flatTargetPos + rotatedVector;

    //Debug.DrawLine(target.position, finalPosition, Color.blue);

    transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, smoothSpeed);
    transform.LookAt(target.position);
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

  void UpdateOutline(GameObject currentObject)
  {
    // Check if the current object is different from the last outlined object
    if (currentObject != _lastOutlined)
    {
      // Remove outline from the last object
      if (_lastOutlined != null)
      {
        ApplyOutline(_lastOutlined, false);
      }

      // Apply outline to the current object, if it's interactable
      if (currentObject != null && currentObject.GetComponent<GameItem>() != null && currentObject.GetComponent<GameItem>().isInteractable)
      {
        ApplyOutline(currentObject, true);
        _lastOutlined = currentObject; // Update the last outlined object
      }
      else
      {
        _lastOutlined = null; // No current object, so set last outlined to null
      }
    }
  }

  void ApplyOutline(GameObject obj, bool shouldOutline)
  {
    Debug.Log("Apply Outline!");
    Renderer renderer = obj.GetComponent<Renderer>();
    if (renderer != null)
    {
      renderer.GetPropertyBlock(_propBlock);
      _propBlock.SetFloat("_Scale", shouldOutline ? 1.1f : 0f); // Assuming 1f is the default scale
      renderer.SetPropertyBlock(_propBlock);
      Debug.Log("Set Float!");
    }
  }
}