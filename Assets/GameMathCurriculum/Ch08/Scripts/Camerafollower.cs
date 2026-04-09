using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    private Vector3 offset = new Vector3(0f, 6f, -5f);
    public float smoothSpeed = 0.1f;
    private float rotationSmoothSpeed = 3f;
    private Vector3 positionVelocity;
    private float zoomDistance = 3f;

    void LateUpdate()
    {
        Vector3 targetoffset = target.rotation * offset.normalized;
        Vector3 desiredPosition = target.position + targetoffset * zoomDistance + Vector3.up * offset.y;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref positionVelocity, smoothSpeed);
        float t = Time.deltaTime * rotationSmoothSpeed;
        Vector3 lookTarget = target.position + Vector3.up * 1.5f;
        Vector3 look = (lookTarget - transform.position).normalized;
        if (look != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(look);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);
        }
    }
}
