using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    public int KnifeCount { get => knifeCount; set => knifeCount = value; }
    public int GoalKnifeCount { get => goalKnifeCount; set => goalKnifeCount = value; }


    [SerializeField]
    int knifeCount;
    [SerializeField]
    int goalKnifeCount;

    [SerializeField]
    Text knifeText;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI hightScoreText;

    [SerializeField]
    GameObject gameClear;
    [SerializeField]
    GameObject gameOver;
    [SerializeField]
    GameObject gameMode;
    [SerializeField]
    AudioClip[] clips;
    [SerializeField]
    ParticleSystem knifeEffect;



    private void Awake()
    {
        if (Inst == null)
            Inst = this;
        else if (Inst != null)
            Destroy(gameObject);

    }
    private void Start()
    {
        GameStart();
    }

    // 나이프 던졌을때 모드마다 다르게 카운트하기
    public void ThrowKnife()
    {
        knifeEffect.Play();
        if (DataManager.Inst.IsInfiniteMode)
        {
            KnifeCount++;
            knifeText.text = KnifeCount.ToString();
        }
        else
        {
            KnifeCount--;
            knifeText.text = KnifeCount.ToString();
            if (KnifeCount <= 0)
                GameClear();
        }

    }

    //게임시작 초기화하기
    public void GameStart()
    {
        // 무한모드일경우 칼의수가 증가 스테이지모드경우 정해진 칼 던지기
        Time.timeScale = 1;
        if (DataManager.Inst.IsInfiniteMode)
        {
            KnifeCount = 0;
            knifeText.text = KnifeCount.ToString();
        }
        else
        {
            GoalKnifeCount = DataManager.Inst.NowStage + 2;
            KnifeCount = GoalKnifeCount;
            knifeText.text = KnifeCount.ToString();
        }

        gameOver.SetActive(false);
        gameClear.SetActive(false);
        gameMode.SetActive(false);

    }

    //게임 클리어 [stage모드 클리어시]
    public void GameClear()
    {
        SoundControl.soundControl.SfxPlay2("clear", clips[0]);
        Time.timeScale = 0;
        DataManager.Inst.NowStage++;
        gameClear.SetActive(true);
        DataManager.Inst.Save();
    }

    //게임오버
    public void GameOver()
    {
        // 스케일 0 으로 만들어서 게임정지
        Time.timeScale = 0;
        // 어떤 모드였는지 확인
        if (DataManager.Inst.IsInfiniteMode)
        {
            //무한모드인경우 신기록을 달성하면 최고스코어 갱신 클리어 효과음
            gameMode.SetActive(true);
            if (DataManager.Inst.HightScore <= knifeCount)
            {
                SoundControl.soundControl.SfxPlay2("clear", clips[0]);
                DataManager.Inst.HightScore = knifeCount;
            }
            else
            {
                SoundControl.soundControl.SfxPlay2("over", clips[1]);
            }
            hightScoreText.text = DataManager.Inst.HightScore.ToString();
            scoreText.text = knifeCount.ToString();
        }
        else
        {
            //무한모드가 아닌경우 게임오버
            SoundControl.soundControl.SfxPlay2("over", clips[1]);
            gameOver.SetActive(true);
        }
        DataManager.Inst.Save();
    }

    //다음스테이지 단계를 올리고 다시 로드
    public void NextStage()
    {
        SoundControl.soundControl.SfxPlay2("clik", clips[2]);
        DataManager.Inst.NowStage++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 다시하기 씬 다시로드
    public void GameRestart()
    {
        SoundControl.soundControl.SfxPlay2("clik", clips[2]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //메뉴 씬으로 이동하기
    public void GoMenu()
    {
        SoundControl.soundControl.SfxPlay2("clik", clips[2]);
        SceneManager.LoadScene("Start");
    }


}
