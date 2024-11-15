using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{

protected Tile  _occupiedTile;
public Tile OccupiedTile => _occupiedTile;
[SerializeField] protected Transform _modelContainer; 
    private void Awake()
    {

       // StartCoroutine(OnSpawnMovement());

        HandleSpawnAnimation();

    }

    public abstract void HandleSpawnAnimation();

    public virtual void MoveToTile(Tile tile)
    {
        if(_occupiedTile != null) _occupiedTile.ClearOccupiedUnit();
        _occupiedTile = tile;
        transform.DOMove(_occupiedTile.TileUnitPlacement.transform.position, 1f).SetEase(Ease.OutBounce);
    }

}
