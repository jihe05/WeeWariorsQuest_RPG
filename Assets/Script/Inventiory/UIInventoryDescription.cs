using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryDescription : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;//아이템 이미지

        [SerializeField]
        private Text title;//아이템 이름

        [SerializeField]
        private Text description;//아이템 설명

        //아이템 효능
        [SerializeField]
        private Text stats;


        public void Awake()
        {
            ResetDescription();
        }

        //설명 초기화
        public void ResetDescription()
        {
            itemImage.gameObject.SetActive(false);
            title.text = string.Empty;
            description.text = string.Empty;
            stats.text = string.Empty;

        }
        
        //설명 참조
        public void SetDescription(Sprite sprite, string itmeName, string itemDescription)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            title.text = itmeName;
            description.text = itemDescription;

        }

        //상점 설명 참조
        public void SetEfficacy(string itemName, string itemHp, string itemXp, string itemStamina)
        {
            stats.text = $"itemHp : {itemHp} \n itemXp : {itemXp} \n itemStamina : {itemStamina}";

        }




    }
}