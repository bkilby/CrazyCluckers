using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{

    public GameObject Enemy;
    public float CooldownInSeconds = 10;
    public AudioClip EnemySpawnSound;

    private AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {

        AudioSource = GetComponent<AudioSource>();

        StartCoroutine(SpawnEnemy());

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator SpawnEnemy()
    {

        yield return new WaitForSeconds(CooldownInSeconds);

        CooldownInSeconds -= 0.1f;

        if (CooldownInSeconds < 1)
        {
            CooldownInSeconds = 1;
        }

        // var positionX = Random.Range(-50, 50);
        // var positionZ = Random.Range(-50, 50);
        var spawnPosition = Random.insideUnitSphere * 35;

        // Instantiate(Enemy, new Vector3(positionX, 1, positionZ), Quaternion.identity);
        Instantiate(Enemy, new Vector3(spawnPosition.x, 1, spawnPosition.z), Quaternion.identity);

        AudioSource.PlayOneShot(EnemySpawnSound);

        StartCoroutine(SpawnEnemy());
    }

}
