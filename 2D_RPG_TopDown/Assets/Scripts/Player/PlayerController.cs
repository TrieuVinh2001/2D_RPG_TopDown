using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public bool FacingLeft { get { return facingLeft; } }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer trailRenderer;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer mySprite;
    private float startMoveSpeed;

    private bool facingLeft = false;
    private bool isDashing = false;

    
    protected override void Awake()
    {
        base.Awake();//Cần để hoạt động
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        startMoveSpeed = moveSpeed;
        playerControls.Combat.Dash.performed += _ => Dash();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        FacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();//Đọc giá trị Vector 2 từ input System

        anim.SetFloat("MoveX", movement.x);
        anim.SetFloat("MoveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);//Di chuyển
    }

    private void FacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySprite.flipX = true;//Xoay hướng ảnh
            facingLeft = true;
        }
        else
        {
            mySprite.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            trailRenderer.emitting = true;//tạo ra khi di chuyển
            StartCoroutine(EndDashRoutine());
        }
        
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = 0.2f;
        float dashCD = 0.3f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startMoveSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
