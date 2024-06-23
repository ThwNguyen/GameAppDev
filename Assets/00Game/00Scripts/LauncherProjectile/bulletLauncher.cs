using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletLauncher : ProjectileLauncherBase
{
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

        Vector3 point = launchPoint.transform.position;

        for (int i = 0; i < quantity; i++)
        {
            if (quantity > 1)
            {
                point = new Vector3(
                    point.x + Random.Range(-randomPoints.x, randomPoints.x),
                    point.y + Random.Range(-randomPoints.y, randomPoints.y),
                    point.z);
            }

            ProjectileBase projectile = objectPooling.Getcomp<ProjectileBase>(projectilePrefab);
            if (projectile != null)
            {
                projectile.Init(transform.parent.localScale);
                projectile.gameObject.SetActive(true);
                projectile.transform.position = point;
                projectile.transform.rotation = Quaternion.identity;

                Vector3 origScale = projectile.transform.localScale;
                projectile.transform.localScale = new Vector3(
                    origScale.x * transform.parent.localScale.x > 0 ? 1 : -1,
                    origScale.y,
                    origScale.z);

                projectile.transform.parent = bulletContainer;
            }
        }
    }
}
