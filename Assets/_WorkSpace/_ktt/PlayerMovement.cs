using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("ジャンプ設定")]
    [SerializeField] private float jumpForce = 10f;

    [Header("接地判定")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.8f, 0.1f);

    [Header("ゴール設定")]
    [SerializeField] private string goalTag = "Goal";

    private Rigidbody2D _rb;
    private Animator _animator;
    private GameController _gameController;
    private bool isGrounded;
    private float moveInput;
    private bool isGoal = false;

    // アニメーション状態名
    private const string ANIM_IDLE = "Idle";
    private const string ANIM_WALK = "Walk";
    private const string ANIM_JUMP = "Jump";

    void Awake()
    {
        _gameController = FindAnyObjectByType<GameController>();
        if (_gameController == null)
        {
            Debug.LogError("GameControllerが見つかりません！");
        }
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError("Animatorが見つかりません！");
        }
    }

    void Update()
    {
        // ゴール到達後は操作不可
        if (isGoal) return;

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

        // 接地判定
        CheckGrounded();

        // ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // アニメーション更新
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        // ゴール後は物理演算も停止
        if (isGoal) return;

        // 移動
        _rb.linearVelocity = new Vector2(moveInput * moveSpeed, _rb.linearVelocity.y);
    }

    private void CheckGrounded()
    {
        // レイヤーを使わずに接地判定（プレイヤー自身以外のコライダーを検出）
        Collider2D[] hits = Physics2D.OverlapBoxAll(groundCheck.position, groundCheckSize, 0f);

        // 検出したコライダーの中にプレイヤー以外があれば接地
        isGrounded = false;
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject != gameObject)
            {
                isGrounded = true;
                break;
            }
        }
    }

    private void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
    }

    private void UpdateAnimation()
    {
        if (_animator == null) return;

        // ジャンプ中（空中）
        if (!isGrounded)
        {
            _animator.Play(ANIM_JUMP);
        }
        // 動いているとき
        else if (Mathf.Abs(moveInput) > 0.01f)
        {
            _animator.Play(ANIM_WALK);
        }
        // 動いていないとき
        else
        {
            _animator.Play(ANIM_IDLE);
        }
    }

    // ゴール判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(goalTag) && !isGoal)
        {
            OnGoalReached();
        }
    }

    private void OnGoalReached()
    {
        isGoal = true;
        _rb.linearVelocity = Vector2.zero; // 移動を停止

        // GameControllerのゴールフラグを立てる
        if (_gameController != null)
        {
            _gameController._isGoal = true;
        }
    }

    // 接地判定の可視化（Editor上でのみ表示）
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        }
    }
}