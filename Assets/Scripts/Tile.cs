using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
   [SerializeField] GameObject _tileModel;
   [SerializeField] private float _tileSpawnOffset = 15f;
   [SerializeField] private float _lowerSpeed = 1f;
   [SerializeField] private float _lowerSpeedDiff = 10f;
   [SerializeField] private GameObject _spawnPlane;

   [SerializeField] private Unit _unitToSpawn;
   private Vector3 _startPosition;

   public Action OnTileLowered;
   private Vector3 _initialScale;

   public void ActivateTile()
   {
      _initialScale = _tileModel.transform.localScale;
      StartCoroutine(LowerTileForSpawn());
   }

   private IEnumerator LowerTileForSpawn()
   {
      var spendDiff = Random.Range(-_lowerSpeedDiff, _lowerSpeedDiff);
      var distanceLowered = 0f;
      var lowerSpeed = _lowerSpeed + spendDiff;
      while (distanceLowered < _tileSpawnOffset)
      {
         var amountToLower = Time.deltaTime * lowerSpeed;
         _tileModel.transform.position += Vector3.down * amountToLower;
         distanceLowered += amountToLower;
         yield return null;
      }

      _tileModel.transform.position = _startPosition;
      OnTileLowered.Invoke();
   }

   public void InitializeModel()
   {
      _startPosition = _tileModel.transform.position;
      _tileModel.SetActive(true);
      _tileModel.transform.position = new Vector3(_tileModel.transform.position.x,
         _tileModel.transform.position.y + _tileSpawnOffset,
         _tileModel.transform.position.z);
   }

   public void SpawnUnit()
   {
     if (_unitToSpawn == null) return;
      StartCoroutine(SpawnUnitWithPortal());
   }

   private IEnumerator SpawnUnitWithPortal()
   {
      
      ////////// Lerp Color from clear to black
         
         float counter = 0;
         float duration = 1f;
         var spawnPlaneRenderer = _spawnPlane.GetComponent<Renderer>();
         while (counter < duration)
         {
            counter += Time.deltaTime;

            float colorTime = counter / duration;
            Debug.Log (colorTime);

            //Change color
            spawnPlaneRenderer.material.color = Color.Lerp (Color.clear, Color.black, counter / duration);
            //Wait for a frame
            yield return null;
         }
      
   
         ////////// X and Y scales to grow dot into square
        
   float xScalecounter = 0;
   float xScaleduration = 1f;
   
   float yScalecounter = 0;
   float yScaleduration =1f;
   
   
   _spawnPlane.transform.DOScaleX(.2f, xScaleduration);
  
  
      while (xScalecounter < xScaleduration)
      {
         xScalecounter += Time.deltaTime;
         yield return null;
      }
      
      _spawnPlane.transform.DOScaleZ(.2f, yScaleduration);
      
      while (yScalecounter < yScaleduration)
      {
         yScalecounter += Time.deltaTime;
        
      
         yield return null;
      }
    
   var spawnedUnit = Instantiate(_unitToSpawn, transform.position, Quaternion.identity);
Debug.Log($"SpawnedUnit name is {spawnedUnit.name}");
   spawnedUnit.transform.DOMove(new Vector3(this.transform.position.x,transform.position.y +2, transform.position.z), 2f);
   yield return new WaitForSeconds(1f);
   
   float counterTwo = 0;
   float durationTwo = 1f;
   while (counterTwo < durationTwo)
   {
      counterTwo += Time.deltaTime;

      float colorTime = counterTwo / durationTwo;
      Debug.Log (colorTime);

      //Change color
      spawnPlaneRenderer.material.color = Color.Lerp (Color.black, Color.clear, counterTwo / durationTwo);
      //Wait for a frame
      yield return null;
   }
      
      yield return null;
   }

}
