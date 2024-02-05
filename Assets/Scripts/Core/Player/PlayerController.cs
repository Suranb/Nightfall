using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private float _moveSpeed = 10.0f;
  [SerializeField] private float _rotationSpeed = 2.0f;
  private Rigidbody _playerRigidbody;
  private Camera _mainCamera;
  private Animator _animator;

  void Start()
  {
    _playerRigidbody = GetComponent<Rigidbody>();
    _mainCamera = Camera.main;
    _animator = GetComponent<Animator>();
  }

  void Update()
  {
    HandleMovement();
    HandleRotation();
    UpdateAnimation();
  }

  private void HandleMovement()
  {
    float moveHorizontal = Input.GetAxisRaw("Horizontal");
    float moveVertical = Input.GetAxisRaw("Vertical");

    Vector3 movementInput = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;
    Vector3 movement = transform.forward * movementInput.z * _moveSpeed + transform.right * movementInput.x * _moveSpeed;
    _playerRigidbody.velocity = new Vector3(movement.x, _playerRigidbody.velocity.y, movement.z);
  }

  private void HandleRotation()
  {
    Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    if (Physics.Raycast(cameraRay, out hit))
    {
      Vector3 pointToLook = hit.point;
      Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

      Vector3 lookDirection = new Vector3(pointToLook.x, transform.position.y, pointToLook.z);
      Quaternion targetRotation = Quaternion.LookRotation(lookDirection - transform.position);
      transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
  }
  private void UpdateAnimation()
  {
    float moveVertical = Input.GetAxisRaw("Vertical");
    bool isMoving = _playerRigidbody.velocity.magnitude > 0;

    _animator.SetBool("IsRunning", isMoving && moveVertical > 0);
    _animator.SetBool("IsRunningBackward", isMoving && moveVertical < 0);
  }
}
