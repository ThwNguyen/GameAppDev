using System.Collections;
using UnityEngine;

public class SparkController : ProjectileBase
{
  
   
   
    public float timerFire= 0f;



    protected override IEnumerator autoDestruct()
    {
        yield return new WaitForSeconds(timerFire);
        animator.SetTrigger(AnimationStrings.hitTrigger);
        base.autoDestruct();

    }

    protected override void Awake()
    {
        base.Awake();

    }
    protected override void Start()
    {
    }
    protected override void OnDisable()
    {

      
        if (rotineAutoDestruct != null)
            StopCoroutine(rotineAutoDestruct);
    }
    protected override void OnEnable()
    {
        rotineAutoDestruct = StartCoroutine(autoDestruct());
    }


}
