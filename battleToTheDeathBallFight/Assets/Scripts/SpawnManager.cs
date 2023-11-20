using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int waveNumber = 1;
    public int enemyCount;
    public GameObject powerupPrefab;
    private float spawnRange = 9.0f;
    
    void Start()
    {
        SpawnEnemyWave(waveNumber); // SpawnEnemyWave(3) is how many enemies we want to spawn.
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    
    void Update()
    {
        // keeping count of all of our enemies.
        enemyCount = FindObjectsOfType<Enemy>().Length; // FindObjectsOfType<>: Will be looking for all of the different Objects in our scene that has the <Enemy> script. Similar to "GetComponent()".
        // Length: Can turn our array's number( our array in this case is FindObjectsOfType<Enemy>() ) into our integer "EnemyCount".
        if (enemyCount == 0) // if our enemy count (this is like a countdown btw ... 321) ever becomes 0...
        {
            waveNumber++;// we'll spawn more enemies.
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }
    private Vector3 GenerateSpawnPosition() 
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ); // actual random position.
        return randomPos; // you need to add this return if it isn't a void.
        /*when we used void before instead of Vector3, it's because we just wanted to store smth in and not return anything,
      but now we want to return smth back which is "randomPos". randomPos is a Vector3 therefore we have to replace the void with Vector3
      and have to remember to return what we need from GenerateSpawnPosition() which is randomPos.
    */
    }

    void SpawnEnemyWave(int enemiesToSpawn) // we can set int's and stuff to our void(as many as we want, just add commas in between), but it's best if we just kept it the minimum amount.
    {
        for (int i = 0; i < enemiesToSpawn; i++) // where (to start); when(it should stop); how(we're going to get there);
        {
            //Instantiate(enemyPrefab, randomPos, enemyPrefab.transform.rotation);
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        } 
    }
}
