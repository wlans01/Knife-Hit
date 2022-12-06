using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nowStageText;
    [SerializeField]
    TextMeshProUGUI hightScoreText;
    [SerializeField]
    AudioClip clickClip;
    [SerializeField]
    GameObject settingPanel;

    private void Start()
    {
        nowStageText.text = $"Stage\n{DataManager.Inst.NowStage}";
        hightScoreText.text = $"Hight Score\n{DataManager.Inst.HightScore}";
    }

    public void GameStart(bool inf)
    {
        SoundControl.soundControl.SfxPlay("click", clickClip);
        DataManager.Inst.IsInfiniteMode = inf;
        SceneManager.LoadScene(1);
    }
    public void SettingPanel(bool b)
    {
        SoundControl.soundControl.SfxPlay("click", clickClip);
        settingPanel.SetActive(b);
    }

    public void Exit()
    {
        DataManager.Inst.Save();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
