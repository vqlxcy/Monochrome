using UnityEngine;

[RequireComponent (typeof(CircleCollider2D))]
public class EnemyBullet : MonoBehaviour
{
    Transform _tr;
    GameController _gc;
    Vector3 _move = new Vector3(1,0,0);

    [SerializeField, Header("’e‚ÌˆÚ“®‘¬“x")]
    int _moveSpeed;

    int _moveDirection;

    float _syokiti;
    public float _direction = 1;

    void Awake()
    {
        _tr = transform;
        _gc = FindAnyObjectByType<GameController>();
    }

    void Update()
    {
        if (_gc._isRotate)
        {
            _moveDirection = -1;
            _direction = -1;
            _tr.localScale = new Vector3(_syokiti * _direction, 0.1f, 1);
        }
        else
        {
            _moveDirection = 1;
            _direction = 1;
            _tr.localScale = new Vector3(_syokiti * _direction, 0.1f, 1);
        }

        _tr.position += _move * Time.deltaTime * _moveSpeed * _moveDirection;

        if (_tr.position.x <= -9 || _tr.position.x >= 9)
        {
            Destroy(gameObject);
        }
    }
}
