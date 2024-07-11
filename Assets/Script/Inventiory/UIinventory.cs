using Inventory.UI;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Ivnentory.UI
{
    public class UIinventory : MonoBehaviour
    {

        //인벤 아이템 요소생성
        [SerializeField]
        private UIinventoryItem itmePrefab;

        //배치될 패널
        [SerializeField]
        private RectTransform contentPanel;

        //아이템 설명 UI
        [SerializeField]
        private UIInventoryDescription itemUIDescription;

        //마우스 움직임
        [SerializeField]
        private ItemMousFolloer MousFolloer;

        //인벤토리 UI항목 리스트
        List<UIinventoryItem> _listOfUIItme = new List<UIinventoryItem>();

        //이벤트 (설명 요청시 , 아이템 작업 요청시, 드래그 시작시, )
        public event Action<int>
            OnDescriptionRequested, OnItemactionRequsted, OnStartDragging;

        //이벤트 (아이템 교체시)
        public event Action<int, int> OnSwapItems;

        //currentlyDraggedItemIndex : 현재 드래그 중인 아이템 인덱스
        private int currentlyDraggedItemIndex = -1;


        private void Start()
        {
            Hide();
        }
        private void Awake()
        {
            Debug.Log("활성화 ");
            Show();
            MousFolloer.Toggle(false);
            itemUIDescription.ResetDescription();
        }

        //UI Size초기화 
        public void InitalizeInventoryUI(int inventorysize)
        {
            //인벤토리의 크기 만큼 반복
            for (int i = 0; i < inventorysize; i++)
            {
                //inventoryItemUI의 정보 가져오기
                UIinventoryItem uiItme = Instantiate(itmePrefab, Vector3.zero, Quaternion.identity);

                //생성될 위치
                uiItme.transform.SetParent(contentPanel);

                //추가
                _listOfUIItme.Add(uiItme);
                //아이템 선택 처리 / 드래그 시작 처리 / 교체 처리 / 아이템 작업 표시 처리
                uiItme.OnItemClicked += HandleItemSelection;
                uiItme.OnItemBegingDrag += HandleBeginDrag;
                uiItme.OnItemDroppedOn += HandleSwap;
                uiItme.OnItemEndDrag += HandleEndDrag;
                uiItme.OnRightMouseBtnClick += HendleshowItemActions;

            }

        }

        //아이템 초기화 
        internal void ResetAllItem()
        {
            //모든 아이템의 
            foreach (var item in _listOfUIItme)
            {
                item.ResetData();//데이터 초기화
                Debug.Log(item);
                item.Deselect();//테두리 비활성화 
            }
        }

        //아이템 설명 업데이트
        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description, string itemHp, string itemXp, string itemStamina)
        {
            //아이템 설명UI 업데이트
            itemUIDescription.SetDescription(itemImage, name, description);
            itemUIDescription.SetEfficacy(name, itemHp , itemXp, itemStamina);

            //모든 아이템 테두리 비활성화 메서드 호출
            DeselecAllItes();

            //지정된 아이템의 테두리 활성화 
            _listOfUIItme[itemIndex].Select();
        }

     


        //인덱스의 위치한 아이템의 데이터를 업데이트 
        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (_listOfUIItme.Count > itemIndex)
            {
                //아이템의 이미지와 수량을 업데이트
                _listOfUIItme[itemIndex].setData(itemImage, itemQuantity);
                Debug.Log("1" + itemImage);
            }
        }



        //아이템 작업 표시
        private void HendleshowItemActions(UIinventoryItem inventoryItemUI)
        {

        }

        //드래그 종료
        private void HandleEndDrag(UIinventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }


        //교체 처리
        private void HandleSwap(UIinventoryItem inventoryItemUI)
        {
            int index = _listOfUIItme.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }

            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
            //아이템 선택 처리 호출
            HandleItemSelection(inventoryItemUI);

        }

        //드래그 종료시 호출
        private void ResetDraggedItem()
        {
            MousFolloer.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        //드래그 시작 처리
        private void HandleBeginDrag(UIinventoryItem inventoryItemUI)
        {
            //IndexOf : 처음으로 나타나는 인덱스 반환
            int index = _listOfUIItme.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            currentlyDraggedItemIndex = index;
            //아이템 선택 처리 호출
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        //드래그 중인 상태일때 호출
        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            MousFolloer.Toggle(true);
            MousFolloer.SetData(sprite, quantity);
        }

        //아이템 선택 처리
        public void HandleItemSelection(UIinventoryItem inventoryItemUI)
        {
            //선택한 아이템이 리스트에서 몇번째인지 가져옴
            int index = _listOfUIItme.IndexOf(inventoryItemUI);

            //인덱스가 1이면 종료
            if (index == -1)
                return;

            //아니면 아이템 설명 요청
            OnDescriptionRequested?.Invoke(index);
        }

        //활성화 될을때
        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }


        public void ResetSelection()
        {
            //아이템 설명 메서드 호출 
            itemUIDescription.ResetDescription();
            DeselecAllItes();
        }

        // 모든 아이템의 선택 비활성화 
        public void DeselecAllItes()
        {
            foreach (UIinventoryItem item in _listOfUIItme)
            {
                item.Deselect();//테두리 비활성화 
            }
        }

        //비활성화 상태
        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();//드래그 종료 메서드
        }

        
    }
}