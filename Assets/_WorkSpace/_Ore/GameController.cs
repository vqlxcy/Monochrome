using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(StageColor))]
[RequireComponent(typeof(StageRotation))]
public class GameController : MonoBehaviour
{
    StageColor _sc;
    StageRotation _sr;

    [SerializeField]
    string _nextStage;
    [SerializeField, Header("色変更の回数制限")]
    int _colorControlLimit;
    [SerializeField, Header("ステージ回転の回数制限")]
    int _stageRotateLimit;
    [SerializeField]
    GameObject _player;
    [SerializeField]
    GameObject _goal;
    [SerializeField,Header("取得できるコイン")]
    GameObject[] _goalCoins;
    [SerializeField,Header("取得できないコイン")]
    GameObject[] _interfaceCoins;
    [SerializeField]
    Transform[] _goalCoinPos;
    [SerializeField]
    Transform[] _interfaceCoinPos;

    List<GameObject> _useCoinList = new();
    List<GameObject> _goalCoinList = new List<GameObject>();
    List<GameObject> _nonUseCoinList = new List<GameObject>();
    List<GameObject> _interfaceCoinList = new List<GameObject>();
    public List<GameObject> _collisionCoinList = new List<GameObject>();


    int _stageNumber;
    int _randomInt;
    Vector3 _savePlayerPosition;
    Vector3 _saveWhiteMovePosition;
    GameObject _whiteMoveBlock;

    public bool _isGoal;
    bool _buttonSpawn;
    public bool _isButtonClicked = false;
    public bool _isPlayerDeath;

    void Start()
    {
        _sc = GetComponent<StageColor>();
        _sr = GetComponent<StageRotation>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _whiteMoveBlock = GameObject.FindGameObjectWithTag("WhiteMove");

        _saveWhiteMovePosition = _whiteMoveBlock.transform.position;
        _isGoal = false;
        _sc._colorControlCount = 0;

        for (int i = 0; i < _goalCoins.Length; i++)
        {
            _useCoinList.Add(_goalCoins[i]);
            _nonUseCoinList.Add(_interfaceCoins[i]);
        }

        if (SceneManager.GetActiveScene().name == "MonoStage01")
        {
            _stageNumber = 1;
        }
        else if (SceneManager.GetActiveScene().name == "MonoStage02")
        {
            _stageNumber = 2;
            RandomCoinSelect();
        }
        else if (SceneManager.GetActiveScene().name == "MonoStage03")
        {
            _stageNumber = 3;
            RandomCoinSelect();
        }
        else
        {
            _stageNumber = 0;
        }

        _saveWhiteMovePosition = _whiteMoveBlock.transform.position;
        _savePlayerPosition = _player.transform.position;
    }

    void Update()
    {
        if (_sc._colorControlNumber % 2 == 0)
        {
            _whiteMoveBlock.transform.position = _saveWhiteMovePosition;
        }

        if (Input.GetKeyDown(KeyCode.Return) && _sc._colorControlCount <= _colorControlLimit)
        {
            _sc._colorControlNumber++;
            _sc._colorControlCount++;
            if (_sc._colorControlNumber % 2 == 0)
            {
                _saveWhiteMovePosition = _whiteMoveBlock.transform.position;
                _player.transform.position = _savePlayerPosition;
                _goal.SetActive(false);
            }
            else if (_sc._colorControlNumber % 2 == 1)
            {
                _savePlayerPosition = _player.transform.position;
                _goal.SetActive(true);
            }

            _sc.BlockColorChange();
            _sc.BGColorChange();
        }

        if (_isGoal)
        {
            SceneManager.LoadScene(_nextStage);
        }

        if (_stageNumber >= 2)
        {
            if (_collisionCoinList[0] == null)
            {
                _isGoal = true;
            }

            if (_buttonSpawn)
            {
                
            }

            if (_isGoal)
            {
                SceneManager.LoadScene(_nextStage);
            }

            if (_stageNumber >= 3)
            {
                if (Input.GetKeyDown(KeyCode.R) && _sr._stageRotateCount <= _stageRotateLimit)
                {
                    _sr.StageRotate();
                    _sr._stageRotateCount++;
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
        _randomInt = Random.Range(0, _useCoinList.Count);
        _goalCoinList.Add(_useCoinList[_randomInt]);
        _interfaceCoinList.Add(_nonUseCoinList[_randomInt]);
        _useCoinList.RemoveAt(_randomInt);
        _nonUseCoinList.RemoveAt(_randomInt);
        if (_useCoinList.Count != 0)
        {
            RandomCoinSelect();
        }
        else
        {
            for (int i = 0; i < _goalCoins.Length; i++)
            {
                GameObject obj = Instantiate(_interfaceCoinList[i], _goalCoinPos[i].position, Quaternion.identity);
                GameObject goalCoins = Instantiate(_goalCoinList[i], _goalCoinPos[i + 3].position, Quaternion.identity);
                _collisionCoinList.Add(goalCoins);
            }
        }
    }
}
