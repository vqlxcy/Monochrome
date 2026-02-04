using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExplanationBackButton : MonoBehaviour
{
    [Header("UI")]
    public Button backButton;

    [Header("îíî≠åıê›íË")]
    public Color baseColor = Color.white;      
    public float glowStrength = 0.3f;         
    public float glowSpeed = 2f;               

    [Header("åàíËSE")]
    public AudioClip clickSE;

    [Header("ñﬂÇÈÉVÅ[Éìñº")]
    public string backSceneName = "MonoTitle";

    bool isDeciding = false;
    Image buttonImage;

    void Start()
    {
        buttonImage = backButton.GetComponent<Image>();
    }

    void Update()
    {
        UpdateGlow();

        if (isDeciding) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDeciding = true;
            PlaySE();
            SceneManager.LoadScene(backSceneName);
        }
    }

    void UpdateGlow()
    {
        if (buttonImage == null) return;

        float t = (Mathf.Sin(Time.time * glowSpeed) + 1f) * 0.5f;

        Color glowColor = baseColor + Color.white * (t * glowStrength);
        glowColor.a = 1f; 

        buttonImage.color = glowColor;
    }

    void PlaySE()
    {
        if (clickSE != null && MonoAudioManager.Instance != null)
        {
            var audio = MonoAudioManager.Instance.bgmSource;
            if (audio != null)
            {
                audio.PlayOneShot(clickSE);
            }
        }
    }
}
