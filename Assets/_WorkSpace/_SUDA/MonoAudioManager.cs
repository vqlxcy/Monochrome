using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class MonoAudioManager : MonoBehaviour
{
    public static MonoAudioManager Instance;

    [Header("BGM AudioSource")]
    public AudioSource bgmSource;

    [Header("UI SE Clips")]
    public AudioClip openPanelSE;
    public AudioClip closePanelSE;

    [Header("Volume Setting UI")]
    public GameObject volumePanel;
    public Slider bgmSlider;
    public TMP_Text bgmPercentText;

    [Header("Scene BGM List")]
    public List<SceneBGM> sceneBGMList = new List<SceneBGM>();

    bool isPanelOpen = false;
    string currentSceneName = "";

    const float DEFAULT_VOLUME = 0.5f;

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
            Destroy(transform.root.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(transform.root.gameObject);
    }

    void Start()
    {
        float bgmVol = PlayerPrefs.GetFloat("BGM_VOLUME", DEFAULT_VOLUME);

        bgmSource.volume = bgmVol;
        bgmSlider.value = bgmVol;

        UpdateVolumeText(bgmVol);

        bgmSlider.onValueChanged.AddListener(OnSliderChanged);

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

        if (isPanelOpen)
        {
            PlaySE(openPanelSE);
        }
        else
        {
            PlaySE(closePanelSE);
        }

        Time.timeScale = isPanelOpen ? 0f : 1f;
    }

    void PlaySE(AudioClip clip)
    {
        if (clip == null || bgmSource == null) return;
        bgmSource.PlayOneShot(clip);
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

    }

    void OnSliderChanged(float value)
    {
        bgmSource.volume = value;
        PlayerPrefs.SetFloat("BGM_VOLUME", value);
        UpdateVolumeText(value);
    }

    void UpdateVolumeText(float value)
    {
        int percent = Mathf.RoundToInt(value * 100f);
        bgmPercentText.text = percent + "%";
    }
}
