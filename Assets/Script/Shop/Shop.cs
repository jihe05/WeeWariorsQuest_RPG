using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public void Buy(int key)
    {
        string itemname = string.Empty;
        switch (key)
        {
            case 0:
                itemname = "���";
                break;
            case 1:
                itemname = "��";
                break;
        }

        Debug.Log($"{itemname} ����");
    }
}

