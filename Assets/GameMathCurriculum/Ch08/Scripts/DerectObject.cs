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
                float y = 30f;
                screenPos.x = Mathf.Clamp(screenPos.x, 25, Screen.width - 25f);
                screenPos.y = Mathf.Clamp(screenPos.y, 25, Screen.height - y);
                screenPos.z = 0f;
                if (boxPosition.z < 0f)
                {
                    screenPos *= -1f;
                }
                images[i].transform.position = screenPos;
            }
        }
    }

}
