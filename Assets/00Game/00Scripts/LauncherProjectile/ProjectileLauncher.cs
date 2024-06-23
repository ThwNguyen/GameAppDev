using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public ProjectileBase projectilePrefab;
    public Transform launchPoint;

    public ProjectileBase skill2Prefab;
    public Transform launchPoint2;

    public ProjectileBase spSkillPrefab;
    public Transform launchPoint3;

    public ProjectileBase diagonalPrefab;
    public Transform diagonalPoint;

    private ObjectPooling objectPooling;

    private rainPoint rainP;
    public Transform bulletContainer;
    private void Start()
    {
        objectPooling = ObjectPooling.Instant;
        rainP = launchPoint2.GetComponent<rainPoint>();
        bulletContainer = ThungChua.Instant.transform;
    }

    public void FireProjectile()
    {
        ProjectileBase projectile = objectPooling.Getcomp<ProjectileBase>(projectilePrefab);
        if (projectile != null)
        {
           
            projectile.Init(transform.parent.localScale);
            projectile.gameObject.SetActive(true);
            projectile.transform.position = launchPoint.transform.position;
            projectile.transform.rotation = Quaternion.identity;

            Vector3 origScale = projectile.transform.localScale;
           if(origScale.x != transform.parent.localScale.x)
            {
                projectile.transform.localScale = new Vector3(
              origScale.x * transform.parent.localScale.x > 0 ? 1 : -1,
              origScale.y,
              origScale.z);
             

            }
          
            projectile.transform.parent = bulletContainer;
        }
    }


    public void DiagonalProjectile()
    {
        ProjectileBase projectile = objectPooling.Getcomp<ProjectileBase>(diagonalPrefab);


        if (projectile != null)
        {
            projectile.Init(transform.parent.localScale);
            projectile.gameObject.SetActive(true);
            projectile.transform.position = diagonalPoint.transform.position;
            projectile.transform.rotation = Quaternion.identity;

          

            Vector3 origScale = projectile.transform.localScale;
            if (origScale.x != transform.parent.localScale.x)
            {
                projectile.transform.localScale = new Vector3(
              origScale.x * transform.parent.localScale.x > 0 ? 1 : -1,
              origScale.y,
              origScale.z);
              

            }
       
            projectile.transform.parent = bulletContainer;
        }
    }

    public void Skill2()
    {
        Vector3 rainPoint = launchPoint2.GetComponent<rainPoint>().GetClosestEnemyPosition();
       

        ProjectileBase projectile = objectPooling.Getcomp<ProjectileBase>(skill2Prefab);
      

        if (projectile != null)
        {
          
            projectile.Init(transform.parent.localScale);
            projectile.gameObject.SetActive(true);
            projectile.transform.position = rainP.HasTarget ? rainPoint : launchPoint2.transform.position;
            projectile.transform.rotation = Quaternion.identity;

          
            Vector3 origScale = projectile.transform.localScale;
            if (origScale.x != transform.parent.localScale.x)
            {
                projectile.transform.localScale = new Vector3(
              origScale.x * transform.parent.localScale.x > 0 ? 1 : -1,
              origScale.y,
              origScale.z);
               

            }
           
            projectile.transform.parent = bulletContainer;
        }
    }

    public void SPSkill()
    {
        ProjectileBase projectile = objectPooling.Getcomp<ProjectileBase>(spSkillPrefab);
      
      


        if (projectile != null)
        {
           
            projectile.Init(transform.parent.localScale);
            projectile.gameObject.SetActive(true);
            projectile.transform.position = launchPoint3.transform.position;
            projectile.transform.rotation = Quaternion.identity;


            Vector3 origScale = projectile.transform.localScale;
            if (origScale.x != transform.parent.localScale.x)
            {
                projectile.transform.localScale = new Vector3(
              origScale.x * transform.parent.localScale.x > 0 ? 1 : -1,
              origScale.y,
              origScale.z);
               

            }
           
            projectile.transform.parent = bulletContainer;
        }
    }
}
