using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ItemBase : MonoBehaviour
{
    public virtual void Effect()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Effect();
        Destroy(gameObject);
    }
}
