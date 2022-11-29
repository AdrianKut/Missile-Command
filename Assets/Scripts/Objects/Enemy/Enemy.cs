using UnityEngine;

public abstract class Enemy : MovingObjects
{
    [SerializeField] private int _scoreToGet;

    private SaveData _saveData;
    private EnemySpawnerManager _enemySpawner;

    private void Start()
    {
        _saveData = SaveData.Instance;
        _enemySpawner = EnemySpawnerManager.Instance;
    }

    public void IncreaseScore()
    {
        _saveData.CurrentScore += _scoreToGet;
        _saveData.Save();
        _saveData.OnScoreUpdate?.Invoke();
    }

    private void OnDestroy()
    {
        IncreaseScore();
        _enemySpawner.EnemyDied();
    }
}
