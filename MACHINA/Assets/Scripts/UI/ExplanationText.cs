using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExplanationText
{
    [SerializeField, Tooltip("ミッション名")]
    private string _missionName = "";
    [SerializeField, TextArea(5, 10), Tooltip("ミッション内容")]
    private string _explantion = "";
    [SerializeField, Tooltip("イメージ")]
    private Sprite _sprite = null;
    [SerializeField, Tooltip("ボイス")]
    private AudioClip _voice = null;

    public string MissionName
    {
        get { return _missionName; }
    }
    public string Explantion
    {
        get { return _explantion; }
    }

    public Sprite SpriteImage
    {
        get { return _sprite; }
    }
    public AudioClip Voice
    {
        get { return _voice; }
    }
}
