using System;
using UnityEngine;

[Serializable]
public class BubbleHandlerSettings
{
    [field: SerializeField]
    public Bubble BubblePrefab { get; private set; }

    [field: SerializeField]
    public BubbleSettings BubbleSettings { get; private set; }

    [field: SerializeField]
    public float MaxDragSpeed { get; private set; }
}