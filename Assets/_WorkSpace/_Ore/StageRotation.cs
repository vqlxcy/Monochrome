using UnityEngine;

public class StageRotation : MonoBehaviour
{
    [SerializeField]
    Transform _stage;
    [SerializeField]
    int _rotationAngle;
    [SerializeField]
    GameObject _player;

    public int _stageRotateCount;

    int _stageRotationControlnumber;

    public void StageRotate()
    {
        if (_stageRotationControlnumber % 2 == 0)
        {
            _stage.eulerAngles = new Vector3(0, 0, _rotationAngle);
        }
        else
        {
            _stage.eulerAngles = new Vector3(0, 0, 0);
        }
        _player.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
