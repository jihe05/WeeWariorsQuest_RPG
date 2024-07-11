using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instans;

    public GameObject Obj_Chat;

    public GameObject treasurBox_Open;
    public GameObject treasurBox_Close;
    public GameObject EventBox;
    public GameObject firstMission;
    public GameObject TimeLine;

    private void Awake()
    {
        Instans = this;
    }

    public void TalkePanelActive()
    {
        Obj_Chat.SetActive(true);
    }
    public void TalkePanelDestroy()
    {
        Obj_Chat.SetActive(true);

        PlayerPrefs.DeleteAll();
    }

    public void TreasurBox()
    {
        treasurBox_Open.SetActive(false);
        treasurBox_Close.SetActive(true);
        DataManager.Instance.CompleteMission(9);
        UImanger.Instance.CoinAndImage(10000);
    }

    public void TreasurBoxSetActive()
    {
        EventBox.gameObject.SetActive(false);
        
    }

    public void ColiderFalse()
    {
        firstMission.SetActive(false);
    }

    public void GuidFlyActive()
    {
        if (TimeLine.activeSelf == true)
        {
            TimeLine.SetActive(false);
        }
        else
        {
            TimeLine.SetActive(true);
        }
        
    }

}
