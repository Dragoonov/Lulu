﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Initialize()
    {

        for (int i = 0; i < 6; i++)
        {
            GameObject helper = GameObject.Find("Helper" + i).transform.GetChild(0).gameObject;
            GameObject tint1 = helper.transform.GetChild(0).gameObject;
            GameObject tint2 = helper.transform.GetChild(1).gameObject;
            GameObject result = helper.transform.GetChild(2).gameObject;
            Color color1 = FindColorForTint(tint1);
            Color color2 = FindColorForTint(tint2);
            if(color1 == Color.black || color2 == Color.black)
            {
                helper.SetActive(false);
                continue;
            }
            tint1.GetComponent<Image>().color = color1;
            tint2.GetComponent<Image>().color = color2;
            result.GetComponent<Image>().color = MixColors(color1, color2);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Color FindColorForTint(GameObject tint)
    {
        string tintName = tint.name.Substring(0, tint.name.Length - 4);
        GameObject bluePrint = GameObject.Find(tintName + "Blueprint");
        if (bluePrint == null)
            return Color.black;
        Color blueprintColor = bluePrint.GetComponent<SpriteRenderer>().color;
        return new Color(blueprintColor.r, blueprintColor.g, blueprintColor.b, 1);
    }

    private Color MixColors(Color color1, Color color2)
    {
        return (color1 + color2) / 2;
    }
}
