using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peaShooter : Plant
{
    public float interval;//间隔
    private float timer;//起始时间
    public GameObject bullet;//子弹
    public Transform bulletPos;//子弹位置

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        timer = 0;
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
        if(timer >= interval)
        {
            timer = 0f;
            //实例一个对象（对象， 位置， 角度）
            //Quaternion.identity表示没有任何旋转的四元数，它的值为(0, 0, 0, 1)
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }
    }
}
