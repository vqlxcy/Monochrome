using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MonoAudioManager : MonoBehaviour
{
    public static MonoAudioManager Instance;

    [Header("AudioSource")]
    public AudioSource bgmSource;
    public AudioSource seSource;

    [Header("Volume Setting UI")]
    public GameObject volumePanel;
    public Slider bgmSlider;
    public Slider seSlider;

    [Header("Scene BGM List")]
    public List<SceneBGM> sceneBGMList = new List<SceneBGM>();

    bool isPanelOpen = false;
    string currentSceneName = "";

    [System.Serializable]
    public class SceneBGM
    {
        public string sceneName;
        public AudioClip bgm;
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        float bgmVol = PlayerPrefs.GetFloat("BGM_VOLUME", 0.7f);
        float seVol = PlayerPrefs.GetFloat("SE_VOLUME", 0.7f);

        bgmSource.volume = bgmVol;
        seSource.volume = seVol;

        bgmSlider.value = bgmVol;
        seSlider.value = seVol;

        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        seSlider.onValueChanged.AddListener(SetSEVolume);

        volumePanel.SetActive(false);

        SceneManager.sceneLoaded += OnSceneLoaded;

        PlayBGMForScene(SceneManager.GetActiveScene().name);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel();
        }
    }

    void TogglePanel()
    {
        isPanelOpen = !isPanelOpen;
        volumePanel.SetActive(isPanelOpen);
        Time.timeScale = isPanelOpen ? 0f : 1f;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGMForScene(scene.name);
    }

    void PlayBGMForScene(string sceneName)
    {
        if (currentSceneName == sceneName) return;

        foreach (var data in sceneBGMList)
        {
            if (data.sceneName == sceneName)
            {
                if (bgmSource.clip == data.bgm) return;

                bgmSource.Stop();
                bgmSource.clip = data.bgm;
                bgmSource.Play();

                currentSceneName = sceneName;
                return;
            }
        }

        Debug.LogWarning("Ç±ÇÃÉVÅ[ÉìÇÃBGMÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ: " + sceneName);
    }

    public void SetBGMVolume(float value)
    {
        bgmSource.volume = value;
        PlayerPrefs.SetFloat("BGM_VOLUME", value);
    }

    public void SetSEVolume(float value)
    {
        seSource.volume = value;
        PlayerPrefs.SetFloat("SE_VOLUME", value);
    }

    public void PlaySE(AudioClip clip)
    {
        seSource.PlayOneShot(clip);
    }
}
