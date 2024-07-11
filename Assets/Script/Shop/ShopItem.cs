using Inventory.Model;
using Inventory.UI;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour , IPointerClickHandler
{
    [SerializeField]
    private Image Itemimage;//아이템 이미지

    [SerializeField]
    private Text ItemPrice;//아이템 가격

    //수량은 1개
    [field: SerializeField]
    public int Quantity { get; set; } = 1;

    //클릭 , 드랍
    public event Action<ShopItem> OnItemClicked, InItemDroppedOn;


    private void Start()
    {
        //크기 초기화
        gameObject.transform.localScale = Vector3.one;
    }

    public void SetItemData(Sprite sprite, int itePrice)
    { 
        Itemimage.sprite = sprite;
        ItemPrice.text = itePrice.ToString("N0");
        
    }
    

    //클릭 이벤트 처리
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
