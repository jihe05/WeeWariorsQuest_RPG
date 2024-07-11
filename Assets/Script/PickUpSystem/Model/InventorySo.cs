 using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Model
{
    //인벤토리 데이터를 관리하는 ScriptableObject
    [CreateAssetMenu]
    public class InventorySo : ScriptableObject
    {
        //인벤토리 데이터 추가
        [SerializeField]
        private List<InventoryItem> inventoryItems;

        //인벤토리 크리를 나타내는 속성, 기본값 10
        [field: SerializeField]
        public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;


        //인벤토리를 초기화하는 메소드
        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                //빈 아이템으로 초기화 
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }

        }

        public void AddItem(InventoryItem item)
        {
            //아이템의 이름과 설명을 받아와 메서드 호출
            AddItem(item.item, item.quantity);
        }

        //아이템을 추가하는 메서드
        public int AddItem(ItemSo item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = new InventoryItem
                    {
                        item = item,
                        quantity = quantity

                    };
                    return quantity;
                }
            }
            return quantity;

            /*
               if (item.IsStackable == false)
            { 
               for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if (IsInventoryFull())
                        return quantity;
                    while (quantity > 0 && IsInventoryFull() == false)
                    { 
                    
                    }
                    return quantity;
                }
            
            }
            quantity = AddStakableItem(item, quantity);
            InformAboutChange();
            return quantity;
             */

        }

        /*
        //주어진 아이템을 첫 번째 빈 슬롯에 추가하는 메서드 
        private int AddItemToFirstFreeSlot(ItemSo item, int quantity)
        {
            //newitem 생성
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity
            };

            //인벤토리의 모든 슬롯 확인
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                //빈슬롯을 찾으면 
                if (inventoryItems[i].IsEmpty)
                {
                    //아이템 추가 
                    inventoryItems[i]= newItem;
                    return quantity;//추가된 수량 반환
                }
            }
            return 0;
        }
        */
        /*
        //인벤토리가 가득 찼느지 여부 확인
        private bool IsInventoryFull()
            //빈슬롯리 없으면 true 있으면 false
         => inventoryItems.Where(item => item.IsEmpty).Any()== false;

        //스택 가능한 아이템을 인벤토리에 추가
        private int AddStackedleItem(ItemSo item, int quantity)
        {
            //인벤토리의 모든 슬롯을 확인
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                //빈슬롯은 건너뜀
                if (inventoryItems[i].IsEmpty)
                    continue;
                //동일한 아잍메을 찾으면 
                if (inventoryItems[i].item.ID == item.ID)
                {
                    //추가 가능한 최대 수량 확인 
                    int amountPossibleToTake =
                         inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    //추가할 수 있는 수량보다 남은 수량이 많으면 
                    if (quantity > amountPossibleToTake)
                    {
                        //슬롯을 최대치로 챙움
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQyantity(inventoryItems[i].item.MaxStackSize);
                        //남은 수량 감소 
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        //슬롯 채우고 남은 수량 반환
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQyantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            //빈 슬롯에 남은 슬롯 추가 
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }
        */

        //현재 인벤토리 상태를 딕셔너리 형태로 반환하는 메서드
        public Dictionary<int, InventoryItem> GetCurrentInventorystate()
        {
            Dictionary<int, InventoryItem> returnValue =
                  new Dictionary<int, InventoryItem>();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;//빈 아이템은 제외합니다.
                returnValue[i] = inventoryItems[i];//인벤토리 상태를 딕셔너리에 추가합니다.

            }
            return returnValue;

        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        //이동시이미지 변경
        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            InventoryItem item = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item;
            //받은 아이템을 딕셔너리 현태로 변환 
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            //딕셔너리 형태로 변환
            OnInventoryUpdated?.Invoke(GetCurrentInventorystate());
        }
    }

    [Serializable]
    public struct InventoryItem //구조체 
    {
        //아이템 수량
        public int quantity;
        //아이템 정보  
        public ItemSo item;

        //아이템 비어 있는지 여부를 반환하는 읽기 전용 속성
        public bool IsEmpty => item == null;

        //아이템 수량을 변경하여 새로운 InventoryItem을 반환하는 메서드
        public InventoryItem ChangeQyantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,

            };

        }

        //빈 InventoryItem을 반환하는 전적 메서드
        public static InventoryItem GetEmptyItem() => new InventoryItem
        {
            item = null,
            quantity = 0,
        };

    }



}
