using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScript : MonoBehaviour
{
    public bool autoplay;
    public static LevelData _levelData
    {
        get
        {
            return LevelSelectorScript.levelData;
        }
    }
    public static NoteMap _noteMap
    {
        get
        {
            return LevelSelectorScript.levelData.GetNoteMapIndex(LevelSelectorScript._notemapIndex);
        }
    }

    public AudioSource audioSource
    {
        get
        {
            return GetComponent<AudioSource>();
        }
    }

    public NotePlayer notePlayer;
    public TrailRenderer trailRenderer;

    public TMPro.TMP_Text combo;
    public TMPro.TMP_Text score;

    public static GameplayScript main;

    public RectTransform Top;
    public RectTransform Bottom;

    NoteAnalyzer analyzer;

    void Awake()
    {
        main = this;
    }

    void Start()
    {

        GameplayData.Data.combo = 0;
        GameplayData.Data.score = 0;

        analyzer = new NoteAnalyzer(notePlayer);
        analyzer.onUpdate.AddListener(Hit);
        analyzer.onEnd.AddListener(End);
        GameplayData.Data.onCombo.AddListener(UpdateInfo);
        
        notePlayer.Play(_levelData.audioClip, _noteMap);
        GameplayState.main.Pause();
        GameplayState.main.DisplayCountdownScreen();
    }

    void Hit()
    {
        Top.LeanCancel();
        Bottom.LeanCancel();
        TouchInputHandler.DeployRay(analyzer.GetNoteDataOffset(0).worldPosition);
        Top.LeanScaleY(1, 0);
        Bottom.LeanScaleY(1, 0);
        
        Top.LeanScaleY(1.1f, 0.5f).setEasePunch();
        Bottom.LeanScaleY(1.1f, 0.5f).setEasePunch();

        LeanTween.value(Camera.main.gameObject, (Color c) => Camera.main.backgroundColor = c, Camera.main.backgroundColor, Random.ColorHSV(), analyzer.GetNoteDataOffset(1).time - analyzer.GetNoteDataOffset(0).time).setEaseInOutExpo();

        trailRenderer.gameObject.LeanMove((Vector3)analyzer.GetNoteDataOffset(1).worldPosition + Vector3.forward * 10, analyzer.GetNoteDataOffset(1).time - analyzer.GetNoteDataOffset(0).time);
        Debug.DrawLine(analyzer.noteData.worldPosition, analyzer.GetNoteDataOffset(1).worldPosition, Color.green, analyzer.GetNoteDataOffset(1).time - analyzer.GetNoteDataOffset(0).time);
    }

    void End()
    {
        _levelData.SaveNoteMap(_noteMap);
        LeanTween.delayedCall(3, ChangeScene);
    }

    void ChangeScene()
    {
        LoadingScreenScript.LoadingScreen(() => UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector"), 1);
    }

    void UpdateInfo()
    {
        combo.gameObject.LeanCancel();
        score.gameObject.LeanCancel();
        combo.transform.localScale = Vector3.one;
        combo.gameObject.LeanScale(Vector3.one * 1.5f, 0.5f).setEasePunch();

        score.transform.localScale = Vector3.one;
        
        score.gameObject.LeanScaleY(2f, 0.5f).setEasePunch();

        combo.text = GameplayData.Data.combo.ToString();
        score.text = string.Format("{0:000000}", GameplayData.Data._score);
    }

}
