using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopDescription : MonoBehaviour
{
    
    [SerializeField]
    private Text title;//아이템 이름

    //아이템 효능
    [SerializeField]
    private Text stats;


    public void Awake()
    {
        ResetShopDescription();
    }

    //설명 초기화
    public void ResetShopDescription()
    {
        title.text = string.Empty;
        stats.text = string.Empty;

    }

    //설명 참조
    public void SetShopDescription(string itmeName)
    {
        title.text = itmeName;

    }

    //상점 설명 참조
    public void SetShopEfficacy(string itemName, string itemHp, string itemXp, string itemStamina)
    {

        title.text = itemName;
        stats.text = $"itemHp : {itemHp} \n itemXp : {itemXp} \n itemStamina : {itemStamina}";

    }
}
