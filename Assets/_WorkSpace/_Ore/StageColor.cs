using UnityEngine;

public class StageColor : MonoBehaviour
{
    [SerializeField, Header("”’‚Ì¢ŠE‚Ì‚É‚¾‚¯Œ©‚¦‚é‚à‚Ì")]
    GameObject _blackDeleteParent;
    [SerializeField, Header("•‚Ì¢ŠE‚Ì‚É‚¾‚¯Œ©‚¦‚é‚à‚Ì")]
    GameObject _whiteDeleteParent;
    [SerializeField, Header("—¼•û‚Å‘¶İ‚·‚é‚à‚Ì")]
    GameObject _monoParent;
    [SerializeField, Header("‰æ‘œ‚Ì·‚µ‘Ö‚¦‚Å‚È‚­Color‚ğ‚¢‚¶‚Á‚Ä”½“]‚³‚¹‚½‚¢‚à‚Ì")]
    GameObject _monoParentBG;
    [SerializeField, Header("—¼•û‚Ì¢ŠE‚Å‘¶İ‚·‚é‚à‚Ì‚ÌŒ©‚½–Ú(”’‚Ì)")]
    Sprite _blackBlock;
    [SerializeField, Header("—¼•û‚Ì¢ŠE‚Å‘¶İ‚·‚é‚à‚Ì‚ÌŒ©‚½–Ú(•‚Ì)")]
    Sprite _whiteBlock;

    public int _colorControlNumber = 0;

    SpriteRenderer[] _monoParentColor;
    SpriteRenderer[] _monoParentBGColor;

    bool _white;
    bool _black;

    public void BlockColorChange()
    {
        _monoParentColor = _monoParent.GetComponentsInChildren<SpriteRenderer>();
        switch(_colorControlNumber % 2)
        {
            case 0:
                _white = true;
                for (int i = 0; i < _monoParentColor.Length; i++)
                {
                    if (_monoParentColor[i].sprite == _whiteBlock)
                    {
                        _monoParentColor[i].sprite = _blackBlock;
                    }
                    else
                    {
                        _monoParentColor[i].sprite = _whiteBlock;
                    }
                }
                _blackDeleteParent.SetActive(true);
                _whiteDeleteParent.SetActive(false);
            break;
            case 1:
                _black = true;
                for (int i = 0; i < _monoParentColor.Length; i++)
                {
                    if (_monoParentColor[i].sprite == _whiteBlock)
                    {
                        _monoParentColor[i].sprite = _blackBlock;
                    }
                    else
                    {
                        _monoParentColor[i].sprite = _whiteBlock;
                    }
                }
                _blackDeleteParent.SetActive(false);
                _whiteDeleteParent.SetActive(true);
            break;
        }
    }

    public void BGColorChange()
    {
        _monoParentBGColor = _monoParentBG.GetComponentsInChildren<SpriteRenderer>();
    }
}
