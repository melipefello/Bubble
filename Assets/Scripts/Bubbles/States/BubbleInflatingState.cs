using UnityEngine;

class BubbleInflatingState : BubbleState
{
    readonly Bubble _bubble;
    readonly Rigidbody2D _rigidbody;
    readonly float _growTime;
    readonly Collider2D[] _overlaps = new Collider2D[2];
    float _inflateTime;

    public BubbleInflatingState(Bubble bubble, Rigidbody2D rigidbody, float growTime)
    {
        _bubble = bubble;
        _rigidbody = rigidbody;
        _growTime = growTime;
    }

    public override void Enter()
    {
        _inflateTime = 0;
        _rigidbody.gravityScale = 0;
    }

    public override void Tick()
    {
        _inflateTime += Time.deltaTime;
        HandleGrowth();
    }

    public override void HandleCollision(Collision2D other)
    {
        if (other.gameObject.GetComponent<Obstacle>())
            _bubble.Die();
    }

    void HandleGrowth()
    {
        while (_inflateTime > _growTime)
        {
            if (!CanGrow())
            {
                _inflateTime = 0;
                break;
            }

            _inflateTime -= _growTime;
            _bubble.Grow();
        }
    }

    bool CanGrow()
    {
        var radius = _bubble.GetRadius(_bubble.Size + 1);
        var overlaps = Physics2D.OverlapCircleNonAlloc(_bubble.Position, radius, _overlaps);
        var willCollideWithOthers = overlaps > 1;
        return !willCollideWithOthers;
    }
}