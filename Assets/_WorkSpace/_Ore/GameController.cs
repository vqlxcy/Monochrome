using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static GameController instance { get; set; }


    StageColor _sc;
    StageRotation _sr;

    int _stageNumber;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;

        _sc = GetComponent<StageColor>();
        _sr = GetComponent<StageRotation>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (_stageNumber >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _sc._colorControlNumber++;

                _sc.BlockColorChange();
            }

            if (_stageNumber >= 2)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    _sr.StageRotate();
                }
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "MonoStage01")
        {
            _stageNumber = 1;
        }
        else if (SceneManager.GetActiveScene().name == "MonoStage02")
        {
            _stageNumber = 2;
        }
        else if (SceneManager.GetActiveScene().name == "MonoStage03")
        {
            _stageNumber = 3;
        }
        else
        {
            _stageNumber = 0;
        }
    }
}
