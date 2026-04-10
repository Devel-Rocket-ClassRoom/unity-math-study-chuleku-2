using UnityEngine;

public class Newdraganddrop : MonoBehaviour
{
    public Camera cam;
    public LayerMask groud;
    public LayerMask dragObject;
    public LayerMask dropzone;
    private newdragobject dragginObject;
    private bool isDraging = false;
    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,dragObject))
            {
                isDraging = true;
                dragginObject = hit.collider.gameObject.GetComponent<newdragobject>();
                dragginObject.DragStart();
                
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(isDraging)
            {
                if(Physics.Raycast(ray,out  RaycastHit hit, Mathf.Infinity,dropzone))
                {
                    dragginObject.DragEnd();
                }
                else
                {
                    dragginObject.Return();
                }
            }

            isDraging = false;
            dragginObject = null;
        }
        else if (isDraging)
        {

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,groud))
            {
               Debug.Log(hit.point);
               float yoffset = dragginObject.transform.localScale.y*0.5f;
               dragginObject.transform.position = new Vector3(hit.point.x, hit.point.y+yoffset, hit.point.z);
                
            }
        }
        

    }
}
