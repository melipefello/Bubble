using UnityEngine;

[CreateAssetMenu]
class GameSettings : ScriptableObject
{
    [field: SerializeField]
    public LevelScope LevelScopePrefab { get; private set; }

    [field: SerializeField]
    public LevelInfo[] LevelInfos { get; private set; }
}