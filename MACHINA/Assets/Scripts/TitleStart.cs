using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleStart : MonoBehaviour
{
    [SerializeField]
    private List<FadeUI> _fadeUIList = new List<FadeUI>();
    // Start is called before the first frame update
    void Start()
    {
        if(TryGetComponent(out FadeUI ui))
        {
            _fadeUIList.Add(ui);
        }
    }

    private void Skip()
    {
        foreach(FadeUI ui in _fadeUIList)
        {
            ui.FadeSkip();
        }
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3")
            || Input.GetButtonDown("Mouse0") || Input.GetButtonDown("Mouse1") || Input.GetButtonDown("Mouse2"))
        {
            Skip();
        }

        if(_fadeUIList[0].Alpha == 0)
        {
            SceneManager.LoadScene("Title", LoadSceneMode.Additive);
        }
    }
}
