using Inventory.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShopSo : ScriptableObject
{
    //데이터 추가
    [SerializeField]
    private List<ShopInvenItem> shopitmes;

    //인벤토리 크기 
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
            returnValeu[i] = shopitmes[i];//인벤토리 상태를 딕셔너리의 추가

        }
        return returnValeu;
    
    }

  

}

//(특성) 인스펙터 창에서 필드를 설정 할 수 있도록 해줌
[Serializable]
public struct ShopInvenItem
{

    public int coin;

    public  ItemSo shopItem;

}
