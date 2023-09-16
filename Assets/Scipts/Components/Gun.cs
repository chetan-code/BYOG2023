using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected Transform nozzle;
    [SerializeField] protected int fireRate;
    [SerializeField] protected float reloadTime;
    [SerializeField] protected float raycastDist;   
    protected RaycastHit hit;
    protected float timeBetweenShoot;
    protected float shootTime;
    public abstract void Shoot();
}
