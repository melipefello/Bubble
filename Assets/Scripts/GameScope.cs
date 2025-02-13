using UnityEngine;

class GameScope : MonoBehaviour
{
    [SerializeField] GameSettings _settings;

    GamePresenter _presenter;
    LevelScope _levelScope;
    GameModel _model;

    void Awake()
    {
        Application.targetFrameRate = 60;
        _presenter = GetComponent<GamePresenter>();
    }

    void Start()
    {
        _model = new GameModel();
        _presenter.Initialize(OnDismissed);
        _levelScope = Instantiate(_settings.LevelScopePrefab);
        _levelScope.Initialize(GetLevelInfo(), OnLevelCompleted, OnLevelFailed);
    }

    void OnLevelCompleted()
    {
        _model.IncreaseProgress();
        _presenter.ShowVictory();
        _levelScope.Dispose();
    }

    void OnLevelFailed()
    {
        _model.ResetProgress();
        _presenter.ShowFailure();
        _levelScope.Dispose();
    }

    void OnDismissed()
    {
        _presenter.HidePanels();
        _levelScope.Initialize(GetLevelInfo(), OnLevelCompleted, OnLevelFailed);
    }

    LevelInfo GetLevelInfo()
    {
        var settingsIndex = _model.LevelProgress % _settings.LevelInfos.Length;
        return _settings.LevelInfos[settingsIndex];
    }
}