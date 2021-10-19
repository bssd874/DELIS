using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "LevelSystem/LevelPack")]
public class LevelPack : ScriptableObject
{
    public string packName;
    public int cost;
    public Data data = new Data();

    public LevelData[] levelDatas;

    private void OnValidate()
    {
        packName = name;
    }


    public class Data
    {
        public bool purchased = false;
    }

    [ContextMenu("Load All Level Data")]
    public void LoadAllLevelData()
    {
        this.LoadLevelData();
    }

}
