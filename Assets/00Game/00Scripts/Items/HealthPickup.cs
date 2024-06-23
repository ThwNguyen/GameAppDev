using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public float healthrestore = 20;

    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    //public AudioSource pickupSource;

    private void Awake()
    {
      //  pickupSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable && damageable.Health < damageable.MaxHealth)
        {
            bool wasHeald = damageable.Heal(healthrestore);


            if (wasHeald)//neu da mat mau va nhat binh mau
            {
                this.gameObject.SetActive(false);
                //if (pickupSource)
                //{
                //    // AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);

                //}
            }
        }

    }
    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;

    }
}
