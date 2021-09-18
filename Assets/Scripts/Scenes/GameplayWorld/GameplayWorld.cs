using System.Collections;
using UnityEngine;

public class GameplayWorld : MonoBehaviour
{
    public AudioClip audioClip = GameplayData.audioClip;
    public LevelResult levelResult = GameplayData.levelResult;
    public NoteMap noteMap = GameplayData.noteMap;

    public NotePlayer notePlayer;

    public static GameplayWorld main;

    public void Awake()
    {
        main = this;
    }

    public void Start()
    {
        audioClip = GameplayData.audioClip;
        levelResult = GameplayData.levelResult;
        noteMap = GameplayData.noteMap;

        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        notePlayer.Play(audioClip, noteMap);
        yield return notePlayer.HitCorountine;
        yield return new WaitForSeconds(3);
        EndGame();
    }

    private void EndGame()
    {
        levelResult.SaveResult(levelResult.name);
        ResultScreen.main.Display();
    }
}