using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private Vector3 _mouseInputPosition;

    public Vector3 GetMousePosition()
    {
        return _mouseInputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
