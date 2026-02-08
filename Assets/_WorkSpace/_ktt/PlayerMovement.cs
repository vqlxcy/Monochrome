using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("ボタン判定")]
    [SerializeField] private string buttonTag = "Button";

    [Header("敵設定")]
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private string bulletTag = "Bullet";
    [SerializeField] private float enemyBounceForce = 8f; // 敵を踏んだ時の跳ね返り
    [SerializeField] private float buttonBounceForce = 6f; // スイッチ踏んだ時の跳ね返り

    [Header("サウンド設定")]
    [SerializeField] private AudioClip jumpSE;
    [SerializeField] private AudioClip goalSE;
    [SerializeField] private AudioClip buttonSE;

    private Rigidbody2D _rb;
    private Animator _animator;
    private AudioSource _audioSource;
    private GameController _gameController;
    private bool isGrounded;
    private float moveInput;
    private bool isGoal = false;
    private bool isDead = false;

    // アニメーションパラメータ名
    private readonly int _isWalkingHash = Animator.StringToHash("Walk");
    private readonly int _isJumpingHash = Animator.StringToHash("Jump");

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
        _audioSource = GetComponent<AudioSource>();

        if (_animator == null)
        {
            Debug.LogError("Animatorが見つかりません！");
        }

        if (_audioSource == null)
        {
            Debug.LogWarning("AudioSourceが見つかりません！AudioSourceを追加します。");
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // ゴール到達後または死亡後は操作不可
        if (isGoal || isDead) return;

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
        // ゴール後または死亡後は物理演算も停止
        if (isGoal || isDead) return;

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

        // ジャンプSEを再生
        if (_audioSource != null && jumpSE != null)
        {
            _audioSource.PlayOneShot(jumpSE);
        }
    }

    private void UpdateAnimation()
    {
        if (_animator == null) return;

        // 歩いているかどうか
        bool isWalking = Mathf.Abs(moveInput) > 0.01f;
        _animator.SetBool(_isWalkingHash, isWalking);

        // ジャンプ中かどうか
        _animator.SetBool(_isJumpingHash, !isGrounded);
    }

    // ゴール・弾判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(goalTag) && !isGoal)
        {
            // ゴールSE再生
            if (_audioSource != null && goalSE != null)
            {
                _audioSource.PlayOneShot(goalSE);
            }
            OnGoalReached();
        }

        // 弾に当たった場合
        if (collision.CompareTag(bulletTag))
        {
            OnPlayerDeath();
        }
    }

    // 敵を踏む判定・ボタン判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            // プレイヤーが敵の上から当たったか判定
            if (IsStompingEnemy(collision))
            {
                StompEnemy(collision.gameObject);
            }
            else
            {
                // 踏んでいない場合は死亡
                OnPlayerDeath();
            }
        }

        if (collision.gameObject.CompareTag(buttonTag))
        {
            // プレイヤーがボタンの上から当たったか判定
            if (IsStompingButton(collision))
            {
                PressButton(collision.gameObject);
            }
        }
    }

    private bool IsStompingEnemy(Collision2D collision)
    {
        // 衝突点の相対的な位置を確認
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // プレイヤーが敵より上にいて、下向きに移動している場合
            if (contact.normal.y > 0.5f && _rb.linearVelocity.y <= 0)
            {
                return true;
            }
        }
        return false;
    }

    private void StompEnemy(GameObject enemy)
    {
        // 敵を破壊
        Destroy(enemy);
        // 踏んだ時の跳ね返り
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, enemyBounceForce);
    }

    private bool IsStompingButton(Collision2D collision)
    {
        // 衝突点の相対的な位置を確認
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // プレイヤーがボタンより上にいて、下向きに移動している場合
            if (contact.normal.y > 0.5f && _rb.linearVelocity.y <= 0)
            {
                return true;
            }
        }
        return false;
    }

    private void PressButton(GameObject button)
    {
        // ボタンSE再生
        if (_audioSource != null && buttonSE != null)
        {
            _audioSource.PlayOneShot(buttonSE);
        }
        // GameControllerにボタンが押されたことを通知
        if (_gameController != null)
        {
            _gameController._isButtonClicked = true;
        }
        // ボタンを破壊
        Destroy(button);
        // 踏んだ時の跳ね返り
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, buttonBounceForce);
    }

    private void OnPlayerDeath()
    {
        if (isDead) return; // 二重処理防止

        isDead = true;
        _rb.linearVelocity = Vector2.zero; // 移動を停止

        // GameOverシーンを読み込む
        SceneManager.LoadScene("MonoGameOver");
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