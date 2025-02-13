using System;
using UnityEngine;

[Serializable]
public class ObstacleHandlerSettings
{
    [field: SerializeField]
    public Obstacle ObstaclePrefab { get; private set; }

    [field: SerializeField]
    public ObstacleSettings ObstacleSettings { get; private set; }
}