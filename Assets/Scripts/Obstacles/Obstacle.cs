using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public event Action HealthChanged;
    public event Action<Obstacle> Died;
    public event Action Initialized;

    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] CircleCollider2D _collider;

    int _health;
    ObstacleSettings _settings;
    float _lastDamageTime;

    public Vector2 Position => _rigidbody.position;
    public Quaternion Rotation => transform.rotation;
    public float HeathPercentage => (float) _health / _settings.MaxHealth;

    void Awake()
    {
        _collider.enabled = false;
        _rigidbody.simulated = false;
    }

    public void Initialize(ObstacleSettings settings)
    {
        _collider.enabled = true;
        _rigidbody.simulated = true;
        _settings = settings;
        _health = settings.MaxHealth;
        Initialized?.Invoke();
    }

    public void Dispose()
    {
        HealthChanged = null;
        Died = null;
    }

    public void Tick()
    {
        _rigidbody.velocity = _rigidbody.velocity.normalized * _settings.Speed;
        HandleHealthRegeneration();
    }

    public void TakeDamage()
    {
        _health--;
        _lastDamageTime = Time.time;
        HealthChanged?.Invoke();
        var isHealthNegative = _health < 0;
        if (isHealthNegative)
            Died?.Invoke(this);
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.velocity = direction * _settings.Speed;
    }

    void HandleHealthRegeneration()
    {
        var timeSinceLastDamage = Time.time - _lastDamageTime;
        var isFullHealth = _health == _settings.MaxHealth;
        if (!isFullHealth && timeSinceLastDamage > _settings.RegenerationTime)
        {
            _health = _settings.MaxHealth;
            HealthChanged?.Invoke();
        }
    }
}