using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSkill : ProjectileLauncherBase
{
    public float yPos = 0.2f;
    private ObjectPooling objectPooling;
    protected override void Start()
    {

        base.Start();
        objectPooling = ObjectPooling.Instant;

    }
    public override void LaunchProjectiles()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile Prefab is not set in the inspector.");
            return;
        }

        if (launchPoint == null)
        {
            Debug.LogError("Launch Point is not set in the inspector.");
            return;
        }
        for (int i = 0; i < quantity; i++) {
            Vector3 point = targetPoint.playerPosition;

            point = new Vector3(
                point.x + Random.Range(-randomPoints.x, randomPoints.x),
                point.y+yPos,
                point.z);
            // GameObject projectile = Instantiate(projectilePrefab, point, projectilePrefab.transform.rotation);

       
                ProjectileBase projectile = objectPooling.Getcomp<ProjectileBase>(projectilePrefab);


                projectile.gameObject.SetActive(true);


                projectile.transform.position = point;
                projectile.transform.rotation = Quaternion.identity;
                projectile.transform.parent = bulletContainer;
            

        }

    }



}
