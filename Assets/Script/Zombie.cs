using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Vector3 direction = new Vector3(-1, 0, 0);
    public float speed = 1;
    private bool isWalk;
    private Animator animator;

    public float damage;
    public float damageInterbal;
    private float damageTimer;

    private float lostHeadHealth;
    private float health;
    private float currentHealth;

    private GameObject head;
    private bool lostHead;
    private bool isDie;

    // Start is called before the first frame update
    void Start()
    {
        isWalk = true;
        animator = GetComponent<Animator>();
        damageTimer = 0;
        health = 100;
        currentHealth = health;
        lostHeadHealth = 30;
        head = transform.Find("Head").gameObject;
        isDie = false;
        lostHead = false;
    }

    // Update is called once per frame
    void Update()
    {
        OnAnimatorMove();

    }

    private void OnAnimatorMove()
    {
        if (isWalk)
        {
            //rigidbody2D.MovePosition(rigidbody2D.position + direction * animator.deltaPosition.magnitude);
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDie)
        {
            return;
        }

        if (collision.tag == "Plant")
        {
            isWalk = false;
            animator.SetBool("Walk", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDie)
        {
            return;
        }

        if (collision.tag == "Plant")
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterbal)
            {
                damageTimer = 0;
                Plant peaShooter = collision.GetComponent<Plant>();
                float newHealth = peaShooter.ChangeHealth(-damage);
/*                if (newHealth <= 0)
                {
                    isWalk = true;
                    animator.SetBool("Walk", true);
                }*/
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isDie)
        {
            return;
        }

        if (collision.tag == "Plant")
        {
            isWalk = true;
            animator.SetBool("Walk", true);
        }
    }

    public void ChangeHealth(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth + damage, 0, health);
        if (currentHealth <= lostHeadHealth && !lostHead)
        {
            lostHead = true;
            animator.SetBool("LostHead", true);
            head.SetActive(true);
        }

        if(currentHealth <= 0)
        {
            animator.SetTrigger("Die");
            isDie = true;
        }
    }

    public void DieAniOver()
    {
        animator.enabled = false;
        Destroy(gameObject);
    }
}
