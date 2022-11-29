using UnityEngine;

public class PlayerMissileCounter : MonoBehaviour
{
    [SerializeField] private int _leftMissile = 10;

    public int GetLeftMissile()
    {
        return _leftMissile;
    }
    
    public void SetLeftMissile(int amount)
    {
        _leftMissile = amount;
    }

    public void DecreaseMissileAmount(int amount)
    {
        _leftMissile -= amount;
    }

    public void IncreaseMissileAmount(int amout)
    {
        _leftMissile += amout;
    }
}
