using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Pistol : Gun
{
    [SerializeField] private TrailRenderer trail;

    private void Start()
    {
        timeBetweenShoot = 1f / fireRate;
    }

    public override void Shoot()
    {
        Debug.Log("[Pistol] : Shoot");
        if (Time.time - shootTime < timeBetweenShoot)
        {
            Debug.Log("cant shoot");
            return;
        }
        else {
            Debug.Log("else shoot");
            shootTime = Time.time;
            if (Physics.Raycast(nozzle.position, nozzle.forward, out hit, raycastDist))
            {
                if (hit.collider != null)
                {
                    StartCoroutine(TrailEffect(nozzle.position + (nozzle.forward * raycastDist)));
                    Debug.Log(" Shoot Damage");
                }
            }
            else
            {
                StartCoroutine(TrailEffect(nozzle.position + (nozzle.forward * raycastDist)));
                    Debug.Log("Shoot - Fake Trail");
            }

        }
    }

    private IEnumerator TrailEffect(Vector3 hitPoint) {
        trail.emitting = false;
        float timeSpent = 0;
        Vector3 startPos = nozzle.position;
        trail.transform.position = startPos;
        trail.emitting = true;
        while (timeSpent < timeBetweenShoot)
        {
            
            trail.transform.position = Vector3.Lerp(hitPoint, startPos, (timeBetweenShoot - timeSpent)/timeBetweenShoot);
            timeSpent += Time.deltaTime;
            yield return null;
        }
    }


    
}
