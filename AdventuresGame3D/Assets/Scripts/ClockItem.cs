using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockItem : MonoBehaviour
{

    public DayNightManager timeManager;
    public float rotationTime = 2.0f;
    public Transform gameAreaCenter;
    public float gameAreaSizeX = 40;
    public float gameAreaSizeZ = 55;

    // Use this for initialization
    void Start()
    {
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 360 * Time.deltaTime / rotationTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = new Vector3(gameAreaCenter.position.x + Random.Range(-gameAreaSizeX / 2.0f, gameAreaSizeX / 2.0f),
            50,
            gameAreaCenter.position.z + Random.Range(-gameAreaSizeZ / 2.0f, gameAreaSizeZ / 2.0f));
        RaycastHit hit;
        if (Physics.Raycast(new Ray(this.transform.position, Vector3.down), out hit, 100f))
        {
            transform.position = hit.point + Vector3.up;
        }
        timeManager.Reset();
    }
}
