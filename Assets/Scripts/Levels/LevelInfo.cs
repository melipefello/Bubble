using System;
using UnityEngine;

[Serializable]
public class LevelInfo
{
    [field: SerializeField]
    public int MovesLimit { get; private set; }

    [field: SerializeField]
    public int RequiredScore { get; private set; }

    [field: SerializeField]
    public Vector2[] ObstaclePoints { get; private set; }
}