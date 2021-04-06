using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcess : MonoBehaviour
{
    public static GameProcess Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameContext.Instance.InitGameEvent();
        CharacterContext.Instance.SubscribeEvent();
        StartCoroutine(GameStart());
    }
    IEnumerator GameStart()
    {
        Debug.Log("Enter Scene");
        yield return new WaitForSeconds(5);
        Debug.Log("Init Character");
        CharacterContext.Instance.InitCharacter();
    }
}
