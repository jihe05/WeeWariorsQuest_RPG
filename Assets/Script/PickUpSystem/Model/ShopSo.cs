using Inventory.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShopSo : ScriptableObject
{
    //������ �߰�
    [SerializeField]
    private List<ShopInvenItem> shopitmes;

    //�κ��丮 ũ�� 
    [field: SerializeField]
    public int Size { get; private set; } = 8;

    public event Action<Dictionary<int, ShopInvenItem>> OnShopUpdata;

    public void AddItem(ShopInvenItem item)
    {
        AddItem(item.shopItem, item.coin);
    }

    public int AddItem(ItemSo item, int coin)
    {
        for (int i = 0; i < shopitmes.Count; 
            i++)
        {
            shopitmes[i] = new ShopInvenItem
            {
                shopItem = item,
                coin = coin

            };
            return coin;
           
        
        
        }
        return coin;

    }


    public Dictionary<int, ShopInvenItem> GetCurrentShopstate()
    { 
    
       Dictionary<int, ShopInvenItem> returnValeu = new Dictionary<int, ShopInvenItem>();

        for (int i = 0; i < shopitmes.Count; i++)
        {
            returnValeu[i] = shopitmes[i];//�κ��丮 ���¸� ��ųʸ��� �߰�

        }
        return returnValeu;
    
    }

  

}

//(Ư��) �ν����� â���� �ʵ带 ���� �� �� �ֵ��� ����
[Serializable]
public struct ShopInvenItem
{

    public int coin;

    public  ItemSo shopItem;

}
