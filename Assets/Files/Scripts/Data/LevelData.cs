using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game Data/Level")]
public class LevelData : ScriptableObject
{
    public int level;
    public int cost;
    public int capacity;
    public float bonus;
    public Color color;
}
