using System.Collections.Generic;
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
    [SerializeField,Header("色変更の回数制限(ステージ毎)")]
    int _firstStageColorControlLimit;
    [SerializeField]
    int _secondStageColorControlLimit;
    [SerializeField]
    int _thirdStageColorControlLimit;
    [SerializeField, Header("ステージ回転の回数制限(ステージ毎)")]
    int _firstStageRotateLimit;
    [SerializeField]
    int _secondStageRotateLimit;
    [SerializeField]
    int _thirdStageRotateLimit;
    [SerializeField]
    GameObject _player;
    [SerializeField]
    GameObject _GoalInstanceButton;
    [SerializeField]
    GameObject _goal;
    [SerializeField]
    GameObject[] _coins;
    [SerializeField]
    Transform[] _coinPos;

    List<GameObject> _useCoinList = new();
    public List<GameObject> _coinList = new List<GameObject>();
    int _colorControlLimit;
    int _stageRotateLimit;
    int _stageNumber;
    int _randomInt;
    Vector3 _savePlayerPosition;
    Vector3 _saveWhiteMovePosition;
    GameObject _whiteMoveBlock;

    public bool _isGoal;
    bool _buttonSpawn;
    public bool _isButtonClicked = false;
    public bool _isPlayerDeath;

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
            if (_sc._colorControlNumber % 2 == 0)
            {
                _whiteMoveBlock.transform.position = _saveWhiteMovePosition;
            }

            if (Input.GetKeyDown(KeyCode.Return) && _sc._colorControlCount <= _colorControlLimit)
            {
                if (_sc._colorControlNumber % 2 == 0)
                {
                    _savePlayerPosition = _player.transform.position;
                }
                else if (_sc._colorControlNumber % 2 == 1)
                {
                    _player.transform.position = _savePlayerPosition;
                    _saveWhiteMovePosition = _whiteMoveBlock.transform.position;
                }
                _sc._colorControlNumber++;

                _sc.BlockColorChange();
                _sc.BGColorChange();
                _sc._colorControlCount++;
            }

            if (_isGoal)
            {
                SceneManager.LoadScene(_secondStage);
            }

            if (_stageNumber >= 2)
            {
                if (Input.GetKeyDown(KeyCode.R) && _sr._stageRotateCount <= _stageRotateLimit)
                {
                    _sr.StageRotate();
                    _sr._stageRotateCount++;
                }

                if (_coinList[0] == null)
                {
                    _buttonSpawn = true;
                }

                if (_buttonSpawn)
                {
                    _GoalInstanceButton.SetActive(true);
                }

                if (_isButtonClicked)
                {
                    _goal.SetActive(true);
                }
                else
                {
                    _goal.SetActive(false);
                }

                if (_isGoal)
                {
                    SceneManager.LoadScene(_thirdStage);
                }
            }
        }
    }

    void RandomCoinSelect()
    {
        #region
        //https://www.pandanoir.info/entry/2013/03/04/193704
        //本当はFisher–Yatesアルゴリズムを使ったほうがいい(やってることはほぼ一緒)
        #endregion
        _randomInt = Random.Range(0, _useCoinList.Count + 1);
        _coinList.Add(_useCoinList[_randomInt]);
        _useCoinList.RemoveAt(_randomInt);
        if (_useCoinList.Count != 0)
        {
            RandomCoinSelect();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _isGoal = false;
        _sc._colorControlCount = 0;
        _player = GameObject.FindGameObjectWithTag("Player");
        _whiteMoveBlock = GameObject.FindGameObjectWithTag("WhiteMove");
        _GoalInstanceButton.SetActive(false);

        for (int i = 0; i < _coins.Length; i++)
        {
            _useCoinList[i] = _coins[i];
            Instantiate(_useCoinList[i], _coinPos[i].position, Quaternion.identity);
        }

        _saveWhiteMovePosition = _whiteMoveBlock.transform.position;
        _savePlayerPosition = _player.transform.position;

        if (SceneManager.GetActiveScene().name == _firstStage)
        {
            _stageNumber = 1;
            _colorControlLimit = _firstStageColorControlLimit;
            _stageRotateLimit = _firstStageRotateLimit;
        }
        else if (SceneManager.GetActiveScene().name == _secondStage)
        {
            _stageNumber = 2;
            _colorControlLimit = _secondStageColorControlLimit;
            _stageRotateLimit = _secondStageRotateLimit;
            RandomCoinSelect();
        }
        else if (SceneManager.GetActiveScene().name == _thirdStage)
        {
            _stageNumber = 3;
            _colorControlLimit = _thirdStageColorControlLimit;
            _secondStageRotateLimit = _thirdStageRotateLimit;
            RandomCoinSelect();
        }
        else
        {
            _stageNumber = 0;
        }
    }
}
