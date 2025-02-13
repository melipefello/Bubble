using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Bubble : MonoBehaviour
{
    public event Action Grew;
    public event Action<Bubble> Died;
    public event Action Released;
    public event Action Initialized;

    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] CircleCollider2D _collider;

    BubbleSettings _settings;
    BubbleState _currentState;

    public float Radius => GetRadius(Size);
    public Vector2 Position => _rigidbody.position;
    public Quaternion Rotation => transform.rotation;
    public Object GameObject => gameObject;

    public int Size { get; private set; } = 1;

    void Awake()
    {
        _collider.enabled = false;
        _rigidbody.simulated = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        _currentState.HandleCollision(other);
    }

    public void Initialize(BubbleSettings settings)
    {
        _collider.enabled = true;
        _rigidbody.simulated = true;
        _settings = settings;
        SetState(new BubbleInflatingState(this, _rigidbody, _settings.GrowTime));
        Initialized?.Invoke();
    }

    public void Dispose()
    {
        Grew = null;
        Died = null;
    }

    public void Die()
    {
        Died?.Invoke(this);
    }

    public void Tick()
    {
        _currentState.Tick();
    }

    public void MoveTowards(Vector2 direction)
    {
        _rigidbody.velocity = direction;
    }

    public void Release()
    {
        SetState(new BubbleFloatingState(_rigidbody));
        Released?.Invoke();
    }

    public float GetRadius(int level) => level * _settings.SizeStep / 2f + _settings.MinimumRadius;

    public void Grow()
    {
        Size++;
        _collider.radius = Radius;
        Grew?.Invoke();
    }

    void SetState(BubbleState state)
    {
        _currentState = state;
        _currentState.Enter();
    }
}