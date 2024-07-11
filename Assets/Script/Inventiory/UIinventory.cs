using Inventory.UI;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Ivnentory.UI
{
    public class UIinventory : MonoBehaviour
    {

        //�κ� ������ ��һ���
        [SerializeField]
        private UIinventoryItem itmePrefab;

        //��ġ�� �г�
        [SerializeField]
        private RectTransform contentPanel;

        //������ ���� UI
        [SerializeField]
        private UIInventoryDescription itemUIDescription;

        //���콺 ������
        [SerializeField]
        private ItemMousFolloer MousFolloer;

        //�κ��丮 UI�׸� ����Ʈ
        List<UIinventoryItem> _listOfUIItme = new List<UIinventoryItem>();

        //�̺�Ʈ (���� ��û�� , ������ �۾� ��û��, �巡�� ���۽�, )
        public event Action<int>
            OnDescriptionRequested, OnItemactionRequsted, OnStartDragging;

        //�̺�Ʈ (������ ��ü��)
        public event Action<int, int> OnSwapItems;

        //currentlyDraggedItemIndex : ���� �巡�� ���� ������ �ε���
        private int currentlyDraggedItemIndex = -1;


        private void Start()
        {
            Hide();
        }
        private void Awake()
        {
            Debug.Log("Ȱ��ȭ ");
            Show();
            MousFolloer.Toggle(false);
            itemUIDescription.ResetDescription();
        }

        //UI Size�ʱ�ȭ 
        public void InitalizeInventoryUI(int inventorysize)
        {
            //�κ��丮�� ũ�� ��ŭ �ݺ�
            for (int i = 0; i < inventorysize; i++)
            {
                //inventoryItemUI�� ���� ��������
                UIinventoryItem uiItme = Instantiate(itmePrefab, Vector3.zero, Quaternion.identity);

                //������ ��ġ
                uiItme.transform.SetParent(contentPanel);

                //�߰�
                _listOfUIItme.Add(uiItme);
                //������ ���� ó�� / �巡�� ���� ó�� / ��ü ó�� / ������ �۾� ǥ�� ó��
                uiItme.OnItemClicked += HandleItemSelection;
                uiItme.OnItemBegingDrag += HandleBeginDrag;
                uiItme.OnItemDroppedOn += HandleSwap;
                uiItme.OnItemEndDrag += HandleEndDrag;
                uiItme.OnRightMouseBtnClick += HendleshowItemActions;

            }

        }

        //������ �ʱ�ȭ 
        internal void ResetAllItem()
        {
            //��� �������� 
            foreach (var item in _listOfUIItme)
            {
                item.ResetData();//������ �ʱ�ȭ
                Debug.Log(item);
                item.Deselect();//�׵θ� ��Ȱ��ȭ 
            }
        }

        //������ ���� ������Ʈ
        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description, string itemHp, string itemXp, string itemStamina)
        {
            //������ ����UI ������Ʈ
            itemUIDescription.SetDescription(itemImage, name, description);
            itemUIDescription.SetEfficacy(name, itemHp , itemXp, itemStamina);

            //��� ������ �׵θ� ��Ȱ��ȭ �޼��� ȣ��
            DeselecAllItes();

            //������ �������� �׵θ� Ȱ��ȭ 
            _listOfUIItme[itemIndex].Select();
        }

     


        //�ε����� ��ġ�� �������� �����͸� ������Ʈ 
        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (_listOfUIItme.Count > itemIndex)
            {
                //�������� �̹����� ������ ������Ʈ
                _listOfUIItme[itemIndex].setData(itemImage, itemQuantity);
                Debug.Log("1" + itemImage);
            }
        }



        //������ �۾� ǥ��
        private void HendleshowItemActions(UIinventoryItem inventoryItemUI)
        {

        }

        //�巡�� ����
        private void HandleEndDrag(UIinventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }


        //��ü ó��
        private void HandleSwap(UIinventoryItem inventoryItemUI)
        {
            int index = _listOfUIItme.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }

            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
            //������ ���� ó�� ȣ��
            HandleItemSelection(inventoryItemUI);

        }

        //�巡�� ����� ȣ��
        private void ResetDraggedItem()
        {
            MousFolloer.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        //�巡�� ���� ó��
        private void HandleBeginDrag(UIinventoryItem inventoryItemUI)
        {
            //IndexOf : ó������ ��Ÿ���� �ε��� ��ȯ
            int index = _listOfUIItme.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            currentlyDraggedItemIndex = index;
            //������ ���� ó�� ȣ��
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        //�巡�� ���� �����϶� ȣ��
        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            MousFolloer.Toggle(true);
            MousFolloer.SetData(sprite, quantity);
        }

        //������ ���� ó��
        public void HandleItemSelection(UIinventoryItem inventoryItemUI)
        {
            //������ �������� ����Ʈ���� ���°���� ������
            int index = _listOfUIItme.IndexOf(inventoryItemUI);

            //�ε����� 1�̸� ����
            if (index == -1)
                return;

            //�ƴϸ� ������ ���� ��û
            OnDescriptionRequested?.Invoke(index);
        }

        //Ȱ��ȭ ������
        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }


        public void ResetSelection()
        {
            //������ ���� �޼��� ȣ�� 
            itemUIDescription.ResetDescription();
            DeselecAllItes();
        }

        // ��� �������� ���� ��Ȱ��ȭ 
        public void DeselecAllItes()
        {
            foreach (UIinventoryItem item in _listOfUIItme)
            {
                item.Deselect();//�׵θ� ��Ȱ��ȭ 
            }
        }

        //��Ȱ��ȭ ����
        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();//�巡�� ���� �޼���
        }

        
    }
}