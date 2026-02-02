using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(CircleCollider2D))]
public class KeyCoin : MonoBehaviour
{
    GameController _gc;

    [SerializeField]
    string _nextStage;

    private void Awake()
    {
        _gc = FindAnyObjectByType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _gc._collisionCoinList[0] == gameObject)
        {
            _gc._collisionCoinList.RemoveAt(0);
            Destroy(gameObject);

            if (_gc._collisionCoinList[0] == null)
            {
                SceneManager.LoadScene(_nextStage);
            }
        }
    }
}
