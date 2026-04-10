using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DerectObject : MonoBehaviour
{
    public Transform[] Boxes;
    public Image[] images;
    private Camera cam;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }



    private void Update()
    {
        for (int i = 0; i < Boxes.Length; i++)
        {
            if (Boxes[i] == null || images.Length <= i) continue;
            Vector3 boxPosition = cam.WorldToViewportPoint(Boxes[i].position);
            Vector3 screenPos = cam.WorldToScreenPoint(Boxes[i].position);

            bool check = boxPosition.x >= 0f && boxPosition.x <= 1f && boxPosition.y >= 0f && boxPosition.y <= 1f && boxPosition.z > 0f;

            if (check)
            {
                images[i].gameObject.SetActive(false);
            }
            else
            {
                images[i].gameObject.SetActive(true);
                float clp = 30f;
             /* screenPos.x = Mathf.Clamp(screenPos.x, clp, Screen.width - clp);
                screenPos.y = Mathf.Clamp(screenPos.y, clp, Screen.height - clp);
                screenPos.z = 0f;
                if (boxPosition.z < 0f)
                {
                    screenPos *= -1f;
                }*/
                Vector3 local = cam.transform.InverseTransformPoint(Boxes[i].position);
                Vector2 dir = new Vector2(local.x, local.y).normalized;
                Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
                float scale = Mathf.Min(center.x/Mathf.Abs(dir.x),center.y/Mathf.Abs(dir.y));
                Vector2 pos = center + dir * scale;
                pos.x = Mathf.Clamp(pos.x,clp,Screen.width-clp);
                pos.y = Mathf.Clamp(pos.y,clp,Screen.height-clp);
                images[i].transform.position = new Vector3(pos.x, pos.y, 0f);
                /*images[i].transform.position = screenPos;*/
            }
        }
    }

}
