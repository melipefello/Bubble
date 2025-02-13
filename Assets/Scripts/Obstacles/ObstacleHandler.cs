using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler
{
    readonly List<Obstacle> _obstacles = new();
    readonly ObstacleHandlerSettings _settings;

    public ObstacleHandler(ObstacleHandlerSettings settings)
    {
        _settings = settings;
    }

    public void Initialize(Vector2[] obstaclePoints)
    {
        foreach (var point in obstaclePoints)
        {
            var obstacle = CreateObstacle(point);
            obstacle.Move(Random.insideUnitCircle.normalized);
        }
    }

    public void Dispose()
    {
        foreach (var obstacle in _obstacles)
            DisposeInstance(obstacle);

        _obstacles.Clear();
    }

    public void Tick()
    {
        foreach (var obstacle in _obstacles)
            obstacle.Tick();
    }

    void OnObstacleDied(Obstacle obstacle)
    {
        DisposeInstance(obstacle);
        _obstacles.Remove(obstacle);
    }

    void DisposeInstance(Obstacle obstacle)
    {
        obstacle.Died -= OnObstacleDied;
        obstacle.Dispose();
        Object.Destroy(obstacle.gameObject);
    }

    Obstacle CreateObstacle(Vector2 position)
    {
        var obstacle = Object.Instantiate(_settings.ObstaclePrefab, position, Quaternion.identity);
        obstacle.Died += OnObstacleDied;
        obstacle.Initialize(_settings.ObstacleSettings);
        _obstacles.Add(obstacle);
        return obstacle;
    }
}