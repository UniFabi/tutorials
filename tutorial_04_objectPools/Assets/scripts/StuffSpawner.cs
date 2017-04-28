//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class StuffSpawner : MonoBehaviour {


    public float velocity;
    public Stuff[] stuffPrefabs;
    float timeSinceLastSpawn;
    public FloatRange timeBetweenSpawns, scale, randomVelocity, angularVelocity;
    public Material stuffMaterial;
    
    float currentSpawnDelay;

    void SpawnStuff(){
        Stuff prefab = stuffPrefabs[Random.Range(0,stuffPrefabs.Length)];
        Stuff spawn = prefab.GetPooledInstance<Stuff>();


        spawn.transform.localPosition = transform.position;
        spawn.transform.localScale = Vector3.one * scale.RandomInRange;
        spawn.transform.localRotation = Random.rotation;
        spawn.Body.velocity = transform.up * velocity + Random.onUnitSphere * randomVelocity.RandomInRange;
        spawn.Body.angularVelocity = Random.onUnitSphere * angularVelocity.RandomInRange;
        spawn.SetMaterial(stuffMaterial);

    }

    void FixedUpdate () {
        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= timeBetweenSpawns.RandomInRange){
            timeSinceLastSpawn -= currentSpawnDelay;
            currentSpawnDelay = timeBetweenSpawns.RandomInRange;
            SpawnStuff();
        }    
    }
}
