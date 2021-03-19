using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PickUpInfo_UI : MonoBehaviour
{
    public Image weaponImage;
    public TMP_Text info;

    public void Set(string info)
    {
        weaponImage.color = new Color(1, 1, 1, 0);
        this.info.text = "Pick Up " + info;
    }
    public void Set(string info, Sprite icon)
    {
        weaponImage.sprite = icon;
        this.info.text = "Pick Up " + info;
    }
}
