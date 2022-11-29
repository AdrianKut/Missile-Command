using UnityEngine;

public class PlayerMissile : MovingObjects
{
    [Header("Target Crosshair")]
    [SerializeField] private GameObject _crosshairPrefab;
    private GameObject _crosshair;

    private void Start()
    {
        SetTargetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        InstantiateTargetCrosshair();
    }

    protected override void Update()
    {
        base.Update();
        DestroyOnTargetPosition();
    }

    private void InstantiateTargetCrosshair()
    {
        _crosshair = Instantiate(_crosshairPrefab, TargetPosition, Quaternion.identity);
    }

    private void DestroyOnTargetPosition()
    {
        Vector2 curretPosition = new Vector2(transform.position.x, transform.position.y);
        if (curretPosition == TargetPosition)
        {
            DestroyGameObjectWithBlast();
        }
    }

    private void OnDestroy()
    {
        Destroy(_crosshair);
    }
}
