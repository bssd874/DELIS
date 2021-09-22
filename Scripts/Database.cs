using System.Collections.Generic;
using UnityEngine;

public static class Database
{
    public static class GameplayData
    {
        public static LevelData levelData;
        public static LevelResult levelResult = null;
        public static AudioClip audioClip
        {
            get
            {
                return levelData.audioClip;
            }
        }
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

    public static class UserData
    {
        public static float skills = 0;
        public static float _skills
        {
            get
            {
                float value = 0;
                int total = 0;
                foreach(LevelPack levelPack in GameData.levelpacks)
                {
                    value += levelPack.skills;
                    total += 1;
                }
                skills = value/total;
                return value / total;
            }
        }
    }


}