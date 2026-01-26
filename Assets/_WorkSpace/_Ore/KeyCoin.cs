using UnityEngine;

[RequireComponent (typeof(CircleCollider2D))]
public class KeyCoin : MonoBehaviour
{
    GameController _gc;

    private void Awake()
    {
        _gc = FindAnyObjectByType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _gc._coinList[0] == gameObject)
        {
            _gc._coinList.RemoveAt(0);
            Destroy(gameObject);
        }
    }
}
