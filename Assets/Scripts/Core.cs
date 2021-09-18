public static class Core
{
    public static LevelPack[] levelPacks = null;

    public static LevelPack[] getLevelPacks
    {
        set
        {
            levelPacks = value;
        }
        get
        {
            if (levelPacks == null)
            {
                levelPacks = LevelPack.GetLevelPacks();
            }

            return levelPacks;
        }
    }
}