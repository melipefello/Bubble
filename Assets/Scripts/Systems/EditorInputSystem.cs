using System;
using UnityEngine;

public class EditorInputSystem : IInputSystem
{
    public event Action Pressed;
    public event Action Released;
    readonly Camera _camera;

    public EditorInputSystem(Camera camera)
    {
        _camera = camera;
    }

    public Vector2 GetPosition() => _camera.ScreenToWorldPoint(Input.mousePosition);

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
            Pressed?.Invoke();

        if (Input.GetMouseButtonUp(0))
            Released?.Invoke();
    }
}