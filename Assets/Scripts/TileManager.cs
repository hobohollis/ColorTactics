using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
       var startPos = _crackFill.transform.position;
        _crackFill.gameObject.SetActive(true);
        var distanceRaised = 0f;
        var raiseSpeed = 2.5f;
        var endHeightDiff = 2.5f; 
        while (distanceRaised < endHeightDiff)
        {
            var amountToRaise = Time.deltaTime * raiseSpeed;
            _crackFill.transform.position += Vector3.up * amountToRaise;
            distanceRaised += amountToRaise;
            yield return null;
        }
        _crackFill.transform.position = new Vector3(startPos.x, startPos.y + endHeightDiff,startPos.z);

        StartTileSpawns();
    }

    private void StartTileSpawns()
    {
        foreach (var tile in _tiles)
        {
            tile.SpawnUnit();
        }
    }
}
