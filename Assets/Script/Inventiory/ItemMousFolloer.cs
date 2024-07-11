
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMousFolloer : MonoBehaviour
{
    [SerializeField]
    private Canvas Canvas;

    [SerializeField]
    private UIinventoryItem Item;

    private void Awake()
    {
        //현재 게임 오브젝트의 최상위 부모의 Canvers컴포넌트를 가져와 할당
        Canvas = transform.root.GetComponent<Canvas>();

        //자식인 오브젝트의 스크립트 가져와 할당
        Item = GetComponentInChildren<UIinventoryItem>();
    }

    
    public void SetData(Sprite sprite, int quantity)
    { 
        //아이템 데이터를 설정하여 슬롯을 채우는 함수 호출
         Item.setData(sprite, quantity);
    }

    private void Update()
    {
        Vector2 position;

        /*마우스 포인터의 화면 좌표를 캔버스의 로컬 좌표로 변환
         *RectTransformUtility : 위치 , 크기, 회전을 제어
         *ScreenPointToLocalPointInRectangle : 사각형 내의 화면 좌표를 로컬 좌표로 변환
         *(변환 기준 , 변환할 화면 좌표, 변환에 사용할 카메라, 변환된 로컬 좌표 저장)
         *마우스 위치를 UI 좌표계로 변환할 수 있음
        */
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)Canvas.transform, 
            Input.mousePosition, Canvas.worldCamera, out position);
        //마우스 포인터의 위치를 오브젝트의 위치로 설정
        transform.position = Canvas.transform.TransformPoint(position);
    }

    //전환 (꺼짐 / 켜짐)
    public void Toggle(bool val)
    {
       
        gameObject.SetActive(val);
    
    }

}
