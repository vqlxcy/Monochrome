using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField]
    int _enemyAttackInterval;
    [SerializeField]
    GameObject _enemyBullet;

    float _time;
    void Update()
    {
        _time += Time.deltaTime;
        if (_time > _enemyAttackInterval)
        {
            Instantiate(_enemyBullet);
            _time = 0;
        }
    }
}
