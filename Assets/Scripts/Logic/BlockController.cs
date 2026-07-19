using UnityEngine;

public class BlockController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D _col;
    private bool _isDropped = false;
    private int _overlapCount = 0;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }

    void Start()
    {
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _col.isTrigger = true;
    }

    public void MoveTo(Vector2 pos)
    {
        if (_isDropped) return;
        transform.position = pos;
    }

    public void DropBlock()
    {
        if (_isDropped) return;

        _isDropped = !_isDropped;

        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.linearVelocity = Vector2.zero;
        _rb.constraints = RigidbodyConstraints2D.None;
        transform.rotation = Quaternion.identity;

        _col.isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayObject"))
        {
            _overlapCount++;
            Debug.Log("Overlap");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayObject"))
        {
            _overlapCount--;
            Debug.Log("No longer overlap");
        }
    }

    public bool CheckOverlapBlock()
    {
        if (_overlapCount >= 1)
        {
            return true; //nyentuh block lain
        }
        else
        {
            return false;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.instance.PlayAudio(SoundType.ImpactNormal);
    }
}
