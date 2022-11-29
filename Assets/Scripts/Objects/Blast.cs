using UnityEngine;

public class Blast : MonoBehaviour, IDestroyableObject
{
    [SerializeField] private Color _colorBlast;
    [SerializeField] private float _maxScale;
    [SerializeField] private float _speedScale;
    [SerializeField] private bool _gameOverOnDestroy = false;
    private Vector2 _targetScale;
    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        _targetScale = new Vector2(_maxScale, _maxScale);
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = _colorBlast;
    }

    private void Update()
    {
        ScaleToDestroy();
    }

    private void ScaleToDestroy()
    {
        var currentScale = Mathf.RoundToInt(transform.localScale.x);
        if (currentScale != _maxScale)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, _targetScale, _speedScale * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);

            if (_gameOverOnDestroy)
            {
                GameManager.Instance.ChangeGameState(GameState.GameOver);
            }
        }
    }
}
