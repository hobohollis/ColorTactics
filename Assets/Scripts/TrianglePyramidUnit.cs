using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TrianglePyramidUnit : Unit
{
    public override void HandleSpawnAnimation()
    {
        StartCoroutine(OnSpawnMovement());
    }
    
    private IEnumerator OnSpawnMovement()
    {
        _modelContainer.DOScale(.5f, .01f);
        _modelContainer.DOMove(new Vector3(this._modelContainer.position.x,_modelContainer.position.y +3.5f, _modelContainer.position.z), 3f).SetEase(Ease.OutSine);
        yield return new WaitForSeconds(1f);
        _modelContainer.DOScale(1f, 2f);
    }

    public override void MoveToTile(Tile tile)
    {
        
        if(_occupiedTile != null) _occupiedTile.ClearOccupiedUnit();
        _occupiedTile = tile;
        var dist = Vector3.Distance(this.transform.position, tile.transform.position);
        transform.DOMove(_occupiedTile.TileUnitPlacement.transform.position, dist*.2f).SetEase(Ease.InOutQuint);
    }
}
