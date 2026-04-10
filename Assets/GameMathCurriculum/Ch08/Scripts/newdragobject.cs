using UnityEngine;

public class newdragobject : MonoBehaviour
{
    public Vector3 originalPosition;
    public bool isReturn;
    private Terrain terrain;
    public float timeReturn = 3f;
    public float timer;
    private Vector3 startPosition;

    private void Start()
    {
        originalPosition = transform.position;
        terrain = Terrain.activeTerrain;
    }

    private void Update()
    {
        if (isReturn)
        {
            timer += Time.deltaTime/timeReturn;
            transform.position = Vector3.Lerp(startPosition, originalPosition, timer);

            if(timer>1f)
            {
                transform.position = originalPosition;
                isReturn = false;
                timer = 0f;
            }
        }
    }
    public void Return()
    {
        ResetDrag();
        timer = 0f;
        isReturn = true;
        startPosition = transform.position;
    }
    public void DragStart()
    {
        ResetDrag();
    }

    public void DragEnd()
    {
        ResetDrag();
    }
    private void ResetDrag()
    {
        isReturn=false;
        timer = 0f;
        startPosition = Vector3.zero;
    }
}
