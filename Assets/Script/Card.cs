using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject objectPrefab;
    private GameObject curGameobject;

    private GameObject darkBg;
    private GameObject progressBar;
    public float waitTime;
    public int useSunNum;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        darkBg = transform.Find("dark").gameObject;
        progressBar = transform.Find("progress").gameObject;
        useSunNum = 50;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UpdateProgress();
        UpdateDarkBg();
    }

    private void UpdateProgress()
    {
        float per = Mathf.Clamp(timer / waitTime, 0, 1);
        progressBar.GetComponent<Image>().fillAmount = 1 - per;
    }

    private void UpdateDarkBg()
    {
        if (progressBar.GetComponent<Image>().fillAmount == 0 && GameManerage.instance.sunNum >= useSunNum){
            darkBg.SetActive(false);
        }
        else
        {
            darkBg.SetActive(true);
        }
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        if (darkBg.activeSelf)
        {
            return;
        }
        //PointerEventData pointerEventData = data as pointerEventData;
        curGameobject = Instantiate(objectPrefab);
        curGameobject.transform.position = TranlateScreenToWorld(pointerEventData.position);
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if(curGameobject == null)
        {
            return;
        }

        curGameobject.transform.position = TranlateScreenToWorld(pointerEventData.position);
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if(curGameobject == null)
        {
            return;
        }

        Collider2D[] col = Physics2D.OverlapPointAll(TranlateScreenToWorld(pointerEventData.position));

        foreach (Collider2D c in col)
        {
            if(c.tag == "Land" && c.transform.childCount == 0)
            {
                curGameobject.transform.parent = c.transform;
                curGameobject.transform.localPosition = Vector3.zero;
                curGameobject.GetComponent<Plant>().setPlantStart();
                curGameobject = null;
                GameManerage.instance.ChangeSunNum(-useSunNum);
                timer = 0;
                break;
            }
        }

        if (curGameobject != null)
        {
            Destroy(curGameobject);
            curGameobject = null;
        }
    }

    private Vector3 TranlateScreenToWorld(Vector3 position){
        Vector3 cameraTranslatePos = Camera.main.ScreenToWorldPoint(position);
        return new Vector3(cameraTranslatePos.x, cameraTranslatePos.y);
    }
}
