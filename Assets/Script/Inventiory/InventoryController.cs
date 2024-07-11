using Inventory.Model;
using Ivnentory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ivnentory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private UIinventory InventoryUI;

        [SerializeField]
        private ShopInven ShopUI;

        [SerializeField]
        private InventorySo InventoryData;

        [SerializeField]
        private ShopSo shopData;

        public List<InventoryItem> InventoryItems = new List<InventoryItem>();
        public List<ShopInvenItem> ShopItems = new List<ShopInvenItem>();


        private void Start()
        {
            PrepareUI();
            PrepareInventorydata();
            PrepareShopData();
        }

        //인벤토리 데이터 준비
        private void PrepareInventorydata()
        {
            //인벤토리 초기화 함수 호출
            InventoryData.Initialize();
            //인벤토리 업데이트 이벤트에 UpdateInventoryUI 메서드 등록 
            InventoryData.OnInventoryUpdated += UpdateInventoryUI;

            foreach (InventoryItem item in InventoryItems)
            {
                if (item.IsEmpty)
                    continue;
                //아이쳄 추가 메서드 호출
                InventoryData.AddItem(item);
            }
            
        }

        private void PrepareShopData()
        {
            //shop업데이트
            shopData.OnShopUpdata += UpdateShopUI;

            foreach (ShopInvenItem item in ShopItems)
            {

               shopData.AddItem(item);
            }

        }

        //인벤토리 UI를 현재 인벤토리 상태로 업데이이트하는 메서드 
        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoruState)
        {
            //모든 인벤토리 초기화 
           InventoryUI.ResetAllItem();
            //이벤토리 상태를 반복하여 각 아잍메 데이터를 UI에 업데이트
            foreach (var item in inventoruState)
            {
                InventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);   
            }
        }

        //인벤토리 UI를 현재 인벤토리 상태로 업데이이트하는 메서드 
        private void UpdateShopUI(Dictionary<int, ShopInvenItem> ShopState)
        {
           
            foreach (var item in ShopState)
            {
                ShopUI.UpdateData(item.Key, item.Value.shopItem.ItemImage, item.Value.coin);
            }
        }


        //UI를 준비하고 이벤트 핸들러 설정하는 메서드
        private void PrepareUI()
        {
            InventoryUI.InitalizeInventoryUI(InventoryData.Size);//크기 초기화 
            ShopUI.InitShopUI();

            //이벤트 핸들러 설정(설정 요청 처리 / 아이템 교체 처리 / 드래그 처리 / 아이템 작업 요청)
            InventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            InventoryUI.OnSwapItems += HandleSwapItems;
            InventoryUI.OnStartDragging += HandleDragging;
            InventoryUI.OnItemactionRequsted += HandleItemActionRequset;

            ShopUI.OnStartEnter += HandleShopEnter;
        }

      
        //아이템 작업 표시 
        private void HandleItemActionRequset(int itemIndex)
        {

        }

        //드래그 처리 
        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = InventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            //드래그 아이템 이미지 설정
            InventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

      


        //아이템 교체 처리 
        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            InventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }


        //설정 요청 처리 
        private void HandleDescriptionRequest(int itemIndex)
        {
            //인벤토리 데이터에서 해당 인덱스의 아이템을 가져옴
            InventoryItem inventoryItme = InventoryData.GetItemAt(itemIndex);

            //아이템이 비어 있는지 확인
            if (inventoryItme.IsEmpty)
            {
                //비어있으면 초기화 
                InventoryUI.ResetSelection();
                return;

            }
            //아니면 아이템의 정보를 가져와 
            ItemSo item = inventoryItme.item;
            //설명 표시
            InventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description , item.ItemHp, item.ItemXp, item.ItemStamina);


        }

        // 상점 항목 선택 후 설명  호출 처리
        private void HandleShopEnter(int itemIndex)
        {
            Debug.Log("들어옴");
           // ShopInvenItem shopInvenItem = shopData.GetCurrentShopstate();


        }


        public void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.I))
            {
                InventoyrOnAndOf();

            }
           
        }

        public void InventoyrOnAndOf()
        {
            if (InventoryUI.isActiveAndEnabled == false)
            {
                //활성화
                InventoryUI.Show();

                //현재 인벤토리 상태를 딕셔너리 현태로 반환하는 메서드 호출
                foreach (var item in InventoryData.GetCurrentInventorystate())
                {
                    //이벤토리 UI에 데이터 업데이트(인벤스롯의 인덱스, 인벤 아이템 이미지, 인벤 아이템 수량)
                    InventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }

            }
            else
            {
                //비활성화
                InventoryUI.Hide();
            }
        }

        public void ShopInvenOnAndOf()
        {
            if (ShopUI.isActiveAndEnabled == false)
            {
               
                ShopUI.Show();

                foreach (var item in shopData.GetCurrentShopstate())
                {
                  
                   
                    ShopUI.UpdateData(item.Key, item.Value.shopItem.ItemImage, item.Value.coin);

                }

            }
            else 
            {

                ShopUI.Hide();
            }
        
        }


        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("WeaPon"))
            {
               ShopInvenOnAndOf();
            }

            if (other.CompareTag("Food"))
            {
                ShopInvenOnAndOf();
            }


            if (other.CompareTag("Potion"))
            {
               ShopInvenOnAndOf();
            }


        }

    }
}