using UnityEngine;

abstract class BubbleState
{
    public abstract void Enter();
    public abstract void Tick();
    public abstract void HandleCollision(Collision2D other);
}