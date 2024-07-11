using Inventory.Model;
using Inventory.UI;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour , IPointerClickHandler
{
    [SerializeField]
    private Image Itemimage;//������ �̹���

    [SerializeField]
    private Text ItemPrice;//������ ����

    //������ 1��
    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    //Ŭ�� , ���
    public event Action<ShopItem> OnItemClicked, InItemDroppedOn;


    private void Start()
    {
        //ũ�� �ʱ�ȭ
        gameObject.transform.localScale = Vector3.one;
    }

    public void SetItemData(Sprite sprite, int itePrice)
    { 
        Itemimage.sprite = sprite;
        ItemPrice.text = itePrice.ToString("N0");
        
    }
    

    //Ŭ�� �̺�Ʈ ó��
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnItemClicked?.Invoke(this);

        }
    }


    public void OnDrop(PointerEventData evenData)
    {
        InItemDroppedOn.Invoke(this);

    }

    public void ItePriceButtonClick()
    {
        int coin = int.Parse(ItemPrice.text);
        Sprite sprite = Itemimage.sprite;

        Debug.Log(sprite.ToString());

        UImanger.Instance.BayCoinAndImage(coin  , sprite);


    }





}
