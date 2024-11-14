using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _RotationSpeedX;
    [SerializeField] private float _RotationSpeedY;
    [SerializeField] private float _RotationSpeedZ;
    // Update is called once per frame
    void Update()
    { transform.Rotate(_RotationSpeedX * .1f,_RotationSpeedY* .1f, _RotationSpeedZ* .1f, Space.Self);
       Debug.Log("test"); 
    }
}
