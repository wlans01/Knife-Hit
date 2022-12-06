using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class SoundControl : MonoBehaviour
{

    public static SoundControl soundControl;

    [Header("--------------------[ Audio ]")]
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField]
    AudioSource bgmplayer;
    [SerializeField]
    AudioClip startBGM;
    [SerializeField]
    AudioClip[] mainBGM;


    float masterVolume, bgmVloume, sfxVolume;



    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (soundControl == null)
        {
            soundControl = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (soundControl != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        VolumeLoad();
    }



    // 씬 바뀔때 배경은 변경
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bgmplayer.Stop();
        if (scene.name == "Start")
            BgmPlay(startBGM);
        else if (scene.name == "KnifeHit")
        {
            AudioClip temp = mainBGM[(int)Random.Range(0, mainBGM.Length)];
            BgmPlay(temp);
        }

    }


    // 배경음 플레이
    void BgmPlay(AudioClip clip)
    {
        bgmplayer.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BackGround")[0];
        bgmplayer.clip = clip;
        bgmplayer.loop = true;
        bgmplayer.Play();
    }

    //효과음 플레이
    public void SfxPlay(string sfxName, AudioClip clip)
    {
        GameObject ap = new GameObject(sfxName + "Sound");
        AudioSource audioSource = ap.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(ap, clip.length);
    }
    //효과음 플레이
    public void SfxPlay2(string sfxName, AudioClip clip)
    {
        bgmplayer.Stop();
        GameObject ap = new GameObject(sfxName + "Sound");
        AudioSource audioSource = ap.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(ap, clip.length);
    }

    // 볼륨 조절 부분
    public void MasterVolume(float val)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat("MasterVolume", val);

    }
    public void BgmVolume(float val)
    {
        audioMixer.SetFloat("BgmVolume", Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat("BgmVolume", val);
    }
    public void SfxVolume(float val)
    {
        audioMixer.SetFloat("SfxVolume", Mathf.Log10(val) * 20);
        PlayerPrefs.SetFloat("SfxVolume", val);
    }

    //볼륨 조절 저장



    void VolumeLoad()
    {
        if (!PlayerPrefs.HasKey(this.gameObject.name + "MasterVolume"))
            return;
        float mv = PlayerPrefs.GetFloat("MasterVolume");
        float bv = PlayerPrefs.GetFloat("BgmVolume");
        float sv = PlayerPrefs.GetFloat("SfxVolume");
        MasterVolume(mv);
        BgmVolume(bv);
        SfxVolume(sv);

    }
}
