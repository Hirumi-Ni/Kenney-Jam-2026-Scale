using UnityEngine;

public class BlockController : MonoBehaviour
{
    private bool isDropped = false;
    private Rigidbody2D _rb;
    private Collider2D _col;
    private SpriteRenderer _spriteRenderer;
    private Color _blockColor;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _col.isTrigger = true;

        _blockColor = _spriteRenderer.color;
        _blockColor.a = .5f;
    }

    public void MoveTo(Vector2 pos)
    {
        if (isDropped) return;
        transform.position = pos;
    }

    public void DropBlock()
    {
        if (isDropped) return;
        isDropped = !isDropped;

        _rb.linearVelocity = Vector2.zero;
        _rb.constraints = RigidbodyConstraints2D.None;
        transform.rotation = Quaternion.identity;

        _col.isTrigger = false;
        _blockColor.a = 1f;
    }
}
