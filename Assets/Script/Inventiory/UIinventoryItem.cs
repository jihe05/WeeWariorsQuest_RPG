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
        private Image borderImage;//�׵θ�


        //�׸��� Ŭ�� ��, ������ ��� �� , ������ �巡�� ���� ��, ������ �巡�� ���� ��,���콺 ������ ��ư Ŭ�� ��
        public event Action<UIinventoryItem> OnItemClicked,
            OnItemDroppedOn, OnItemBegingDrag, OnItemEndDrag, OnRightMouseBtnClick;

        //������ ����ִ°�
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

        //������ �����͸� �ʱ�ȭ�ϸ� ������ ���
        public void ResetData()
        {
           
           ItemImage.gameObject.SetActive(false);
          
            empty = true;
        }

        //������ �����͸� �����Ͽ� ������ ä��
        public void setData(Sprite sprite, int quantity)
        {
           
            ItemImage.gameObject.SetActive(true);
            ItemImage.sprite = sprite;
            CountText.text = quantity + "";
            empty = false;
           
        }


        //������ �������� ��Ȱ��ȭ
        public void Deselect()
        {
           
            borderImage.enabled = false;
        }

        //������ �׵θ��� Ȱ��ȭ
        public void Select()
        {
            borderImage.enabled = true;
        }



        //Ŭ���� �̺�Ʈ ó��
        public void OnPointerClick(PointerEventData pointerData)
        {

            //��ư�� Ŭ��������
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                //��ư Ŭ�� �̺�Ʈ �߻�
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                //�Ϲ� Ŭ�� �̺�Ʈ �߻�
                OnItemClicked?.Invoke(this);
            }
        }

        //�巡�� ����
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("4" + ItemImage);
            if (empty)
                return;
            //������� ���� ��쿡�� �̺�Ʈ ����
            OnItemBegingDrag?.Invoke(this);
        }

        //�巡�� ����
        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        //���
        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }
    }
}