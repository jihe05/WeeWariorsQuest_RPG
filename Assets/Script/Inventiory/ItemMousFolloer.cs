
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
        //���� ���� ������Ʈ�� �ֻ��� �θ��� Canvers������Ʈ�� ������ �Ҵ�
        Canvas = transform.root.GetComponent<Canvas>();

        //�ڽ��� ������Ʈ�� ��ũ��Ʈ ������ �Ҵ�
        Item = GetComponentInChildren<UIinventoryItem>();
    }

    
    public void SetData(Sprite sprite, int quantity)
    { 
        //������ �����͸� �����Ͽ� ������ ä��� �Լ� ȣ��
         Item.setData(sprite, quantity);
    }

    private void Update()
    {
        Vector2 position;

        /*���콺 �������� ȭ�� ��ǥ�� ĵ������ ���� ��ǥ�� ��ȯ
         *RectTransformUtility : ��ġ , ũ��, ȸ���� ����
         *ScreenPointToLocalPointInRectangle : �簢�� ���� ȭ�� ��ǥ�� ���� ��ǥ�� ��ȯ
         *(��ȯ ���� , ��ȯ�� ȭ�� ��ǥ, ��ȯ�� ����� ī�޶�, ��ȯ�� ���� ��ǥ ����)
         *���콺 ��ġ�� UI ��ǥ��� ��ȯ�� �� ����
        */
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)Canvas.transform, 
            Input.mousePosition, Canvas.worldCamera, out position);
        //���콺 �������� ��ġ�� ������Ʈ�� ��ġ�� ����
        transform.position = Canvas.transform.TransformPoint(position);
    }

    //��ȯ (���� / ����)
    public void Toggle(bool val)
    {
       
        gameObject.SetActive(val);
    
    }

}
