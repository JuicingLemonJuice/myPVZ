using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : Plant
{
    public float readTime;
    private float timer;
    public GameObject sun;
    private Vector3 sunPos;
    private System.Random rand;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        timer = 0;
        sunPos = new Vector3(50, 25, 0);
        rand = new System.Random();
        health = 100;
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= readTime)
        {
            timer = 0;
            animator.SetTrigger("produce");
            sunPos.x = rand.Next(-50, 50);
            Instantiate(sun, transform.position + sunPos, Quaternion.identity);
        }
    }
}
