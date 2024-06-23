using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellController : ProjectileBase
{
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
