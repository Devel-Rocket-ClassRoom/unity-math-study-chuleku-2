using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Rotate();
    }
    private void FixedUpdate()
    {
        Vector3 moveDirection = (Vector3.forward * playerInput.Move) + (Vector3.right * playerInput.Rotate);
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        Vector3 delta = moveDirection * moveSpeed * Time.fixedDeltaTime;

        playerRigidbody.MovePosition(playerRigidbody.position + delta);
    }
    private void Rotate()
    {
        Ray _cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (plane.Raycast(_cameraRay, out rayLength))
        {
            Vector3 pointToLook = _cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
