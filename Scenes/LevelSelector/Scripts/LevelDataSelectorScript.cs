using UnityEngine;
public class LevelDataSelectorScript : MonoBehaviour
{
    public static LevelDataSelectorScript main;
    public static LevelData levelData;
    public static LevelData[] _LevelDatas
    {
        get
        {
            return LevelSelectorScript._levelPack.levelDatas;
        }
    }

    public LevelDataPanelScript[] levelDataPanelScripts;

    public GameObject Panel;
    public RectTransform Grid;

    void Awake()
    {
        main = this;
        LevelSelectorScript.onUpdateLevelPack.AddListener(UpdatePanelData);
    }

    public void UpdatePanelData()
    {
        foreach (LevelDataPanelScript levelDataPanelScript in levelDataPanelScripts)
        {
            Destroy(levelDataPanelScript.gameObject);
        }
        levelDataPanelScripts = new LevelDataPanelScript[_LevelDatas.Length];
        for (int i = 0; i < _LevelDatas.Length; i++) 
        {
            LevelData levelData = _LevelDatas[i];
            LevelDataPanelScript panelScript = Instantiate(Panel, Grid).GetComponent<LevelDataPanelScript>();
            levelDataPanelScripts[i] = panelScript;
            panelScript._levelData = levelData;
        }
        
        if(_LevelDatas.Length > 0) LevelSelectorScript._levelData = levelDataPanelScripts[0]._levelData;
        
    }
}