using Inventory.Model;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    //아아템 정보 
    [field: SerializeField]
    public ItemSo InventoryItem { get; private set; }

    //개수는 1개
    [field: SerializeField]
    public int Quantity { get; set; } = 1;


    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
    }

    internal void DestroyItem()
    {
       GetComponent<Collider>().enabled = false;

        Destroy(gameObject);
      
    }

   
}
