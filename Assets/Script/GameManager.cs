using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManerage : MonoBehaviour
{
    public static GameManerage instance;
    public int sunNum;

    public GameObject bornParent;
    public GameObject zombiePrefab;
    public float createZombieTime;

    private int zombieOrderIndex;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        UIManager.Instance.InitUI();
        CreateZombie();
        zombieOrderIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSunNum(int changNum)
    {
        sunNum += changNum;

        if(sunNum <= 0)
        {
            sunNum = 0;
        }

        UIManager.Instance.UpdateUI();
    }

    public void CreateZombie()
    {
        StartCoroutine(DalayCreateZombie());
    }

    IEnumerator DalayCreateZombie()
    {
        yield return new WaitForSeconds(createZombieTime);

        GameObject zombie = Instantiate(zombiePrefab);
        int index = Random.Range(0, 5);
        zombie.transform.parent = bornParent.transform.Find("born (" + index.ToString() + ")");
        zombie.transform.localPosition = Vector3.zero;
        zombie.GetComponent<SpriteRenderer>().sortingOrder = zombieOrderIndex++ + (index + 1) * 20;
        StartCoroutine(DalayCreateZombie());
    }   
}
