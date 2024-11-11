using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Game Data/Level Database")]
public class LevelDatabase : ScriptableObject
{
    [Header("List of Levels")]
    public LevelData[] levels;
}
