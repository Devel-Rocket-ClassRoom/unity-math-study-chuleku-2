using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static readonly string MoveAxis = "Vertical";
    public static readonly string RotateAxis = "Horizontal";

    public float Move { get; private set; }
    public float Rotate { get; private set; }
    void Update()
    {
        Move = Input.GetAxis(MoveAxis);
        Rotate = Input.GetAxis(RotateAxis);
    }
}
