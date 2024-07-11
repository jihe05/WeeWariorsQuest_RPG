using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryDescription : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;//������ �̹���

        [SerializeField]
        private Text title;//������ �̸�

        [SerializeField]
        private Text description;//������ ����

        //������ ȿ��
        [SerializeField]
        private Text stats;


        public void Awake()
        {
            ResetDescription();
        }

        //���� �ʱ�ȭ
        public void ResetDescription()
        {
            itemImage.gameObject.SetActive(false);
            title.text = string.Empty;
            description.text = string.Empty;
            stats.text = string.Empty;

        }
        
        //���� ����
        public void SetDescription(Sprite sprite, string itmeName, string itemDescription)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            title.text = itmeName;
            description.text = itemDescription;

        }

        //���� ���� ����
        public void SetEfficacy(string itemName, string itemHp, string itemXp, string itemStamina)
        {
            stats.text = $"itemHp : {itemHp} \n itemXp : {itemXp} \n itemStamina : {itemStamina}";

        }




    }
}