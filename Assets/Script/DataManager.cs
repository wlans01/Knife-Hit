using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Inst { get; set; }
    public int NowStage { get => nowStage; set => nowStage = value; }
    public int HightScore { get => hightScore; set => hightScore = value; }
    public bool IsInfiniteMode { get => isInfiniteMode; set => isInfiniteMode = value; }

    [SerializeField]
    int nowStage;
    [SerializeField]
    int hightScore;
    [SerializeField]
    bool isInfiniteMode;

    private void Awake()
    {
        if (Inst == null)
            Inst = this;
        else if (Inst != null)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("NowStage", NowStage);
        PlayerPrefs.SetInt("HightScore", HightScore);
    }
    public void Load()
    {
        if (!PlayerPrefs.HasKey("NowStage"))
        {
            NowStage = 1;
            HightScore = 0;
            Save();
            return;
        }
        NowStage = PlayerPrefs.GetInt("NowStage");
        HightScore = PlayerPrefs.GetInt("HightScore");


    }
}
