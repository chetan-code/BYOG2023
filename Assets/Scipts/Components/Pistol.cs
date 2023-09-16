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

    public override void Shoot(Vector3 startPos,Vector3 aimPos)
    {
        
        if (Time.time - shootTime < timeBetweenShoot)
        {
            Debug.Log("cant shoot");
            return;
        }
        else {
            shootTime = Time.time;
            if (Physics.Raycast(startPos, (aimPos- startPos), out hit, raycastDist))
            {
                if (hit.collider != null)
                {
                    Enemy enemy = hit.collider.GetComponent<Enemy>();

                    StartCoroutine(TrailEffect(hit.point, enemy));
                    Debug.Log(" Shoot Damage to : " + hit.collider.name);
                }
            }
            else
            {
                StartCoroutine(TrailEffect(nozzle.position + (nozzle.forward * raycastDist), null));
                    Debug.Log("Shoot - Fake Trail");
            }

        }
    }

    private IEnumerator TrailEffect(Vector3 hitPoint , Enemy enemy) {
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
        if (enemy != null) {
            enemy.TakeDamage(hitPoint, damagePerHit);
        }
    }


    
}
