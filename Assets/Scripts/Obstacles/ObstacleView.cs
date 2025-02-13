using UnityEngine;

[RequireComponent(typeof(Obstacle))]
class ObstacleView : MonoBehaviour
{
    [SerializeField] Transform _healthFillRoot; //Removed Shapes module

    Obstacle _obstacle;

    void Awake()
    {
        _obstacle = GetComponent<Obstacle>();
        _obstacle.Initialized += OnInitialized;
        _obstacle.HealthChanged += UpdateHealth;
    }

    void LateUpdate()
    {
        UpdateTransform();
    }

    void OnDestroy()
    {
        _obstacle.Initialized -= OnInitialized;
        _obstacle.HealthChanged -= UpdateHealth;
    }

    void OnInitialized()
    {
        UpdateHealth();
        UpdateTransform();
    }

    void UpdateTransform()
    {
        transform.SetPositionAndRotation(_obstacle.Position, _obstacle.Rotation);
    }

    void UpdateHealth()
    {
        _healthFillRoot.localScale = _obstacle.HeathPercentage * Vector3.one;
    }
}