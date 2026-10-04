using UnityEngine;

public class ScrollMovement : MonoBehaviour
{
    public float groundSpeed;
    private Rigidbody rb;
    private GameManager gameManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if(gameManager.IsGameOver == false)
        {
            rb.MovePosition(rb.position + Vector3.back * groundSpeed * Time.fixedDeltaTime);
        }
    }
}
