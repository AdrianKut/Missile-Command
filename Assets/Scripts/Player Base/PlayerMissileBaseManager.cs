using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MouseInput))]
public class PlayerMissileBaseManager : Singleton<PlayerMissileBaseManager>
{
    [SerializeField] private GameObject _playerBase;
    [SerializeField] private List<PlayerMissileBase> _listPlayerBases;
    [SerializeField] private Transform _spawnYOffset;

    private MouseInput _mouseInput;
    private GameManager _gameManager;

    protected override void Awake()
    {
        base.Awake();
        _mouseInput = GetComponent<MouseInput>();
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.OnPauseGame += PauseGame;
        _gameManager.OnStartGame += StartGame;
        _gameManager.OnNextLevel += RestartBaseVariables;
    }

    private void OnDisable()
    {
        _gameManager.OnPauseGame -= PauseGame;
        _gameManager.OnStartGame -= StartGame;
        _gameManager.OnNextLevel -= RestartBaseVariables;
    }

    private void Update()
    {
        SpawnMissileOnMousePosition();
    }

    public void StartGame()
    {
        _playerBase.SetActive(true);
    }

    public void PauseGame()
    {
        _playerBase.SetActive(false);
    }

    private void RestartBaseVariables()
    {
        for (int i = 0; i < _listPlayerBases.Count; i++)
        {
            _listPlayerBases[i].SetStartingVariables();
        }
    }

    private void SpawnMissileOnMousePosition()
    {
        if (_gameManager.GetChangeState() == GameState.StartGame
            && Input.GetKeyDown(KeyCode.Mouse0)
            && _mouseInput.GetMousePosition().y >= _spawnYOffset.localPosition.y)
        {
            ShootFromNearestBase();
        }
    }

    private void ShootFromNearestBase()
    {
        PlayerMissileBase nearestBaseGameObject = null;
        float minDistance = float.MaxValue;

        for (int i = 0; i < _listPlayerBases.Count; i++)
        {
            if (_listPlayerBases[i].IsEnabled == false)
                continue;

            float currentBaseDistance = Vector3.Distance(_mouseInput.GetMousePosition(), _listPlayerBases[i].transform.position);

            if (minDistance >= currentBaseDistance)
            {
                minDistance = currentBaseDistance;
                nearestBaseGameObject = _listPlayerBases[i];
            }
        }
        
        if (nearestBaseGameObject != null)
        {
            PlayerMissileBase playerMissileBase = nearestBaseGameObject.GetComponent<PlayerMissileBase>();
            playerMissileBase.Shoot();
        }
    }
}
