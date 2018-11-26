using UnityEngine;

public class ZombieFieldOfView : MonoBehaviour
{

    public bool canSeeTarget;
    public float filedOfView = 55f;
    public Transform target;
    public Transform zombieEyePoint;
    public Transform playerEyePoint;
    public Vector3 lastKnownPosition = Vector3.zero;

    SphereCollider sphereCollider;
    
    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        lastKnownPosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerEyePoint = target.Find("eyesPoint");
    }

    bool InFieldOfView()
    {
        Vector3 directionToTarget = target.position - transform.position;
        float angle = Vector3.Angle(directionToTarget, transform.forward);

        //Debug.DrawLine(transform.position, target.position, Color.red);
        //Debug.DrawLine(transform.position, transform.forward * 20, Color.blue);
        if (angle <= filedOfView)
        {
            return true;
        }
        return false;
    }

    bool ClearLineOfSight()
    {
        RaycastHit hit;
        //Debug.DrawRay(zombieEyePoint.position, (playerEyePoint.position - zombieEyePoint.position).normalized * sphereCollider.radius, Color.black);
        if (Physics.Raycast(zombieEyePoint.position, (playerEyePoint.position - zombieEyePoint.position).normalized, out hit, sphereCollider.radius))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    void UpdateSight()
    {
        canSeeTarget = InFieldOfView() && ClearLineOfSight();
    }

    private void OnTriggerStay(Collider other)
    {
        UpdateSight();
        if (canSeeTarget)
        {
            lastKnownPosition = target.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        canSeeTarget = false;
    }
}
