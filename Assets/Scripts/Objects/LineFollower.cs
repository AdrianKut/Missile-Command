using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineFollower : MonoBehaviour, IDestroyableObject
{
    [SerializeField] private Color _lineColor;
    [SerializeField] private AnimationCurve _curve;
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        InitialLineParameters();
    }

    private void Update()
    {
        UpdateLinePosition();
    }

    private void InitialLineParameters()
    {
        //Two position counts = 1 - start pos | 2 - following missile
        _lineRenderer.positionCount = 2;
        _lineRenderer.widthCurve = _curve;
        _lineRenderer.startColor = _lineColor;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position);
    }

    private void UpdateLinePosition()
    {
        _lineRenderer.SetPosition(1, transform.position);
    }
}
