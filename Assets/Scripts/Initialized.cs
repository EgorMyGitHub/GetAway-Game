using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialized : MonoBehaviour
{
    [SerializeField] private Player player;
    
    private void Awake()
    {
        ComponentRoot.Bind(player);
    }
}
