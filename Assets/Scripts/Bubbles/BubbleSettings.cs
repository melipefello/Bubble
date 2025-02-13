using System;
using UnityEngine;

[Serializable]
public class BubbleSettings
{
    [field: SerializeField]
    public float GrowTime { get; private set; }

    [field: SerializeField]
    public float SizeStep { get; private set; }

    [field: SerializeField]
    public float MinimumRadius { get; private set; }
}