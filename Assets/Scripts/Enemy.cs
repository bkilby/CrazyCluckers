using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Transform Player;
    public GameObject Projectile;
    public AudioClip ShootSound;
    public Transform EggSpawnLocation;

    private float DistanceFromPlayer = 0f;
    private float LookSpeed = 0.7f;
    private AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(Shoot());

        Player = GameObject.Find("Player").transform;

        AudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        LookAtPlayer();

        DistanceFromPlayer = Vector3.Distance(transform.position, Player.position);

        if (DistanceFromPlayer > 5)
        {

            transform.position += transform.forward * 4 * Time.deltaTime;

        }

        if (transform.position.y > 0)
        {

            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

    }

    void LookAtPlayer()
    {
        //transform.LookAt(Player);

        // Determine which direction to rotate towards
        var targetDirection = Player.position - transform.position;

        // The step size is equal to speed times frame time.
        var singleStep = LookSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        var newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    IEnumerator Shoot()
    {

        yield return new WaitForSeconds(1);

        if (DistanceFromPlayer <= 30)
        {

            var spawnPosition = transform.position;

            Instantiate(Projectile, EggSpawnLocation.position + (transform.forward * 2), transform.rotation);

            AudioSource.PlayOneShot(ShootSound);
            // Instantiate(Projectile, spawnPosition + (transform.forward * 5), transform.rotation);

        }

        StartCoroutine(Shoot());

    }

}
