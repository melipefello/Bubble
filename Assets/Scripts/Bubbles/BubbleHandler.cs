using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class BubbleHandler
{
    public event Action BubbleReleased;
    public event Action BubbleDied;

    readonly BubbleHandlerSettings _settings;
    readonly List<Bubble> _bubbles = new();

    public int TotalSize => _bubbles.Sum(bubble => bubble.Size);

    public BubbleHandler(BubbleHandlerSettings settings)
    {
        _settings = settings;
    }

    public void Dispose()
    {
        foreach (var bubble in _bubbles)
            DisposeInstance(bubble);

        _bubbles.Clear();
    }

    public void Tick()
    {
        foreach (var bubble in _bubbles)
            bubble.Tick();
    }

    public Bubble CreateBubble(Vector2 position)
    {
        var bubble = Object.Instantiate(_settings.BubblePrefab, position, Quaternion.identity);
        bubble.Initialize(_settings.BubbleSettings);
        bubble.Died += OnBubbleDied;
        bubble.Released += OnBubbleReleased;
        _bubbles.Add(bubble);
        return bubble;
    }

    void DisposeInstance(Bubble bubble)
    {
        bubble.Died -= OnBubbleDied;
        bubble.Released -= OnBubbleReleased;
        bubble.Dispose();
        Object.Destroy(bubble.GameObject);
    }

    void OnBubbleDied(Bubble bubble)
    {
        DisposeInstance(bubble);
        _bubbles.Remove(bubble);
        BubbleDied?.Invoke();
    }

    void OnBubbleReleased()
    {
        BubbleReleased?.Invoke();
    }
}