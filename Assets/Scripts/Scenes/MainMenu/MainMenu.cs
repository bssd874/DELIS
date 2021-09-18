using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public LevelPack[] levelPacks;

    private void Start()
    {
        levelPacks = LevelPack.GetLevelPacks();
    }
}