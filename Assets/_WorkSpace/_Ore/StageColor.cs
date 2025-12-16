using UnityEngine;

public class StageColor : MonoBehaviour
{
    [SerializeField,Header("”’‚Ì¢ŠE‚Ì‚É‚¾‚¯Œ©‚¦‚é‚à‚Ì")]
    GameObject _whiteDeleteParent;
    [SerializeField,Header("•‚Ì¢ŠE‚Ì‚É‚¾‚¯Œ©‚¦‚é‚à‚Ì")]
    GameObject _blackDeleteParent;

    public int _colorControlNumber = 0;

    bool _white;
    bool _black;

    public void ColorChange()
    {
        switch(_colorControlNumber % 2)
        {
            case 0:
                _white = true;
                _whiteDeleteParent.SetActive(true);
                _blackDeleteParent.SetActive(false);
            break;
            case 1:
                _black = true;
                _whiteDeleteParent.SetActive(false);
                _blackDeleteParent.SetActive(true);
            break;
        }
    }
}
