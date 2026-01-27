using UnityEngine;

public class StageColor : MonoBehaviour
{
    [SerializeField, Header("”’‚Ì¢ŠE‚Ì‚É‚¾‚¯Œ©‚¦‚é‚à‚Ì")]
    GameObject _blackDeleteParent;
    [SerializeField, Header("•‚Ì¢ŠE‚Ì‚É‚¾‚¯Œ©‚¦‚é‚à‚Ì")]
    GameObject _whiteDeleteParent;
    [SerializeField, Header("—¼•û‚Å‘¶İ‚·‚é‚à‚Ì(Sprute’£‘Ö)")]
    GameObject _monoParent;
    [SerializeField, Header("—¼•û‚Å‘¶İ‚·‚é‚à‚Ì(Color”’l•ÏX)")]
    GameObject _monoParentBG;
    [SerializeField, Header("—¼•û‚Ì¢ŠE‚Å‘¶İ‚·‚é‚à‚Ì‚ÌSprite(”’‚Ì)")]
    Sprite _blackBlock;
    [SerializeField, Header("—¼•û‚Ì¢ŠE‚Å‘¶İ‚·‚é‚à‚Ì‚ÌSprite(•‚Ì)")]
    Sprite _whiteBlock;
    [SerializeField, Header("AudioSource")]
    AudioSource _as;
    [SerializeField, Header("F”½“]‚ÌSE")]
    AudioClip _colorChangeSE;

    public int _colorControlNumber = 0;
    public int _colorControlCount;

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
                _black = true;
                _white = false;
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
            case 1:
                _white = true;
                _black = false;
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
        }
    }

    public void BGColorChange()
    {
        _as.PlayOneShot(_colorChangeSE);
        _monoParentBGColor = _monoParentBG.GetComponentsInChildren<SpriteRenderer>();
        switch(_colorControlNumber % 2)
        {
            case 0:
                _white = true;
                _black = false;
                for (int i = 0; i < _monoParentBGColor.Length; i++)
                {
                    if (_monoParentBGColor[i].color == new Color(0, 0, 0))
                    {
                        _monoParentBGColor[i].color = new Color(1, 1, 1);
                    }
                    else if (_monoParentBGColor[i].color == new Color(1, 1, 1))  
                    {
                        _monoParentBGColor[i].color = new Color(0, 0, 0);
                    }
                }
            break;
            case 1:
                _black = true;
                _white = false;
                for (int i = 0; i < _monoParentBGColor.Length; i++)
                {
                    if (_monoParentBGColor[i].color == new Color(0, 0, 0))
                    {
                        _monoParentBGColor[i].color = new Color(1, 1, 1);
                    }
                    else if (_monoParentBGColor[i].color == new Color(1, 1, 1)) 
                    {
                        _monoParentBGColor[i].color = new Color(0, 0, 0);   
                    }
                }
            break;
        }
    }
}
