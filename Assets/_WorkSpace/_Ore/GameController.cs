using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(StageColor))]
[RequireComponent(typeof(StageRotation))]
public class GameController : MonoBehaviour
{
    static GameController instance { get; set; }

    StageColor _sc;
    StageRotation _sr;

    [SerializeField]
    string _firstStage;
    [SerializeField]
    string _secondStage;
    [SerializeField]
    string _thirdStage;
    [SerializeField]
    GameObject _firstStageGoalInstanceButton;

    int _stageNumber;
    public bool _isGoal = false;
    public bool _isButtonClicked = false;

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
                _sc.BGColorChange();
            }

            if (_isButtonClicked)
            {
                _firstStageGoalInstanceButton.SetActive(true);
            }
            else
            {
                _firstStageGoalInstanceButton.SetActive(false);
            }

            if (_isGoal)
            {
                _isGoal = false;
                SceneManager.LoadScene(_firstStage);
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
        if (SceneManager.GetActiveScene().name == _firstStage)
        {
            _stageNumber = 1;
        }
        else if (SceneManager.GetActiveScene().name == _secondStage)
        {
            _stageNumber = 2;
        }
        else if (SceneManager.GetActiveScene().name == _thirdStage)
        {
            _stageNumber = 3;
        }
        else
        {
            _stageNumber = 0;
        }
    }
}
