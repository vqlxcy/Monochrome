using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody2D _rb;
    private bool isGrounded;

    private float moveInput;
    private bool jumpInput;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // “ü—ÍŽæ“¾‚Ì‚Ý
        moveInput = 0f;
        if (Input.GetKey(KeyCode.A)) moveInput = -1f;
        if (Input.GetKey(KeyCode.D)) moveInput = 1f;

        if (Input.GetKeyDown(KeyCode.W))
            jumpInput = true;

<<<<<<< HEAD
=======
        if (Input.GetKey(KeyCode.A)) h = -1f;
        if (Input.GetKey(KeyCode.D)) h = 1f;

        _rb.linearVelocity += new Vector2(h * moveSpeed, _rb.linearVelocity.y);
    }

    // ƒWƒƒƒ“ƒv
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
        }
    }

    // ¢ŠE”½“]i‰¼j
    void WorldReverse()
    {
>>>>>>> 0d0353ced9bb23bbb4233027a7e019980a4d82e6
        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log("¢ŠE”½“]I");
    }

    void FixedUpdate()
    {
        // ˆÚ“®
        _rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            _rb.linearVelocity.y
        );

        // ƒWƒƒƒ“ƒv
        if (jumpInput && isGrounded)
        {
            _rb.linearVelocity = new Vector2(
                _rb.linearVelocity.x,
                jumpForce
            );
        }

        jumpInput = false;
    }

    // Ú’n”»’è
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
