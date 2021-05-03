using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FXContext : MonoBehaviour
{
    public static FXContext Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SubscribeEvent()
    {
        GameContext.Instance.RegisterObserver(GameEvent.GameEventType.SpawnFX, SpawnFX);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnFX(GameEvent data)
    {
        SpawnFXEvent e = (SpawnFXEvent)data;
        
        GameObject effect = Instantiate(
            Resources.Load(Path.Combine("FX", e.Prefab)), 
            e.Position, e.Rotation) as GameObject;
    }
}
