using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIinventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        [SerializeField]
        private Image ItemImage;

        [SerializeField]
        private TextMeshProUGUI CountText;

        [SerializeField]
        private Image borderImage;//테두리


        //항목을 클릭 시, 아이템 드랍 시 , 아이템 드래그 시작 시, 아이템 드래그 종료 시,마우스 오른쪽 버튼 클릭 시
        public event Action<UIinventoryItem> OnItemClicked,
            OnItemDroppedOn, OnItemBegingDrag, OnItemEndDrag, OnRightMouseBtnClick;

        //슬롯이 비어있는가
        private bool empty = true;


        private void Start()
        {
            gameObject.transform.localScale = Vector3.one;
            Debug.Log($"Start {ItemImage}");
        }

        private void Awake()
        {
            ResetData();
            Deselect();
        }

        //아이템 데이터를 초기화하며 슬롯을 비움
        public void ResetData()
        {
           
           ItemImage.gameObject.SetActive(false);
          
            empty = true;
        }

        //아이템 데이터를 설정하여 슬롯을 채움
        public void setData(Sprite sprite, int quantity)
        {
           
            ItemImage.gameObject.SetActive(true);
            ItemImage.sprite = sprite;
            CountText.text = quantity + "";
            empty = false;
           
        }


        //아이템 테투리를 비활성화
        public void Deselect()
        {
           
            borderImage.enabled = false;
        }

        //아이템 테두리를 활성화
        public void Select()
        {
            borderImage.enabled = true;
        }



        //클릭시 이벤트 처리
        public void OnPointerClick(PointerEventData pointerData)
        {

            //버튼을 클릭했을때
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                //버튼 클릭 이벤트 발생
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                //일반 클릭 이벤트 발생
                OnItemClicked?.Invoke(this);
            }
        }

        //드래그 시작
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("4" + ItemImage);
            if (empty)
                return;
            //비어있지 않은 경우에만 이벤트 실행
            OnItemBegingDrag?.Invoke(this);
        }

        //드래그 종료
        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        //드랍
        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }
    }
}