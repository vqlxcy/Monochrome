using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField]
    float _enemyAttackInterval;
    [SerializeField]
    GameObject _enemyBullet;

    float _time;
    void Update()
    {
        _time += Time.deltaTime;
        if (_time > _enemyAttackInterval)
        {
            Instantiate(_enemyBullet,gameObject.transform.position,Quaternion.identity);
            _time = 0;
        }
    }
}
