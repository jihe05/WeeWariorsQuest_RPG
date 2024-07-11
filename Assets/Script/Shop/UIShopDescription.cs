using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopDescription : MonoBehaviour
{
    
    [SerializeField]
    private Text title;//������ �̸�

    //������ ȿ��
    [SerializeField]
    private Text stats;


    public void Awake()
    {
        ResetShopDescription();
    }

    //���� �ʱ�ȭ
    public void ResetShopDescription()
    {
        title.text = string.Empty;
        stats.text = string.Empty;

    }

    //���� ����
    public void SetShopDescription(string itmeName)
    {
        title.text = itmeName;

    }

    //���� ���� ����
    public void SetShopEfficacy(string itemName, string itemHp, string itemXp, string itemStamina)
    {

        title.text = itemName;
        stats.text = $"itemHp : {itemHp} \n itemXp : {itemXp} \n itemStamina : {itemStamina}";

    }
}
