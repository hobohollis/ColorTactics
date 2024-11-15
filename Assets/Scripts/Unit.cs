using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{

private Tile  _occupiedTile;
    private void Awake()
    {

       // StartCoroutine(OnSpawnMovement());

        HandleSpawnAnimation();

    }

    public abstract void HandleSpawnAnimation();

    public void MoveToTile(Tile tile)
    {
        _occupiedTile.ClearOccupiedUnit();
        _occupiedTile = tile;
        transform.DOMove(_occupiedTile.TileUnitPlacement.transform.position, 1f).SetEase(Ease.OutBounce);
    }

    public void Init(Tile tile)
    {
       _occupiedTile = tile;
    }
}
