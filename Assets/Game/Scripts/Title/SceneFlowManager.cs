using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager : MonoBehaviour
{
    static SceneFlowManager _instance;
    public string NickName;
    public int ColorNum;
    public static SceneFlowManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject myObject = new GameObject();
                myObject.name = "SceneFlowManager";
                myObject.AddComponent<SceneFlowManager>();
                DontDestroyOnLoad(myObject);
                _instance = myObject.GetComponent<SceneFlowManager>();
            }
            return _instance;
        }
    }

    public void Init()
    {
        
    }
    public void TitleToIngameFromGameStart()
    {
        StartCoroutine(LoadSceneAsync(1));
    }

    public void IngameToTitleFromEndGame()
    {
        StartCoroutine(LoadSceneAsync(0, Test));
    }

    IEnumerator LoadSceneAsync(int sceneNum, Action callback = null)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(sceneNum);
        while (true)
        {
            yield return null;
            if (asyncOperation.isDone) {
                callback?.Invoke();
                break;
            } 
        }
    }

    void Test()
    {
        TitleManager.Instance.EndGame();
    }
}
