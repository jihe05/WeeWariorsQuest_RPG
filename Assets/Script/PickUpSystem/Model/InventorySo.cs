 using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Model
{
    //�κ��丮 �����͸� �����ϴ� ScriptableObject
    [CreateAssetMenu]
    public class InventorySo : ScriptableObject
    {
        //�κ��丮 ������ �߰�
        [SerializeField]
        private List<InventoryItem> inventoryItems;

        //�κ��丮 ũ���� ��Ÿ���� �Ӽ�, �⺻�� 10
        [field: SerializeField]
        public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;


        //�κ��丮�� �ʱ�ȭ�ϴ� �޼ҵ�
        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                //�� ���������� �ʱ�ȭ 
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }

        }

        public void AddItem(InventoryItem item)
        {
            //�������� �̸��� ������ �޾ƿ� �޼��� ȣ��
            AddItem(item.item, item.quantity);
        }

        //�������� �߰��ϴ� �޼���
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
        //�־��� �������� ù ��° �� ���Կ� �߰��ϴ� �޼��� 
        private int AddItemToFirstFreeSlot(ItemSo item, int quantity)
        {
            //newitem ����
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity
            };

            //�κ��丮�� ��� ���� Ȯ��
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                //�󽽷��� ã���� 
                if (inventoryItems[i].IsEmpty)
                {
                    //������ �߰� 
                    inventoryItems[i]= newItem;
                    return quantity;//�߰��� ���� ��ȯ
                }
            }
            return 0;
        }
        */
        /*
        //�κ��丮�� ���� á���� ���� Ȯ��
        private bool IsInventoryFull()
            //�󽽷Ը� ������ true ������ false
         => inventoryItems.Where(item => item.IsEmpty).Any()== false;

        //���� ������ �������� �κ��丮�� �߰�
        private int AddStackedleItem(ItemSo item, int quantity)
        {
            //�κ��丮�� ��� ������ Ȯ��
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                //�󽽷��� �ǳʶ�
                if (inventoryItems[i].IsEmpty)
                    continue;
                //������ �Ɵ���� ã���� 
                if (inventoryItems[i].item.ID == item.ID)
                {
                    //�߰� ������ �ִ� ���� Ȯ�� 
                    int amountPossibleToTake =
                         inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    //�߰��� �� �ִ� �������� ���� ������ ������ 
                    if (quantity > amountPossibleToTake)
                    {
                        //������ �ִ�ġ�� ì��
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQyantity(inventoryItems[i].item.MaxStackSize);
                        //���� ���� ���� 
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        //���� ä��� ���� ���� ��ȯ
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQyantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            //�� ���Կ� ���� ���� �߰� 
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }
        */

        //���� �κ��丮 ���¸� ��ųʸ� ���·� ��ȯ�ϴ� �޼���
        public Dictionary<int, InventoryItem> GetCurrentInventorystate()
        {
            Dictionary<int, InventoryItem> returnValue =
                  new Dictionary<int, InventoryItem>();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;//�� �������� �����մϴ�.
                returnValue[i] = inventoryItems[i];//�κ��丮 ���¸� ��ųʸ��� �߰��մϴ�.

            }
            return returnValue;

        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        //�̵����̹��� ����
        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            InventoryItem item = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item;
            //���� �������� ��ųʸ� ���·� ��ȯ 
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            //��ųʸ� ���·� ��ȯ
            OnInventoryUpdated?.Invoke(GetCurrentInventorystate());
        }
    }

    [Serializable]
    public struct InventoryItem //����ü 
    {
        //������ ����
        public int quantity;
        //������ ����  
        public ItemSo item;

        //������ ��� �ִ��� ���θ� ��ȯ�ϴ� �б� ���� �Ӽ�
        public bool IsEmpty => item == null;

        //������ ������ �����Ͽ� ���ο� InventoryItem�� ��ȯ�ϴ� �޼���
        public InventoryItem ChangeQyantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,

            };

        }

        //�� InventoryItem�� ��ȯ�ϴ� ���� �޼���
        public static InventoryItem GetEmptyItem() => new InventoryItem
        {
            item = null,
            quantity = 0,
        };

    }



}
