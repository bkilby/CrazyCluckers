using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEgg : MonoBehaviour
{

    public Camera Camera;

    public GameObject Projectile;
    public AudioClip ShootSound;
    public float CooldownInSeconds = 0.5f;

    public bool CanShoot = true;

    private AudioSource AudioSource;

    // Start is called before the first frame update


    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (CanShoot && !GameManager.Paused && Input.GetMouseButtonDown(0))
        {

            CanShoot = false;

            StartCoroutine(ShootCooldown());

            Instantiate(Projectile, Camera.main.transform.position + (Camera.main.transform.forward * 2), Camera.main.transform.rotation);

            AudioSource.PlayOneShot(ShootSound);

        }

    }


    IEnumerator ShootCooldown()
    {

        yield return new WaitForSeconds(CooldownInSeconds);

        CanShoot = true;

    }

}
