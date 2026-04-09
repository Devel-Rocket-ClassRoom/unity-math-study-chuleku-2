using TMPro;
using UnityEngine;
using System.Collections.Generic;
public class BezierRandomMover : MonoBehaviour
{
    [Header("=== 스플라인 경로 ===")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Vector3 waypoint;

    [Header("=== 컨베이어 박스 ===")]
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject quad;
  
    public int spawnBoxCount = 5;
    private class Boxes
    {
        public GameObject obj;
        public Vector3 midPoint;
        public float move;
        public float speed;
    }
    private List<Boxes> boxList = new List<Boxes>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < spawnBoxCount; i++)
            {
                float r = Random.value;
                if (r < 0.35f)
                {
                    SpawnBall();
                }
                else if(r >=0.35f && r<0.7f)
                {
                    SpawnBox();
                }
                else
                {
                    SpawnQuad();
                }
            }
        }
        for(int i = boxList.Count-1;i>=0 ; i--)
        {
            Boxes b = boxList[i];
            b.move += Time.deltaTime * b.speed;
            b.obj.transform.position = QuadraticBezier(
                startPoint.position,
                b.midPoint,
                endPoint.position,
                b.move
            );

            if (b.move >= 1f)
            {
                Destroy(b.obj);
                boxList.RemoveAt(i);
            }
        }
    }

    private void SpawnBox()
    {
        GameObject newBox = Instantiate(box, startPoint.position, Quaternion.identity);
        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);
        float y = Random.Range(-10f, 10f);
        Vector2 randomx = new Vector2(startPoint.position.x + x, endPoint.position.x + z);
        Vector2 randomz = new Vector2(startPoint.position.z + z, endPoint.position.z + z);
        Vector2 randomy = new Vector2(startPoint.position.y + y, endPoint.position.y + y);
        float rx = Random.Range(randomx.x, randomx.y);
        float rz = Random.Range(randomz.x, randomz.y);
        float ry = Random.Range(randomy.x, randomy.y);
        float randomSpeed = Random.Range(0.5f, 3f);
        waypoint = new Vector3(rx, ry, rz);

        Boxes newBoxes = new Boxes
        {
            obj = newBox,
            midPoint = waypoint,
            move = 0f,
            speed = randomSpeed
        };
        ApplyRandomColor(newBox);

        boxList.Add(newBoxes);
    }
    private void SpawnBall()
    {
        GameObject newBox = Instantiate(ball, startPoint.position, Quaternion.identity);
        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);
        float y = Random.Range(-10f, 10f);
        Vector2 randomx = new Vector2(startPoint.position.x + x, endPoint.position.x + x);
        Vector2 randomz = new Vector2(startPoint.position.z + z, endPoint.position.z + x);
        Vector2 randomy = new Vector2(startPoint.position.y + y, endPoint.position.y + y);
        float rx = Random.Range(randomx.x, randomx.y);
        float rz = Random.Range(randomz.x, randomz.y);
        float ry = Random.Range(randomy.x, randomy.y);
        float randomspeed = Random.Range(0.5f, 5f);
        waypoint = new Vector3(rx, ry, rz);

        Boxes newBoxes = new Boxes
        {
            obj = newBox,
            midPoint = waypoint,
            move = 0f,
            speed = randomspeed
        };
        ApplyRandomColor(newBox);

        boxList.Add(newBoxes);
    }
    private void SpawnQuad()
    {
        GameObject newBox = Instantiate(quad, startPoint.position, Quaternion.identity);
        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);
        float y = Random.Range(-10f, 10f);
        Vector2 randomx = new Vector2(startPoint.position.x + x, endPoint.position.x + x);
        Vector2 randomz = new Vector2(startPoint.position.z + z, endPoint.position.z + x);
        Vector2 randomy = new Vector2(startPoint.position.y + y, endPoint.position.y + y);
        float rx = Random.Range(randomx.x, randomx.y);
        float rz = Random.Range(randomz.x, randomz.y);
        float ry = Random.Range(randomy.x, randomy.y);
        float randomspeed = Random.Range(3f, 5f);
        waypoint = new Vector3(rx, ry, rz);

        Boxes newBoxes = new Boxes
        {
            obj = newBox,
            midPoint = waypoint,
            move = 0f,
            speed = randomspeed
        };
        ApplyRandomColor(newBox);

        boxList.Add(newBoxes);
    }
    private Vector3 QuadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        return Vector3.Lerp(a, b, t);
    }
    private void ApplyRandomColor(GameObject target)
    {
        Renderer rend = target.GetComponent<Renderer>();
        TrailRenderer render = target.GetComponent<TrailRenderer>();
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.5f, 0f);
        curve.AddKey(0f, 0.5f);
        if (rend != null)
        {
            rend.material.color = Random.ColorHSV(0f, 1f, 0.6f, 1f, 0.7f, 1f);
            render.material.color = rend.material.color;
            render.time = 0.2f;
            render.widthCurve = curve;
        }
    }

}
