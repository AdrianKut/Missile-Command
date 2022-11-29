using UnityEngine;

[RequireComponent(typeof(PlayerMissileCounter), typeof(UIPlayerMissileBase))]
public class PlayerMissileBase : MonoBehaviour
{
    public bool IsEnabled = true;

    [SerializeField] private GameObject _missilePrefab;
    [SerializeField] private string _enemyTag;

    private UIPlayerMissileBase _ui;
    private PlayerMissileCounter _missileCounter;
    private int _leftMissleOnStart;

    private void Awake()
    {
        _ui = GetComponent<UIPlayerMissileBase>();
        _missileCounter = GetComponent<PlayerMissileCounter>();
    }

    private void Start()
    {
        _leftMissleOnStart = _missileCounter.GetLeftMissile();
    }

    public void SetStartingVariables()
    {
        IsEnabled = true;
        _missileCounter.SetLeftMissile(_leftMissleOnStart);
        SetMissileCounterText();
    }

    public void Shoot()
    {
        if (IsEnabled)
        {
            _missileCounter.DecreaseMissileAmount(1);
            SetMissileCounterText();

            if (_missileCounter.GetLeftMissile() <= 0)
            {
                IsEnabled = false;
            }

            InstantiateMissile();
        }
    }

    private void SetMissileCounterText()
    {
        switch (_missileCounter.GetLeftMissile())
        {
            case 0:
                _ui.SetText("OUT");
                break;
            case <= 3:
                _ui.SetText("LOW\n" + _missileCounter.GetLeftMissile());
                break;
            default:
                _ui.SetText(_missileCounter.GetLeftMissile().ToString());
                break;
        }
    }

    private void InstantiateMissile()
    {
        Instantiate(_missilePrefab, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(_enemyTag))
        {
            _missileCounter.SetLeftMissile(0);
            SetMissileCounterText();
            IsEnabled = false;
        }
    }
}
