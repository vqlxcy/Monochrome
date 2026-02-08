using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameClearUIManager : MonoBehaviour
{
    public Button playAgainButton;
    public Button backToTitleButton;

    [Header("ëIëêFê›íË")]
    public Color selectedColor = Color.white;
    public Color unselectedColor = new Color(0.6f, 0.6f, 0.6f);

    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip moveSE;     
    public AudioClip decideSE;   

    [Header("ëJà⁄Ç‹Ç≈ÇÃë“Çøéûä‘")]
    public float waitTime = 0.3f;

    int currentIndex = 0; 
    bool isDeciding = false;

    void Start()
    {
        UpdateButtonVisual();
    }

    void Update()
    {
        if (isDeciding) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentIndex != 0)
            {
                currentIndex = 0;
                PlayMoveSE();
                UpdateButtonVisual();
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentIndex != 1)
            {
                currentIndex = 1;
                PlayMoveSE();
                UpdateButtonVisual();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDeciding = true;

            PlayDecideSE();

            if (currentIndex == 0)
            {
                StartCoroutine(LoadScene("MonoStage01"));
            }
            else
            {
                StartCoroutine(LoadScene("MonoTitle"));
            }
        }
    }

    void UpdateButtonVisual()
    {
        playAgainButton.GetComponent<Image>().color =
            (currentIndex == 0) ? selectedColor : unselectedColor;

        backToTitleButton.GetComponent<Image>().color =
            (currentIndex == 1) ? selectedColor : unselectedColor;
    }

    void PlayMoveSE()
    {
        if (audioSource && moveSE)
            audioSource.PlayOneShot(moveSE);
    }

    void PlayDecideSE()
    {
        if (audioSource && decideSE)
            audioSource.PlayOneShot(decideSE);
    }

    IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneName);
    }
}
