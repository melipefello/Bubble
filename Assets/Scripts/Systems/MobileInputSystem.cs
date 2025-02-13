using System;
using UnityEngine;

public class MobileInputSystem : IInputSystem
{
    public event Action Pressed;
    public event Action Released;
    readonly Camera _camera;

    public MobileInputSystem(Camera camera)
    {
        _camera = camera;
    }

    public Vector2 GetPosition()
    {
        var position = Vector2.zero;
        if (Input.touchCount > 0)
            position = Input.GetTouch(0).position;

        return _camera.ScreenToWorldPoint(position);
    }

    public void Tick()
    {
        if (Input.touchCount <= 0)
            return;

        var touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
            Pressed?.Invoke();
        else if (touch.phase == TouchPhase.Ended)
            Released?.Invoke();
    }
}