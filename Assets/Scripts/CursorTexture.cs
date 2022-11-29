using UnityEngine;

public class CursorTexture : MonoBehaviour
{
    [SerializeField] private Texture2D _cursorTexture;
    [SerializeField] private CursorMode _cursorMode = CursorMode.Auto;
    private Vector2 _hotSpot = Vector2.zero;

    private void Start()
    {
        SetCursorTexture();
    }

    private void SetCursorTexture()
    {
        _hotSpot = new Vector2(_cursorTexture.width / 2f, _cursorTexture.height / 2f);
        Cursor.SetCursor(_cursorTexture, _hotSpot, _cursorMode);
    }
}
