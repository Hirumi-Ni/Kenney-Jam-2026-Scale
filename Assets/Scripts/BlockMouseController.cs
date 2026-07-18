using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BlockMouseController : MonoBehaviour
{
    private bool _isDropped = false;
    private Rigidbody2D _rb;
    private Collider2D _col;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }

    void Start()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _col.isTrigger = true;
    }

    void Update()
    {
        if(!_isDropped)
        {
            Vector3 _blockPos = Mouse.current.position.ReadValue();
            _blockPos.z = 10;
            Vector2 _worldPoint = Camera.main.ScreenToWorldPoint(_blockPos);
            _worldPoint.y = 3.6f;
            transform.position = _worldPoint;
        }
    }

    void FixedUpdate()
    {

    }

    public void DropBlock()
    {
        _rb.linearVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        _isDropped = !_isDropped;

        if (_isDropped)
        {
            _rb.constraints = RigidbodyConstraints2D.None;
            _col.isTrigger = false;
        }
        else
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _col.isTrigger = true;
        }
    }
}
