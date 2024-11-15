using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private Unit _selectedUnit;
    private Camera cam;

    void Start()
    {
        cam = Camera.main; 
      
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) ProcessClick();
        if (Input.GetMouseButtonDown(1)) _selectedUnit = null;

    }

    private void ProcessClick()
    {
       /* 
        Debug.Log("initial click");
       var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
       var tile = TileManager.instance.GetTileAtPosition(mousePos);
       if (tile== null)
       {
           Debug.Log("click returned no tile");
           return;
       }

       Debug.Log($"click found tile {tile.name}");
       if(_selectedUnit != null && tile.OccuipiedUnit == null) tile.SetToOccupyTile(_selectedUnit);

       if (_selectedUnit == null && tile.OccuipiedUnit != null) _selectedUnit = tile.OccuipiedUnit;
       
       Debug.Log($"selcted unit is {_selectedUnit.name}");*/
       
       
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       RaycastHit hit;
       if (Physics.Raycast(ray, out hit, 100))
       {
           Debug.Log(hit.transform.name);
           Debug.Log("hit");
       }
       if(hit.transform == null) return;
       if(hit.transform.GetComponent<Tile>() == null) return;
       Tile tile = hit.transform.GetComponent<Tile>();
       if (tile == null) return;
       
       Debug.Log($"click found tile {tile.name}");
       if(_selectedUnit != null && tile.OccuipiedUnit == null) tile.SetToOccupyTile(_selectedUnit);

       if (_selectedUnit == null && tile.OccuipiedUnit != null) _selectedUnit = tile.OccuipiedUnit;

      if(_selectedUnit != null) Debug.Log($"selcted unit is {_selectedUnit.name}");
    }
}
