using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public void DeathEnemy()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
