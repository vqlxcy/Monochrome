using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    GameController _gc;

    [SerializeField]
    float _enemyAttackInterval;
    [SerializeField]
    GameObject _enemyBullet;

    float _time;
    int _bulletVelocity = 1;

    void Start()
    {
        _gc = FindAnyObjectByType<GameController>();
    }

    void Update()
    {
        _time += Time.deltaTime * _bulletVelocity;
        if (_gc._isRotate == !_gc._isRotate)
        {
            _bulletVelocity *= -1;
        }
        if (_time > _enemyAttackInterval)
        {
            Instantiate(_enemyBullet,gameObject.transform.position,Quaternion.identity);
            _time = 0;
        }
    }
}
