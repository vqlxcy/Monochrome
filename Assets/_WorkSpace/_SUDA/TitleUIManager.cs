using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TitleUIManager : MonoBehaviour
{
    public Button playButton;
    public Button explanationButton;

    [Header("‘I‘ð’†•\Ž¦")]
    public RectTransform arrow;
    public Vector3 arrowOffset = new Vector3(-50f, 0f, 0f);

    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip selectMoveSE;   
    public AudioClip decideSE;       

    [Header("‘JˆÚ‚Ü‚Å‚Ì‘Ò‚¿ŽžŠÔ")]
    public float waitTime = 0.3f;

    int currentIndex = 0; 
    bool isDeciding = false;

    void Start()
    {
        UpdateArrowPosition();
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
                UpdateArrowPosition();
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentIndex != 1)
            {
                currentIndex = 1;
                PlayMoveSE();
                UpdateArrowPosition();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDeciding = true;

            if (audioSource && decideSE)
            {
                audioSource.PlayOneShot(decideSE);
            }

            if (currentIndex == 0)
            {
                StartCoroutine(LoadScene("MonoStage01"));
            }
            else
            {
                StartCoroutine(LoadScene("MonoExplanation01"));
            }
        }
    }

    void PlayMoveSE()
    {
        if (audioSource && selectMoveSE)
        {
            audioSource.PlayOneShot(selectMoveSE);
        }
    }

    void UpdateArrowPosition()
    {
        RectTransform target =
            currentIndex == 0
            ? playButton.GetComponent<RectTransform>()
            : explanationButton.GetComponent<RectTransform>();

        arrow.position = target.position + arrowOffset;
    }

    IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneName);
    }
}
