using UnityEngine;

public class MovingObjects : MonoBehaviour, IDestroyableObject
{
    protected Vector2 TargetPosition;

    [Header("Enemy Settings")]
    public float MoveSpeed;
    [SerializeField] private GameObject _blastPrefab;

    protected virtual void Update()
    {
        MoveTowards();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag != collision.transform.tag)
        {
            DestroyGameObjectWithBlast();
        }
    }

    public virtual void MoveTowards()
    {
        transform.position = Vector2.MoveTowards(transform.position, TargetPosition, MoveSpeed * Time.deltaTime);
    }

    public virtual void SetTargetPosition(Vector2 vector2)
    {
        TargetPosition = vector2;
    }

    public void DestroyGameObjectWithBlast()
    {
        Destroy(gameObject);
        var blast = Instantiate(_blastPrefab, transform.position, Quaternion.identity);
        blast.tag = gameObject.tag;
    }
}
