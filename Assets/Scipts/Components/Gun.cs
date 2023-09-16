using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected Transform nozzle;
    [SerializeField] protected int fireRate = 10;
    [SerializeField] protected float reloadTime = 5;
    [SerializeField] protected float raycastDist = 20;
    [SerializeField] protected int damagePerHit = 10;
    protected RaycastHit hit;
    protected float timeBetweenShoot;
    protected float shootTime;
    public abstract void Shoot(Vector3 startPos,Vector3 aimPos);
}
