using System;
using UnityEngine;

public class LevelScope : MonoBehaviour
{
    [SerializeField] LevelSettings _settings;
    [SerializeField] EdgeCollider2D _collider;
    [SerializeField] Camera _camera;

    Action _levelCompleted;
    Action _levelFailed;
    LevelPresenter _presenter;
    LevelModel _model;
    IInputSystem _inputSystem;
    BubbleHandler _bubbleHandler;
    ObstacleHandler _obstacleHandler;
    BubbleInputHandler _bubbleInputHandler;

    void Awake()
    {
        _presenter = GetComponent<LevelPresenter>();
        _camera.orthographicSize = GetCameraSize(_collider.bounds);
#if UNITY_EDITOR
        _inputSystem = new EditorInputSystem(_camera);
#else
        _inputSystem = new MobileInputSystem(_camera);
#endif
    }

    void Update()
    {
        _inputSystem.Tick();
        _bubbleInputHandler.Tick();
        _bubbleHandler.Tick();
        _obstacleHandler.Tick();
    }

    public void Initialize(LevelInfo levelInfo, Action levelCompleted, Action levelFailed)
    {
        _levelFailed = levelFailed;
        _levelCompleted = levelCompleted;
        _model = new LevelModel(levelInfo);
        _bubbleHandler = new BubbleHandler(_settings.BubbleHandlerSettings);
        _bubbleHandler.BubbleDied += OnBubbleDied;
        _bubbleHandler.BubbleReleased += OnBubbleReleased;
        _bubbleInputHandler = new BubbleInputHandler(_inputSystem,
            _bubbleHandler,
            _collider.bounds,
            _settings.BubbleHandlerSettings);

        _bubbleInputHandler.Initialize();
        _obstacleHandler = new ObstacleHandler(_settings.ObstacleHandlerSettings);
        _obstacleHandler.Initialize(levelInfo.ObstaclePoints);
        _presenter.UpdateMoves(_model);
        _presenter.UpdateScore(_model);
    }

    public void Dispose()
    {
        _bubbleHandler.BubbleDied -= OnBubbleDied;
        _bubbleHandler.BubbleReleased -= OnBubbleReleased;
        _bubbleInputHandler.Dispose();
        _bubbleHandler.Dispose();
        _obstacleHandler.Dispose();
    }

    float GetCameraSize(Bounds colliderBounds)
    {
        var size = colliderBounds.size.x / _camera.aspect / 2f;
        return _settings.CameraMargin + size;
    }

    void OnBubbleDied()
    {
        _model.DecreaseMove(_settings.MovePenalty);
        _presenter.UpdateMoves(_model);
        if (_model.RemainingMoves <= 0)
            _levelFailed?.Invoke();
    }

    void OnBubbleReleased()
    {
        _model.SetScore(_bubbleHandler.TotalSize);
        _model.DecreaseMove(1);
        _presenter.UpdateScore(_model);
        _presenter.UpdateMoves(_model);
        CheckVictoryCondition();
    }

    void CheckVictoryCondition()
    {
        if (_model.Score >= _model.RequiredScore)
            _levelCompleted?.Invoke();
        else if (_model.RemainingMoves <= 0)
            _levelFailed?.Invoke();
    }
}