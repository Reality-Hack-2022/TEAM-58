using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string playerName = "";

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void NameEntered(string t)
    {
        playerName = t;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
