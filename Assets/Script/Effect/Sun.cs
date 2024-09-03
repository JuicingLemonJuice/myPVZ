using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    private float distance = 0;
    public Vector3 plusScale;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        distance += speed * Time.deltaTime;

        if (distance <= 50)
        { 
            transform.position += direction * speed * Time.deltaTime;
        }

        if(transform.localScale.x <= 1)
        {
            transform.localScale += plusScale;
        }
    }

    public void OnMouseDown()
    {
        Destroy(gameObject);
        GameManerage.instance.ChangeSunNum(25);
    }
}
