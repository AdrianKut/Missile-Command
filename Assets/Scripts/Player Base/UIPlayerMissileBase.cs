using TMPro;
using UnityEngine;

public class UIPlayerMissileBase : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMissileCounter;

    public void SetText(string text)
    {
        _textMissileCounter.text = text;
    }
}
