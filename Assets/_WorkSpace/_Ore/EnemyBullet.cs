using UnityEngine;

[RequireComponent (typeof(CircleCollider2D))]
public class EnemyBullet : MonoBehaviour
{
    Transform _tr;
    Vector3 _move = new Vector3(1,0,0);

    [SerializeField, Header("’e‚ÌˆÚ“®‘¬“x")]
    int _moveSpeed;

    void Awake()
    {
        _tr = transform;
    }

    void Update()
    {
        _tr.position += _move * Time.deltaTime * _moveSpeed;

        if (_tr.position.x <= -13 || _tr.position.x >= 13)
        {
            Destroy(gameObject);
        }
    }
}
