using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    [Header("Game Over UI")]
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private TextMeshProUGUI _scoreGameOverText;

    [Header("Next Level UI")]
    [SerializeField] private GameObject _nextLevelUI;
    [SerializeField] private TextMeshProUGUI _scoreNextLevelText;

    [Header("Stage")]
    [SerializeField] private TextMeshProUGUI _currentStageText;

    private int _currentStageCounter = 1;
    private int _currentScore;
    private GameManager _gameManager;
    private SaveData _saveData = SaveData.Instance;
    private void Start()
    {
        _saveData = SaveData.Instance;
        _gameManager = GameManager.Instance;
        _gameManager.OnGameOver += ShowGameOverUI;
        _gameManager.OnNextLevel += ShowNextLevelUI;
        _saveData.OnScoreUpdate += UpdateTextScore;

        _bestScoreText.text = _saveData.BestScore.ToString();
    }

    private void OnDisable()
    {
        _gameManager.OnGameOver -= ShowGameOverUI;
        _gameManager.OnNextLevel -= ShowNextLevelUI;
        _saveData.OnScoreUpdate -= UpdateTextScore;
    }

    public void RestartGameOnClick()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        _nextLevelUI.SetActive(false);
        _gameManager.ChangeGameState(GameState.StartGame);
    }

    private void UpdateTextScore()
    {
        if (_gameManager.GetChangeState() == GameState.StartGame)
        {
            _currentScore = _saveData.CurrentScore;
            _scoreText.text = _currentScore.ToString();
        }
    }

    private void ShowGameOverUI()
    {
        if (_currentScore >= _saveData.BestScore)
        {
            _saveData.BestScore = _currentScore;
            _saveData.Save();

            _scoreGameOverText.text = "NEW BEST SCORE: " + _currentScore;
        }
        else
        {
            int bestScore = _saveData.BestScore;
            _scoreGameOverText.text = "YOUR SCORE: " + _currentScore + "\nBEST SCORE: " + bestScore;
        }

        _gameOverUI.SetActive(true);
    }

    private void ShowNextLevelUI()
    {
        SaveData saveData = SaveData.Instance;

        _nextLevelUI.SetActive(true);
        _scoreNextLevelText.text = "BONUS POINTS: " + saveData.BonusPoints +
            "\nCURRENT SCORE: " + (_currentScore + saveData.BonusPoints);

        saveData.CurrentScore += saveData.BonusPoints;
        saveData.BonusPoints = 0;
        saveData.Save();
        _currentScore = saveData.CurrentScore;

        _currentStageText.text = $"{++_currentStageCounter}";

        UpdateTextScore();
    }
}
