using UnityEngine;

public class ElectricCutter : MonoBehaviour
{
    Transform _tr;
    StageColor _sc;
    GameController _gc;
    BoxCollider2D _c2d;
    SpriteRenderer _sr;


    [SerializeField]
    int _moveSpeed;
    [SerializeField]
    int _alpha;
    void Start()
    {
        _tr = GetComponent<Transform>();
        _sc = FindAnyObjectByType<StageColor>();
        _c2d = GetComponent<BoxCollider2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _tr.position += new Vector3(_moveSpeed * Time.deltaTime, 0, 0);
        if (_tr.position.x >= 10 || _tr.position.x <= -10)
        {
            _moveSpeed *= -1;
        }

        if (_sc._colorControlNumber == 0)
        {
            _sr.color = new Vector4(255, 255, 255, 255);
            _c2d.enabled = true;
        }
        else
        {
            _sr.color = new Vector4(255, 255, 255, _alpha);
            _c2d.enabled = false;
        }
    }
}
