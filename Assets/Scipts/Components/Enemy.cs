using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Color damageColor;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Transform bodyPartToShake;
    [SerializeField] private Rig rig;

    private int currentHealth = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(Vector3 hitPoint, int damage) { 
        currentHealth -= damage;
        renderer.material.DOColor(damageColor, 0.1f).SetLoops(2, LoopType.Yoyo);
        Debug.Log("current health : " + currentHealth);
        if (currentHealth <= maxHealth / 2) { 
            rig.weight = 0;
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
