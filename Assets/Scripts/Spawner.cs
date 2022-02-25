using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] int amountZombie = 10;
    private int  zombieSpawned = 0;
    [SerializeField] float _spawnTime = 1f;
    [SerializeField] Zombie _zombiePrefab;
    [SerializeField] Zombie _fastZombiePrefab;
    float _nextSpawnTime;
    // Update is called once per frame
    void Update()
    {
        if(ReadyToSpawn() && zombieSpawned <= amountZombie){
            StartCoroutine(Spawn());
        }
    }
    bool ReadyToSpawn() => Time.time >= _nextSpawnTime;
    IEnumerator Spawn(){
        zombieSpawned ++;
        _nextSpawnTime = Time.time + _spawnTime;
        Zombie zombie;
        if(zombieSpawned % 5 == 0){
            zombie = Instantiate(_fastZombiePrefab, transform.position, transform.rotation);
        }else
            zombie = Instantiate(_zombiePrefab, transform.position, transform.rotation);
        // Start anim of spawner here if you have
        yield return new WaitForSeconds(1f);
        zombie.StartWalking();
    }
}
