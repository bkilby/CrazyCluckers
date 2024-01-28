using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkShootEgg : NetworkBehaviour
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
        if(!IsOwner)
        {
            return;
        }

        if (CanShoot && !GameManager.Paused && Input.GetMouseButtonDown(0))
        {

            CanShoot = false;

            StartCoroutine(ShootCooldown());

            ShootServerRpc();

            AudioSource.PlayOneShot(ShootSound);

        }

    }


    IEnumerator ShootCooldown()
    {

        yield return new WaitForSeconds(CooldownInSeconds);

        CanShoot = true;

    }

    [ServerRpc]

    private void ShootServerRpc()
    {
        GameObject go = Instantiate(Projectile, Camera.main.transform.position + (Camera.main.transform.forward * 2), Camera.main.transform.rotation);
        go.GetComponent<NetworkObject>().Spawn();
    }

}
