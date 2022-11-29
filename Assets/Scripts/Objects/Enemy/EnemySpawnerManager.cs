using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemiesCounterPerStage))]
public class EnemySpawnerManager : Singleton<EnemySpawnerManager>
{
    [Header("Enemies")]
    [SerializeField] private GameObject _airplanePrefab;
    [SerializeField] private GameObject _missilePrefab;
    [SerializeField, Range(1, 10)] private float _spawnAirplaneDelay;
    [SerializeField, Range(1, 10)] private float _spawnMissileDelay;

    [Header("Spawn points")]
    [SerializeField] private Vector2 _leftCorner;
    [SerializeField] private Vector2 _rightCorner;

    [Header("Targets")]
    [SerializeField] private List<GameObject> _targetsEnemies;

    [SerializeField] private int _leftEnemies;

    private EnemiesCounterPerStage _enemiesCounter;
    private GameManager _gameManager;

    override protected void Awake()
    {
        base.Awake();
        _enemiesCounter = GetComponent<EnemiesCounterPerStage>();
    }

    private void Start()
    {
        SpawnNewEnemies();

        _leftEnemies = _enemiesCounter.GetLeftEnemies();
        _gameManager = GameManager.Instance;
        _gameManager.OnNextLevel += SpawnNewEnemies;
    }

    private void OnDisable()
    {
        _gameManager.OnNextLevel -= SpawnNewEnemies;
    }

    private void SpawnNewEnemies()
    {
        StartCoroutine(InstantiateEnemyMissile());
        StartCoroutine(InstantiateAirplane());
    }

    private IEnumerator InstantiateAirplane()
    {
        int amountAirplane = _enemiesCounter.GetLeftAirplane();
        for (int i = 0; i < amountAirplane; i++)
        {
            yield return new WaitForSeconds(_spawnAirplaneDelay);

            Vector2 targetVector = Vector2.zero;
            Vector2 spawnPosition = Vector2.zero;
            int randomVector = Random.Range(0, 2);

            if (randomVector == 0)  // from right to left
            {
                targetVector = Vector2.left;
                spawnPosition = new Vector2(_rightCorner.x + 2, _rightCorner.y - 5);
            }
            else // from left to right
            {
                targetVector = Vector2.right;
                spawnPosition = new Vector2(_leftCorner.x - 2, _leftCorner.y - 5);
            }

            InstantiateEnemy(_airplanePrefab, spawnPosition, targetVector);
        }
    }

    private IEnumerator InstantiateEnemyMissile()
    {
        int amountMissile = _enemiesCounter.GetLeftMissile();
        for (int i = 0; i < amountMissile; i++)
        {
            yield return new WaitForSeconds(_spawnMissileDelay);

            float randomX = Random.Range(_leftCorner.x, _rightCorner.x);
            float randomY = Random.Range(_leftCorner.y, _rightCorner.y);
            int randomTarget = Random.Range(0, _targetsEnemies.Count);

            Vector2 randomPosition = new Vector2(randomX, randomY);
            var targetPosition = _targetsEnemies[randomTarget].transform.position;

            InstantiateEnemy(_missilePrefab, randomPosition, targetPosition);
        }
    }

    private void InstantiateEnemy(GameObject gameObject, Vector2 position, Vector2 targetPosition)
    {
        var newEnemy = Instantiate(gameObject, position, Quaternion.identity);
        newEnemy.GetComponent<Enemy>().SetTargetPosition(targetPosition);
    }

    public void EnemyDied()
    {
        if (_gameManager.GetChangeState() != GameState.GameOver)
        {
            _leftEnemies--;
            TryFinishStage();
        }
    }

    private void TryFinishStage()
    {
        if (_leftEnemies == 0)
        {
            _enemiesCounter.IncreaseAmountOfEnemies();
            _leftEnemies = _enemiesCounter.GetLeftEnemies();
            _gameManager.ChangeGameState(GameState.NextLevel);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftCorner, _rightCorner); //To check enemy missile spawn position
    }
}
