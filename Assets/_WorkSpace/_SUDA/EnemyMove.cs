using UnityEngine;

public class EnemyMove: MonoBehaviour
{
    [Header("ターゲット")]
    public string playerTag = "Player";
    Transform player;

    [Header("追跡設定")]
    public float detectDistance = 6f;
    public float jumpDistance = 1.8f;
    public float moveSpeed = 2f;

    [Header("ジャンプ設定")]
    public float jumpPower = 6f;
    public float jumpCooldown = 1.5f;

    [Header("接地判定")]
    public LayerMask groundLayer;

    Rigidbody2D rb;
    Animator animator;
    Collider2D col;

    bool isGrounded;
    bool canJump = true;

    void Start()
    {
        var p = GameObject.FindGameObjectWithTag(playerTag);
        if (p != null)
            player = p.transform;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        CheckGround();

        if (distance > detectDistance)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.Play("EnemyIdle");
            return;
        }

        float dir = Mathf.Sign(player.position.x - transform.position.x);

        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
        animator.Play("EnemyWalk");

        if (dir > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        if (distance < jumpDistance && isGrounded && canJump)
        {
            Jump();
        }
    }

    void CheckGround()
    {
        Bounds b = col.bounds;

        Vector2 origin = new Vector2(b.center.x, b.min.y - 0.05f);
        Vector2 size = new Vector2(b.size.x * 0.9f, 0.1f);

        isGrounded = Physics2D.OverlapBox(origin, size, 0, groundLayer);
    }

    void Jump()
    {
        canJump = false;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    void ResetJump()
    {
        canJump = true;
    }

    void OnDrawGizmosSelected()
    {
        if (col == null) col = GetComponent<Collider2D>();
        if (col == null) return;

        Bounds b = col.bounds;
        Vector2 origin = new Vector2(b.center.x, b.min.y - 0.05f);
        Vector2 size = new Vector2(b.size.x * 0.9f, 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(origin, size);
    }
}
