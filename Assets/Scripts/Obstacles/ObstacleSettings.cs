using System;
using UnityEngine;

[Serializable]
public class ObstacleSettings
{
    [field: SerializeField]
    public int MaxHealth { get; private set; }

    [field: SerializeField]
    public float RegenerationTime { get; private set; }

    [field: SerializeField]
    public float Speed { get; private set; }
}