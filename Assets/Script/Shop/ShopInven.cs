using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInven : MonoBehaviour
{
    [SerializeField]
    private ShopItem ShopItem_Prefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private UIShopDescription uIShopDescription;

    [SerializeField]
    private GameObject Goj_Stats;

    //�κ��丮 UI�׸� ����Ʈ
     private List<ShopItem> _listOfUIItme = new List<ShopItem>();

    //�̺�Ʈ(���� ��û��)
    public event Action<int> OnStartEnter;

    private void Awake()
    {
        Hide();
        Goj_Stats.SetActive(false);
        uIShopDescription.ResetShopDescription();

    }

  

    //������ ����
    public void InitShopUI()
    {
        for (int i = 0; i < 8; i++)
        {
            ShopItem shopItem = Instantiate(ShopItem_Prefab, Vector3.zero, Quaternion.identity);

            //������ ��ġ 
            shopItem.transform.SetParent(contentPanel);

            _listOfUIItme.Add(shopItem);

            //������ ����ó��
            shopItem.OnItemClicked += HandleItemSlelction;

        }
    
    }


    public void UpdateData(int itemIndex, Sprite itemImage, int itemCoin)
    {
        if (_listOfUIItme.Count > itemIndex)
        {
            //�������� ���ݰ� �̹��� ������Ʈ
            _listOfUIItme[itemIndex].SetItemData(itemImage, itemCoin);
        }
    }


    public void HandleItemSlelction(ShopItem shopItemUI)
    {
        int index = _listOfUIItme.IndexOf(shopItemUI);

        OnStartEnter?.Invoke(index);
    }



    //Ȱ��ȭ ������
    public void Show()
    {
        gameObject.SetActive(true);
        //ResetSelection();
    }


    //��Ȱ��ȭ ����
    public void Hide()
    {
        gameObject.SetActive(false);
        //ResetDraggedItem();//�巡�� ���� �޼���
    }

}
