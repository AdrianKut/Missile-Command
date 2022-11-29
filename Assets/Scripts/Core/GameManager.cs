using System;
using System.Linq;
using UnityEngine;

public enum GameState
{
    Pause,
    StartGame,
    GameOver,
    NextLevel
}

public class GameManager : Singleton<GameManager>
{
    public Action OnPauseGame;
    public Action OnStartGame;
    public Action OnGameOver;
    public Action OnNextLevel;

    public GameState _gameState;

    public GameState GetChangeState()
    {
        return _gameState;
    }

    private void Start()
    {
        Pause();
    }

    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Pause:
                Pause();
                break;
            case GameState.StartGame:
                StartGame();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.NextLevel:
                NextLevel();
                break;
        }

        _gameState = state;
    }

    private void Pause()
    {
        OnPauseGame?.Invoke();
        Time.timeScale = 0f;
    }

    private void StartGame()
    {
        OnStartGame?.Invoke();
        Time.timeScale = 1f;
    }

    private void GameOver()
    {
        OnGameOver?.Invoke();

        DestroyDestroyableObjectsOnScene("Enemy");
        DestroyDestroyableObjectsOnScene("Player");

        Time.timeScale = 0f;
    }

    private void NextLevel()
    {
        OnNextLevel?.Invoke();

        DestroyDestroyableObjectsOnScene("Enemy");
        DestroyDestroyableObjectsOnScene("Player");

        Time.timeScale = 0f;
    }

    private void DestroyDestroyableObjectsOnScene(string tag)
    {
        foreach (GameObject destroyableObject in GameObject.FindGameObjectsWithTag(tag))
        {
            if (destroyableObject.GetComponent(typeof(IDestroyableObject)))
            {
                Destroy(destroyableObject);
            }
        }
    }
}
