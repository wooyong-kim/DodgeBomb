using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    public Image[] myHearts;
    int myLife = 3;
    public int Life
    {
        get => myLife;
        set
        {
            myLife = Mathf.Clamp(value, 0, myHearts.Length);
            for(int i = myHearts.Length - 1; i >= myHearts.Length - myLife; i--)
            {
                myHearts[i].enabled = true;
            }
            for(int i = 0; i <  myHearts.Length - myLife; i++)
            {
                myHearts[i].enabled = false;
            }
        }
    }
}
