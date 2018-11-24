using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public GameObject zombiePrefab;
    public GameObject dynamicWaypointPrefab;
    public Transform gameAreaCenter;
    public float gameAreaSizeX = 40;
    public float gameAreaSizeZ = 55;

    GameObject dynamicWaypoint;
    bool groundFound;
    
    // Use this for initialization
    void Start()
    {
        Invoke("SpawnZombie", 2);
    }

    void SpawnZombie()
    {
        dynamicWaypoint = Instantiate(dynamicWaypointPrefab);

        //place the dynamic waypoint randomly
        TeleportAtRandomPosition(dynamicWaypoint);
        TeleportAtRandomPosition(transform.gameObject);

        GameObject zombie = (GameObject)Instantiate(zombiePrefab, this.transform.position, Quaternion.identity);
        
        ZombieAI zombieScript = zombie.GetComponent<ZombieAI>();
        zombieScript.waypoint = dynamicWaypoint.transform;
        zombieScript.SetTarget(dynamicWaypoint.transform);
        zombieScript.mySpawner = this;
        
        Invoke("SpawnZombie", Random.Range(20, 30));
    }

    public void TeleportAtRandomPosition(GameObject objectToTeleport)
    {
        //zombies can't climb in this game, so we have to ensure that the waypoint is placed on the ground and not in the roof of a building
        while (!groundFound)
        {
            objectToTeleport.transform.position = new Vector3(gameAreaCenter.position.x + Random.Range(-gameAreaSizeX / 2.0f, gameAreaSizeX / 2.0f),
                50,
                gameAreaCenter.position.z + Random.Range(-gameAreaSizeZ / 2.0f, gameAreaSizeZ / 2.0f));
            RaycastHit hit;
            if (Physics.Raycast(new Ray(objectToTeleport.transform.position, Vector3.down), out hit, 100f))
            {
                if (hit.collider.tag == "CityGround")
                {
                    objectToTeleport.transform.position = hit.point;
                    groundFound = true;
                }
            }
        }

        groundFound = false;
    }
}
