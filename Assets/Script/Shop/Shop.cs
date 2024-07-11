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
                itemname = "»ç°ú";
                break;
            case 1:
                itemname = "±Ö";
                break;
        }

        Debug.Log($"{itemname} ±¸¸Å");
    }
}

