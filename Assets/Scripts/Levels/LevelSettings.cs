using UnityEngine;

[CreateAssetMenu]
class LevelSettings : ScriptableObject
{
    [field: SerializeField]
    public int MovePenalty { get; private set; } = 3;

    [field: SerializeField]
    public float CameraMargin { get; private set; } = 4;

    [field: SerializeField]
    public BubbleHandlerSettings BubbleHandlerSettings { get; private set; }

    [field: SerializeField]
    public ObstacleHandlerSettings ObstacleHandlerSettings { get; private set; }
}