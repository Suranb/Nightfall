using UnityEngine;

public class TopDown_Camera : MonoBehaviour
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
}