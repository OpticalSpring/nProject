using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContext : MonoBehaviour
{
    public static UIContext Instance;

    private void Awake()
    {
        Instance = this;
    }
}
