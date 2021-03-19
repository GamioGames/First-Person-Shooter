using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairDamage_UI : MonoBehaviour
{
    public float startSize;
    public float maxSize;
    [Tooltip("Damage ui duration")]
    public float maxTime;

    private RectTransform croshair;
    private CanvasGroup group;
    private float actualTime;
    private void Awake()
    {
        croshair = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
        croshair.sizeDelta = new Vector2(startSize, startSize);
    }

    private void OnEnable()
    {
        NewHit(true);
    }

    public void NewHit(bool fadeIn, bool lastHit = false)
    {
        // Cahnge red or white color
        if (lastHit)
        {
            foreach(RectTransform child in croshair)
            {
                child.GetComponent<Image>().color = new Color(0.95f, 0.2f, 0.2f);
                LeanTween.size(croshair, new Vector2(maxSize+10f, maxSize+10f), 0.2F);
            }
        }
        else
        {
            foreach (RectTransform child in croshair)
            {
                child.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
        }


        actualTime = maxTime;
        if (fadeIn)
        {
            group.alpha = 0;
            LeanTween.alphaCanvas(group, 0.9f, 0.15f);
            croshair.sizeDelta = new Vector2(startSize, startSize);
            LeanTween.size(croshair, new Vector2(maxSize, maxSize), 0.2F);
        }
        else
        {
            group.alpha = 0.9f;

            if(croshair.sizeDelta.x <= (startSize + 0.1f))
            {
                LeanTween.size(croshair, new Vector2(maxSize, maxSize), 0.1F);
            }
            else if(croshair.sizeDelta.x >= (maxSize - 0.1f))
            {
                LeanTween.size(croshair, new Vector2(startSize, startSize), 0.1F);
            }
        }
    }

    private void Update()
    {
        actualTime -= Time.deltaTime;
        if (actualTime <= 0)
        {
            LeanTween.alphaCanvas(group, 0, 0.15f).setOnComplete(() => gameObject.SetActive(false));
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            NewHit(false,true);
        }
    }
}
