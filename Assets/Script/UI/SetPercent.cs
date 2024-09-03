using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m : MonoBehaviour
{
    private GameObject progress;
    private GameObject head;
    private GameObject levelText;
    private GameObject bg;
    private GameObject flag_prefab;

    // Start is called before the first frame update
    void Start()
    {
        progress = transform.Find("progress").gameObject;
        head = transform.Find("head").gameObject;
        levelText = transform.Find("levelText").gameObject;
        bg = transform.Find("bg").gameObject;
        flag_prefab = Resources.Load("Prefab/Flag") as GameObject;
    }

    public void SetPercent(float percent)
    {
        progress.GetComponent<Image>().fillAmount = percent;
        float originPosX = bg.GetComponent<RectTransform>().position.x + bg.GetComponent<RectTransform>().sizeDelta.x / 2;
        float width = bg.GetComponent<RectTransform>().sizeDelta.x;
        float offset = 0;
        head.GetComponent<RectTransform>().position = new Vector2(originPosX - percent * width + offset, head.GetComponent<RectTransform>().position.y);
    }

    public void SetFlagPercent(float percent)
    {
        float originPosX = bg.GetComponent<RectTransform>().position.x + bg.GetComponent<RectTransform>().sizeDelta.x / 2;
        float width = bg.GetComponent<RectTransform>().sizeDelta.x;
        float offset = 0;
        GameObject newFlag = Instantiate(flag_prefab);
        newFlag.transform.SetParent(transform, false);
        newFlag.GetComponent<RectTransform>().position = new Vector2(originPosX - percent * width + offset, newFlag.GetComponent<RectTransform>().position.y);
        head.transform.SetAsLastSibling();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
