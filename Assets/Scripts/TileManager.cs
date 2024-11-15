using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private Transform _crackFill;
    private Tile[] _tiles;
    private int _numberOfTilesLowered;

    private void Start()
    {
        _crackFill.gameObject.SetActive(false);
      _tiles =  _tileMap.GetComponentsInChildren<Tile>();
      _tiles = _tiles.ToArray().OrderBy((d) => (d.transform.position - transform.position).sqrMagnitude).ToArray();
      
      foreach (var tile in _tiles)
      {
          tile.InitializeModel();
          tile.OnTileLowered += OnTileLowered;
      }
      StartCoroutine(IncrementalSetOffTiles ());
     
    }

    private void OnTileLowered()
    {
        _numberOfTilesLowered++;
        
        if(_numberOfTilesLowered == _tiles.Length)  StartCoroutine(ActivateCrackFill ());
            
    }

    public IEnumerator IncrementalSetOffTiles()
    {
        yield return null;
       int numberOfTilesLowered = 0;
        while (numberOfTilesLowered < _tiles.Length)
        {
            
           if( numberOfTilesLowered < _tiles.Length) _tiles[numberOfTilesLowered].ActivateTile();
           if( numberOfTilesLowered + 1 < _tiles.Length)_tiles[numberOfTilesLowered + 1].ActivateTile();
           if( numberOfTilesLowered + 2 < _tiles.Length) _tiles[numberOfTilesLowered + 2].ActivateTile();
            numberOfTilesLowered+= 3;
            yield return new WaitForSeconds(.1f);

        }
    }

    public IEnumerator ActivateCrackFill()
    {
        _crackFill.gameObject.SetActive(true);
        var raiseSpeed = 2.1f;
        var endHeightDiff = 2.5f; 
        StartTileSpawns();
        Vector3 goalScale = new Vector3(_crackFill.transform.localScale.x * 2, _crackFill.transform.localScale.y,
            _crackFill.transform.localScale.z * 2);
        _crackFill.DOScale(goalScale, 1);
        _crackFill.transform.DOMove(new Vector3( _crackFill.transform.position.x, _crackFill.transform.position.y + endHeightDiff ,  _crackFill.transform.position.z ), raiseSpeed);
        yield return null;
    }

    private void StartTileSpawns()
    {
        foreach (var tile in _tiles)
        {
            tile.SpawnUnit();
        }
    }
}
