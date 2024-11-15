using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SphereUnit : Unit
{
    
    public override void HandleSpawnAnimation()
    {
        StartCoroutine(OnSpawnMovement());
    }
    
    private IEnumerator OnSpawnMovement()
    {
        transform.DOScale(.5f, .01f);
        transform.DOMove(new Vector3(this.transform.position.x,transform.position.y +3.5f, transform.position.z), 3f).SetEase(Ease.OutSine);
        yield return new WaitForSeconds(1f);
        transform.DOScale(1f, 2f);
        yield return new WaitForSeconds(1.5f);
        transform.DOMove(new Vector3(this.transform.position.x,transform.position.y -1.5f, transform.position.z), 3f).SetEase(Ease.OutSine);
    }
}
