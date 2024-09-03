using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peaShooter : Plant
{
    public float interval;//���
    private float timer;//��ʼʱ��
    public GameObject bullet;//�ӵ�
    public Transform bulletPos;//�ӵ�λ��

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
            //ʵ��һ�����󣨶��� λ�ã� �Ƕȣ�
            //Quaternion.identity��ʾû���κ���ת����Ԫ��������ֵΪ(0, 0, 0, 1)
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }
    }
}
