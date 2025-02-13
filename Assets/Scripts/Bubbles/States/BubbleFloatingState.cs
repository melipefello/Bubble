using UnityEngine;

class BubbleFloatingState : BubbleState
{
    readonly Rigidbody2D _rigidbody;

    public BubbleFloatingState(Rigidbody2D rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public override void Enter()
    {
        _rigidbody.gravityScale = -1;
    }

    public override void Tick() { }

    public override void HandleCollision(Collision2D other)
    {
        var direction = other.contacts[0].normal.normalized;
        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            obstacle.Move(-direction);
            obstacle.TakeDamage();
        }
    }
}