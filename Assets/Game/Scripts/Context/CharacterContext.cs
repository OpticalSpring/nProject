using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContext : MonoBehaviour
{

    List<GameCharacter> gameCharacters;


    private void Start()
    {
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.Test, TextFunc);
        new TestEvent().Send();
    }

    private void TextFunc(GameEvent eventData)
    {
        Debug.Log("TestGood");
    }
}
