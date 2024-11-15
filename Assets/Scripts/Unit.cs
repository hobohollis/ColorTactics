using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    private void Awake()
    {

       // StartCoroutine(OnSpawnMovement());

        HandleSpawnAnimation();

    }

    public abstract void HandleSpawnAnimation();
    
}
