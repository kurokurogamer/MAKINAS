using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
	[SerializeField, Tooltip("決定音")]
	private AudioClip _pushSE = null;
    [SerializeField, Tooltip("キャンセル音")]
    private AudioClip _cancelSE = null;
    [SerializeField, Tooltip("操作音")]
    private AudioClip _moveSE = null;

    public AudioClip PushSE
	{
		get { return _pushSE; }
	}
    public AudioClip CancelSE
    {
        get { return _cancelSE; }
    }
    public AudioClip MoveSE
    {
        get { return _moveSE; }
    }
}
