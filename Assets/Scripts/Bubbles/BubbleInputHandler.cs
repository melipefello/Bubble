using JetBrains.Annotations;
using UnityEngine;

public class BubbleInputHandler
{
    readonly IInputSystem _inputSystem;
    readonly BubbleHandler _bubbleHandler;
    readonly Bounds _bounds;
    readonly BubbleHandlerSettings _settings;
    [CanBeNull] Bubble _currentBubble;

    public BubbleInputHandler(IInputSystem inputSystem, BubbleHandler bubbleHandler, Bounds bounds,
        BubbleHandlerSettings settings)
    {
        _inputSystem = inputSystem;
        _bubbleHandler = bubbleHandler;
        _bounds = bounds;
        _settings = settings;
    }

    public void Initialize()
    {
        _inputSystem.Pressed += OnInputPressed;
        _inputSystem.Released += OnInputReleased;
    }

    public void Dispose()
    {
        _inputSystem.Released -= OnInputReleased;
        _inputSystem.Pressed -= OnInputPressed;
    }

    public void Tick()
    {
        if (_currentBubble != null)
            MoveTowardsInput(_currentBubble);
    }

    void OnInputPressed()
    {
        var inputPosition = _inputSystem.GetPosition();
        var inputWithinBounds = _bounds.Contains(inputPosition);
        if (!inputWithinBounds)
            return;

        var isOverCollider = Physics2D.OverlapPoint(inputPosition) != null;
        if (isOverCollider)
            return;

        _currentBubble = _bubbleHandler.CreateBubble(inputPosition);
    }

    void OnInputReleased()
    {
        if (_currentBubble != null)
            _currentBubble.Release();

        _currentBubble = null;
    }

    void MoveTowardsInput(Bubble bubble)
    {
        var rawDirection = _inputSystem.GetPosition() - bubble.Position;
        var direction = Vector2.ClampMagnitude(rawDirection, _settings.MaxDragSpeed);
        bubble.MoveTowards(direction);
    }
}