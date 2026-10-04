using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction moveLeftAction;
    public InputAction moveRightAction;
    public InputAction jumpAction;
    public InputAction slideAction;
    private Animator playerAnim;
    private Rigidbody playerRb;
    private float startYScale = 1f;
    public float crouchYScale = 0.5f;
    public Ground ground;
    /*private float currentXPosition;*/
    public float moveLeftRightSpeed = 5f;
    public float jumpForce = 5f;
    public float slideForce = 5f;
    private int currentLane = 1;
    private float targetX;
    private bool isOnGround;
    private Transform playerCamera;
    private Vector3 cameraStartLocalScale;
    private GameManager gameManager;

    void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerCamera = Camera.main.transform;
        cameraStartLocalScale = playerCamera.localScale;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return new WaitUntil(()=>Ground.IsInitialized);
        jumpAction.Enable();
        slideAction.Enable();
        moveLeftAction.Enable();
        moveRightAction.Enable();
        targetX = Ground.section2Middle;
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        isOnGround = true;
        /*currentXPosition = Ground.section2Middle;*/
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.IsGameOver)
        {
            HandleInput();
        }
        else if(gameManager.IsGameOver)
        {
            playerAnim.SetTrigger("GameOver_trig");
        }
    }

    void FixedUpdate()
    {
        if(!gameManager.IsGameOver)
        {
            HandleMovement();
        }
    }

    void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(
        transform.position.x,
        Camera.main.transform.position.y,
        Camera.main.transform.position.z
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            gameManager.IsGameOver = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

 
    
    void HandleInput() // ABSTRACTION
    {
        if(moveLeftAction.triggered && currentLane > 0)
        {
            currentLane--;
            UpdateLane();
        }
        if(moveRightAction.triggered && currentLane < 2)
        {
            currentLane++;
            UpdateLane();
        }
        if(jumpAction.triggered)
        {
            if(isOnGround)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                playerAnim.SetTrigger("Jump_trig");
                isOnGround = false;
            }
        }
        if(slideAction.triggered)
        {   
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.y);
            playerRb.AddForce(Vector3.down * slideForce, ForceMode.Impulse);
        }
        if(slideAction.WasReleasedThisFrame())
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z); 
        }
    }

    void HandleMovement() // ABSTRACTION
    {
        float newXPos = Mathf.MoveTowards(transform.position.x, targetX, moveLeftRightSpeed * Time.deltaTime);
        playerRb.MovePosition(new Vector3(newXPos, transform.position.y, transform.position.z));
    }
    void UpdateLane() // ABSTRACTION
    {
        if(currentLane==0) targetX = Ground.section1Middle;
        if(currentLane==1) targetX = Ground.section2Middle;
        if(currentLane==2) targetX = Ground.section3Middle;
    }
}

