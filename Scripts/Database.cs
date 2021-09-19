using System.Collections.Generic;
using UnityEngine;

public static class Database
{
    public static class GameplayData
    {
        public static LevelResult levelResult = null;
        public static AudioClip audioClip = null;
        public static NoteMap noteMap = null;
    }

    public static class GameData
    {
        public static LevelPack[] levelpacks
        {
            get
            {
                LevelPack[] packs = Resources.LoadAll<LevelPack>("LevelPacks");
                return packs;
            }
        }

        public static LevelPack levelPack = null;

        public static LevelData[] levelDatas
        {
            get
            {
                return levelPack.levelDatas;
            }
        }
    }

}