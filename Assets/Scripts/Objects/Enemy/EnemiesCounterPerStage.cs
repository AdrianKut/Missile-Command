using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpawnCounter
{
    protected int _amountOnStart;
    protected int _increasePerStage;

    protected EnemySpawnCounter(int amountEnemiesOnStart, int increaseEnemiesPerStage)
    {
        _amountOnStart = amountEnemiesOnStart;
        _increasePerStage = increaseEnemiesPerStage;
    }

    public virtual void IncreaseAmountOfEnemies()
    {
        _amountOnStart += _increasePerStage;
    }

    public int GetAmountOnStart()
    {
        return _amountOnStart;
    }
}

public class MissileSpawnCounter : EnemySpawnCounter
{
    public MissileSpawnCounter(int amountEnemiesOnStart, int increaseEnemiesPerStage) :
        base(amountEnemiesOnStart, increaseEnemiesPerStage)
    {

    }
}

public class AirplaneSpawnCounter : EnemySpawnCounter
{
    public AirplaneSpawnCounter(int amountEnemiesOnStart, int increaseEnemiesPerStage) :
        base(amountEnemiesOnStart, increaseEnemiesPerStage)
    {

    }
}

public class EnemiesCounterPerStage : MonoBehaviour
{
    [Header("Missile")]
    [SerializeField] private int _amountMissile;
    [SerializeField] private int _increaseMissilePerStage;

    [Header("Airplane")]
    [SerializeField] private int _amountAirplane;
    [SerializeField] private int _increaseAirplanePerStage;

    private MissileSpawnCounter _missile;
    private AirplaneSpawnCounter _airplane;

    public List<EnemySpawnCounter> _listEnemiesCounter;

    private void Awake()
    {
        InitialListEnemiesCounter();
    }

    private void InitialListEnemiesCounter()
    {
        _missile = new MissileSpawnCounter(_amountMissile, _increaseMissilePerStage);
        _airplane = new AirplaneSpawnCounter(_amountAirplane, _increaseAirplanePerStage);

        _listEnemiesCounter = new List<EnemySpawnCounter>
        {
            _missile,
            _airplane
        };
    }

    public int GetLeftMissile()
    {
        return _missile.GetAmountOnStart();
    }

    public int GetLeftAirplane()
    {
        return _airplane.GetAmountOnStart();
    }

    public int GetLeftEnemies()
    {
        int amount = 0;
        
        foreach (var item in _listEnemiesCounter)
        {
            amount += item.GetAmountOnStart();
        }

        return amount;
    }

    public void IncreaseAmountOfEnemies()
    {
        foreach (var item in _listEnemiesCounter)
        {
            item.IncreaseAmountOfEnemies();
        }
    }
}
