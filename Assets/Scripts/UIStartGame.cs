using UnityEngine;

public class UIStartGame : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void StartGameOnClick()
    {
        _gameManager.ChangeGameState(GameState.StartGame);
        gameObject.SetActive(false);
    }
}
