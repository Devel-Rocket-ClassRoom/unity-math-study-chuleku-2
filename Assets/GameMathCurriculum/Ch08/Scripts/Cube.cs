using TMPro;
using Unity.Properties;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class Cube : MonoBehaviour
{
    private Camera cam;
    private float maxRayDistance = 100f;
    private string cubeTag = "Box";
    private string endpointTag = "endpoint";
    private bool isHitting;
    private GameObject cubeObject;
    private GameObject endpointObject;
    private Vector3[] temp;
    private Vector3 upPos;
    private float speed = 5f;
    private float t = 0f;
    public GameObject[] cubeObjects;
    private bool isreturn;
   
    private void Awake()
    {
        cam = GetComponent<Camera>();
        isHitting = false;
        isreturn = false;
        temp = new Vector3[cubeObjects.Length];

        for(int i = 0; i < cubeObjects.Length; i++)
        {
            temp[i] = cubeObjects[i].transform.position;
        }
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance))
            {
                if (hit.collider.CompareTag(cubeTag))
                {
                    cubeObject = hit.collider.gameObject;
                    isHitting = true;
                    endpointObject = null;
                }
            }
        }
        if (isHitting && cubeObject != null)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance))
            {

                if (hit.collider.gameObject != cubeObject)
                {
                    if(hit.collider.CompareTag(endpointTag))
                    {
                        endpointObject = hit.collider.gameObject;
                    }
                    else
                    {
                        endpointObject = null;
                    }
                    float yOffset = cubeObject.transform.localScale.y * 0.5f;
                    cubeObject.transform.position = new Vector3(hit.point.x, hit.point.y + yOffset, hit.point.z);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isHitting && cubeObject != null)
            {
                t = 0f;
                isHitting = false;
                if (endpointObject != null)
                {
                    float yOffset = cubeObject.transform.localScale.y * 0.5f;
                    float endyOffset = endpointObject.transform.localScale.y * 1f; 
                    cubeObject.transform.position = new Vector3(endpointObject.transform.position.x, endpointObject.transform.position.y + yOffset+endyOffset, endpointObject.transform.position.z);
                    isreturn = false;
                    cubeObject = null;
                }
                else
                {
                    isreturn = true;
                    upPos = cubeObject.transform.position;
         
                }
            }
            endpointObject = null;
           
        }
        

        if(isreturn&&cubeObject!=null)
        {
            for (int j = 0; j < cubeObjects.Length; j++)
            {
                if (temp[j] == cubeObjects[j].transform.position)
                {
                    continue;
                }
                else
                {
                    t += Time.deltaTime * speed;
                    cubeObject.transform.position = Vector3.Lerp(upPos, temp[j], t);
                }
            }
            if(t >1f)
            {
                cubeObject = null;
                isreturn = false;
            }
        }
    }
}
