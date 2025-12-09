using UnityEngine;

public class StageColor : MonoBehaviour
{
    [SerializeField]
    GameObject _whiteParent;
    [SerializeField]
    GameObject _blackParent;

    public int _colorControlNumber = 0;

    bool _white;
    bool _black;

    void ColorChange()
    {
        switch(_colorControlNumber % 2)
        {
            case 0:
                _white = true;
                _whiteParent.SetActive(true);
                _blackParent.SetActive(false);
            break;
            case 1:
                _black = true;
                _whiteParent.SetActive(false);
                _blackParent.SetActive(true);
            break;
        }
    }
}
