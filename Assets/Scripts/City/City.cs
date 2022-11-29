using System.Diagnostics;
using UnityEngine;

public class City : MonoBehaviour
{
    public bool IsEnabled = true;

    [SerializeField] private string _collisionTag;
    [SerializeField] private Sprite _city;
    [SerializeField] private Sprite _destroyedCity;

    private SpriteRenderer _spriteRenderer;

    private CityManager _cityManager;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeSprite(_city);
    }

    private void Start()
    {
        _cityManager = CityManager.Instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(_collisionTag))
        {
            IsEnabled = false;
            ChangeSprite(_destroyedCity);
            _cityManager.CheckEnabledCities();
        }
    }

    public void SetStartingVariable()
    {
        ChangeSprite(_city);
        IsEnabled = true;
    }

    private void ChangeSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
