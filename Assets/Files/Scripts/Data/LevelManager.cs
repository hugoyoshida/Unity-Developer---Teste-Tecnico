using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelDatabase levelDatabase;

    public LevelData GetLevelData(int level)
    {
        foreach (var levelData in levelDatabase.levels)
        {
            if (levelData.level == level)
                return levelData;
        }

        Debug.LogError($"Level {level} not found!");
        return null;
    }
}
