using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    protected float health;
    protected float currentHealth;

    protected bool isStart;
    protected Animator animator;
    protected BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isStart = false;
        animator = GetComponent<Animator>();
        animator.speed = 0;
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPlantStart()
    {
        isStart = true;
        animator.speed = 1;
        boxCollider2D.enabled = true;
    }

    public float ChangeHealth(float num)
    {
        currentHealth = Mathf.Clamp(currentHealth + num, 0, health);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        return currentHealth;
    }
}
