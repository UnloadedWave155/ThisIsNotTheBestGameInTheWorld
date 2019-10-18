using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//script I got online
public class Spawner : MonoBehaviour
{
     public float spawnTime;        // The amount of time between each spawn.
     public float spawnDelay;       // The amount of time before spawning starts.
     public GameObject enemy;
	 public bool active;
 
     public int maxDistance;
     private Transform target;
	 
     private Transform myTransform;
 
     void Awake()
     {
         myTransform = transform;
 
     }
 
     void Start()
     {
		 active = false;
		 spawnTime=spawnDelay;
         GameObject stop = GameObject.FindGameObjectWithTag("player");
 
         target = stop.transform;
 
         maxDistance = 5;
 
         
     }
	 void FixedUpdate(){
		 //SpawnTimeDelay();
		if (Vector3.Distance(target.position, myTransform.position) < maxDistance && (spawnTime==spawnDelay))
            {
			active = true;
            Instantiate(enemy, transform.position, Quaternion.identity);
			spawnTime-=Time.deltaTime;
			}
		if(Vector3.Distance(target.position, myTransform.position) > maxDistance ){
			spawnTime=spawnDelay;
			active = false;			
		}
	 }
 
     /*IEnumerator SpawnTimeDelay()
     {
         while (true)
             {
                 if (Vector3.Distance(target.position, myTransform.position) < maxDistance)
                 {
                 Instantiate(enemy, transform.position, Quaternion.identity);
                 yield return new WaitForSeconds(spawnTime);
                 }
 
                 if (Vector3.Distance(target.position, myTransform.position) > maxDistance)
                 {
                     yield return null;
                 }                
             }
     }*/
 
}
