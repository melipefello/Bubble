using System;
using UnityEngine;

public interface IInputSystem
{
    event Action Pressed;
    event Action Released;
    Vector2 GetPosition();
    void Tick();
}