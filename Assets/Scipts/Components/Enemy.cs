using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Color damageColor;
    [SerializeField] private Renderer renderer;
    [SerializeField] private float timeBetweenFire;
    [SerializeField] private Bullet bigBullet;
    [SerializeField] private Transform muzzle;

    private int currentHealth = 0;
    private float lastShootTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;  
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastShootTime > timeBetweenFire) {
            Shoot();
        }  
    }

    private void Shoot() {
        lastShootTime = Time.time;
        Bullet go = Instantiate(bigBullet, muzzle.position, Quaternion.identity);
    }

    public void TakeDamage(Vector3 hitPoint, int damage) { 
        currentHealth -= damage;
        renderer.material.DOColor(damageColor, 0.1f);
        renderer.material.DOColor(Color.white, 0.1f).SetDelay(0.1f);
       
        Debug.Log("current health : " + currentHealth);
        if (currentHealth <= maxHealth / 2) { 
        }
        if (currentHealth <= 0)
        {
            Death();        
        }
    }

    public void Death() {
        Destroy(gameObject);
    }
}
