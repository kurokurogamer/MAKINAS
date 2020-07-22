using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddScene : MonoBehaviour
{
    [SerializeField, Tooltip("追加するシーン")]
    private List<string> _sceneNameList = new List<string>();
    private void Awake()
    {
        foreach(string sceneName in _sceneNameList)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
