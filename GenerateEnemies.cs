using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject[] enemies;
    public Vector3 spawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeasWait;
    public static int enemigosMuertos = 0;
    public Text bajas;

    public int starWait;
    public bool stop;

    int randEnemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        spawnWait = Random.Range(spawnLeasWait, spawnMostWait);
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(starWait);

        while (!stop)
        {
            randEnemy = Random.Range(0, 2);
            
            Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), 3.861f, Random.Range(-spawnValues.z, spawnValues.z));
            Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }
}
