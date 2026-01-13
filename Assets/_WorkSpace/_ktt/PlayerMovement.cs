using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("ジャンプ設定")]
    [SerializeField] private float jumpForce = 10f;

    [Header("接地判定")]
    [SerializeField] private Transform groundCheck; // Playerの子オブジェクトに"GroundCheck"を作りここに入れる
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.8f, 0.1f); // 接地判定の範囲
    [SerializeField] private LayerMask groundLayer; // Layerで"Ground"を作ってここに設定する(地面のLayerはGroundにする)

    private Rigidbody2D _rb;
    private bool isGrounded;
    private float moveInput;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); // GravityScaleは3くらい
    }
    void Update()
    {
        // 入力取得
        moveInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
        }

        CheckGrounded(); // 接地判定
       
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // ジャンプ
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(moveInput * moveSpeed, _rb.linearVelocity.y); // 移動
    }
    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer); // OverlapBoxでIsTriggerのコライダーも検出
    }
    private void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce); // _rb.linearVelocity.xを変えることで、ジャンプ中の左右移動を制限できる
    } 
    private void OnDrawGizmos() // 接地判定の可視化
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        }
    }
}
// NoFrictionをBoxCollider2Dに入れる