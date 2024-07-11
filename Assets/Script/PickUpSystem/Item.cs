using Inventory.Model;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    //�ƾ��� ���� 
    [field: SerializeField]
    public ItemSo InventoryItem { get; private set; }

    //������ 1��
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
