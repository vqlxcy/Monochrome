using UnityEngine;

[RequireComponent (typeof(CircleCollider2D))]
public class EnemyBullet : MonoBehaviour
{
    Transform _tr;
    GameController _gc;
    Vector3 _move = new Vector3(1,0,0);

    [SerializeField, Header("’e‚ÌˆÚ“®‘¬“x")]
    int _moveSpeed;

    void Awake()
    {
        _tr = transform;
        _gc = FindAnyObjectByType<GameController>();
    }

    void Update()
    {
        if (_gc._isRotate == !_gc._isRotate)
        {
            _moveSpeed *= -1;
        }

        _tr.position += _move * Time.deltaTime * _moveSpeed;

        if (_tr.position.x <= -13 || _tr.position.x >= 13)
        {
            Destroy(gameObject);
        }
    }
}
