using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{

    public float Speed = 2000f;
    public AudioClip DeathSound;

    private Rigidbody Rigidbody;
    private AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {

        AudioSource = GetComponent<AudioSource>();

        Rigidbody = GetComponent<Rigidbody>();

        // Rigidbody.AddForce(10, 0, 0, ForceMode.Impulse);

        Rigidbody.AddForce(transform.forward * Speed);

        StartCoroutine(DestroyEgg());

    }

    // Update is called once per frame
    void Update()
    {

        // transform.Translate(Vector3.forward * Time.deltaTime);



    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "Player")
        {

            Time.timeScale = 0;

            GameManager.Paused = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

        if (collision.gameObject.tag == "Enemy")
        {

            Destroy(collision.gameObject);

            AudioSource.PlayOneShot(DeathSound);
            GameManager.Score++;

        }

    }


    IEnumerator DestroyEgg()
    {

        yield return new WaitForSeconds(0.1f);

        if (Rigidbody.velocity == Vector3.zero)
        {

            Destroy(gameObject);

        }
        else
        {

            StartCoroutine(DestroyEgg());

        }

    }

}
