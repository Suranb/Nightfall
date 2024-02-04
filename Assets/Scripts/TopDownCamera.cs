using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
  public Transform target;
  public float height = 10f;
  public float distance = 20f;
  public float angle = 45f;
  public float smoothSpeed = 0.5f;

  private Vector3 refVelocity;

  // Start is called before the first frame update
  void Start()
  {
    HandleCamera();
  }

  // Update is called once per frame
  void Update()
  {
    HandleCamera();
  }

  private void HandleCamera()
  {
    if (!target)
    {
      return;
    }

    Vector3 worldPos = (Vector3.forward * -distance) + (Vector3.up * height);
    Vector3 rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPos;
    Vector3 flatTargetPosition = target.position;
    flatTargetPosition.y = 0f; // Keep the camera's target at the same y-level as the target
    Vector3 finalPosition = flatTargetPosition + rotatedVector;

    transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, smoothSpeed);
    transform.LookAt(target.position);
  }
}
