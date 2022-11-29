using System.Collections.Generic;
using UnityEngine;

public class CityManager : Singleton<CityManager>
{
    [SerializeField] private List<City> _cities;
    [SerializeField] private int _bonusPointsPerCity;

    private GameManager _gameManager;

    protected override void Awake()
    {
        base.Awake();
        _gameManager = GameManager.Instance;
        _gameManager.OnNextLevel += RestartCitiesVariabiles;
    }

    private void OnDisable()
    {
        _gameManager.OnNextLevel -= RestartCitiesVariabiles;
    }

    public void CheckEnabledCities()
    {
        var currentDestroyedCity = 0;
        foreach (var city in _cities)
        {
            if (city.IsEnabled == false)
                currentDestroyedCity++;
        }

        if (currentDestroyedCity == _cities.Count)
        {
            _gameManager.ChangeGameState(GameState.GameOver);
        }
    }

    public void RestartCitiesVariabiles()
    {
        SaveData saveData = SaveData.Instance;

        for (int i = 0; i < _cities.Count; i++)
        {
            if (_cities[i].IsEnabled)
            {
                saveData.BonusPoints += _bonusPointsPerCity;
            }
            _cities[i].SetStartingVariable();
        }
    }
}
