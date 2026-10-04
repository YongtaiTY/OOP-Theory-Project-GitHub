using UnityEngine;

public class Ground : MonoBehaviour
{
    public static Ground Instance;
    public GameObject[] obstacles;
    public float zMin {get; private set;} // ENCAPSULATION
    public float zMax {get; private set;} // ENCAPSULATION
    public float zSize {get; private set;} // ENCAPSULATION
    public static float section1Middle {get; private set;} // ENCAPSULATION
    public static float section2Middle {get; private set;} // ENCAPSULATION
    public static float section3Middle {get; private set;} // ENCAPSULATION
    private float xMin {get; set;}
    private float xMax {get; set;}
    private float sectionWidth {get; set;}
    private float boundary1 {get; set;}
    private float boundary2 {get; set;}
    public static bool IsInitialized { get; private set; } // ENCAPSULATION

    void Awake()
    {
        Instance = this;
        Renderer groundRend = GetComponent<Renderer>();
        Bounds bounds = groundRend.bounds;
        xMin = bounds.min.x;
        xMax = bounds.max.x;
        zMin = bounds.min.z;
        zMax = bounds.max.z;
        zSize = bounds.size.z;

        sectionWidth = (xMax-xMin)/3;
        boundary1 = xMin + sectionWidth;
        boundary2 = xMin + sectionWidth*2;

        section1Middle = (xMin + boundary1)/2;
        section2Middle = (boundary1 + boundary2)/2;
        section3Middle = (boundary2 + xMax)/2;
            
        DrawLine(new Vector3(boundary1, 0.01f, zMin), new Vector3(boundary1, 0.01f, zMax), Color.red);
        DrawLine(new Vector3(boundary2, 0.01f, zMin), new Vector3(boundary2, 0.01f, zMax), Color.red);

        IsInitialized = true;
    }

    void Start()
    {
        SpawnRandomObstaclesSec1();
        SpawnRandomObstaclesSec2();
        SpawnRandomObstaclesSec3();
    }

    void DrawLine(Vector3 startPos, Vector3 endPos, Color color)
    {
        GameObject lineObj = new GameObject("SectionLine");
        lineObj.transform.SetParent(transform);
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.positionCount = 2;
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = color;
        lr.endColor = color;
    }

    void OnDrawGizmos()
    {
            Renderer groundRend = GetComponent<Renderer>();
            Bounds bounds = groundRend.bounds;
            float xMin = bounds.min.x;
            float xMax = bounds.max.x;
            float zMin = bounds.min.z;
            float zMax = bounds.max.z;

            float sectionWidth = (xMax-xMin)/3;
            float boundary1 = xMin + sectionWidth;
            float boundary2 = xMin + sectionWidth*2;
            DrawGizmoLine(new Vector3(boundary1, 0.01f, zMin), new Vector3(boundary1, 0.01f, zMax), Color.red);
            DrawGizmoLine(new Vector3(boundary2, 0.01f, zMin), new Vector3(boundary2, 0.01f, zMax), Color.red);
    }

    void DrawGizmoLine(Vector3 start, Vector3 end, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(start, end);
    }

    void SpawnRandomObstaclesSec1()
    {
        SpawnRandomObstacle(section1Middle);  
    }

    void SpawnRandomObstaclesSec2()
    {
        SpawnRandomObstacle(section2Middle);  
    }

    void SpawnRandomObstaclesSec3()
    {
        SpawnRandomObstacle(section3Middle);  
    }

    void SpawnRandomObstacle(float sectionMiddle)
    {
        int randomIndex = Random.Range(0, obstacles.Length);
        GameObject selectedObstacle = obstacles[randomIndex];
        Obstacles obstacleScript = selectedObstacle.GetComponent<Obstacles>();
        float randomZIndex = Random.Range(zMin, zMax-5);
        GameObject spawned = Instantiate(selectedObstacle, new Vector3(sectionMiddle, obstacleScript.YPosition, randomZIndex), selectedObstacle.transform.rotation);
        spawned.transform.SetParent(transform);
    }
}
