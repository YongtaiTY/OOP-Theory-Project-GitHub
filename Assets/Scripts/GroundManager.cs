using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public GameObject groundPrefab;
    private GameObject initialGround;
    private Vector3 nextSpawnPos;
    private float spawnTriggerZ = 575f;
    private float groundPrefabLengthZ;
    private GameObject lastSpawned;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        groundPrefabLengthZ = groundPrefab.GetComponent<Renderer>().bounds.size.z;
        initialGround = GameObject.Find("InitialGround");
        Renderer initialRend = initialGround.GetComponent<Renderer>();
        nextSpawnPos = new Vector3(155, 0, initialRend.bounds.max.z + groundPrefab.GetComponent<Renderer>().bounds.extents.z);
        SpawnGround(nextSpawnPos);
    }

    // Update is called once per frame
    void Update()
    {
        if(lastSpawned.transform.position.z < spawnTriggerZ)
        {
            nextSpawnPos = lastSpawned.transform.position + new Vector3(0, 0,groundPrefabLengthZ);
            SpawnGround(nextSpawnPos);
        }
    }

    void SpawnGround(Vector3 spawnPos) // ABSTRACTION
    {
        GameObject newGround = Instantiate(groundPrefab, spawnPos, groundPrefab.transform.rotation);
        newGround.transform.SetParent(transform);
        lastSpawned = newGround;
    }
}