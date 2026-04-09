using Unity.Properties;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class Cube : MonoBehaviour
{
    private Camera cam;
    private float maxRayDistance = 100f;
    private string cubeTag = "Box";
    private string groundTag = "Ground";
    private bool isHitting;
    private GameObject cubeObject;
    private Renderer cubeRenderer;
    private Vector3 temp;
    private bool firstCheck;
    private void Awake()
    {
        cam = GetComponent<Camera>();
        isHitting = false;
        firstCheck = false;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance))
            {

                if (hit.collider.CompareTag(cubeTag))
                {
                    cubeObject = hit.collider.gameObject;
                    isHitting = true;
                }
            }
        }

        if (isHitting && cubeObject != null)
        {
            if(Physics.Raycast(ray,out  RaycastHit hit, maxRayDistance))
            {
                float yOffset = cubeObject.transform.localScale.y * 0.5f;
                cubeObject.transform.position = new Vector3(hit.point.x, hit.point.y + yOffset, hit.point.z);
            }
        }

    }
}
