using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private float _moveSpeed = 10.0f;
  [SerializeField] private float _rotationSpeed = 5.0f;
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
    HandleAttack();
  }
  private void HandleAttack()
  {
    _animator.SetBool("IsAttacking", Input.GetMouseButton(0));
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
    Vector3 localVelocity = transform.InverseTransformDirection(_playerRigidbody.velocity);

    _animator.SetFloat("horizontalMovement", localVelocity.x);
    _animator.SetFloat("verticalMovement", localVelocity.z);
  }
}



/*
bool isMoving = _playerRigidbody.velocity.magnitude > 0;
bool isStrafingLeft = moveHorizontal < 0;
bool isStrafingRight = moveHorizontal > 0;

_animator.SetBool("IsRunning", isMoving && moveVertical > 0);
_animator.SetBool("IsRunningBackward", isMoving && moveVertical < 0);
_animator.SetBool("IsStrafingLeft", isStrafingLeft);
_animator.SetBool("IsStrafingRight", isStrafingRight);
*/