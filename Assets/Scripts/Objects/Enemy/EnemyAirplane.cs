using UnityEngine;

public class EnemyAirplane : Enemy
{
    [Header("Airplane")]
    [SerializeField] private GameObject _bigBlast;
   
    private Vector2 vector2Bound;

    private void OnEnable()
    {
        TargetPosition = Vector2.zero;
        vector2Bound = -transform.position;
    }

    protected override void Update()
    {
        Move();
    }

    private void LateUpdate()
    {
        CheckPosition();
    }

    public void Move()
    {
        transform.Translate(TargetPosition * MoveSpeed * Time.deltaTime);
    }

    public override void SetTargetPosition(Vector2 vector2)
    {
        TargetPosition = vector2;
    }

    private void CheckPosition()
    {
        if (transform.position.x <= vector2Bound.x && TargetPosition.x < 0)
        {
            GameOverOnBound();
        }
        else if (transform.position.x >= vector2Bound.x && TargetPosition.x > 0)
        {
            GameOverOnBound();
        }
    }

    private void GameOverOnBound()
    {
        Destroy(gameObject);
        Instantiate(_bigBlast, Vector2.zero, Quaternion.identity);
    }
}
