using Inventory.Model;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UImanger : MonoBehaviour
{
    public static UImanger Instance { get; private set; }
 

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        StartCoin();
        PlayerHpData();
        BossHpData();
        BossBa.gameObject.SetActive(false);

    }

   
    //___________________Coin_________________________



    [Header("-Coin-")]
    public Text Text_playercoin;

    public int Coin = 10000;


    public void StartCoin()
    {
        Text_playercoin.text = Coin.ToString("N0");

    }

    public void BayCoinAndImage(int _coin , Sprite itemImage)
    {
        if (_coin < Coin  && Coin != 0)
        {
            Coin -= _coin;
            _coin = Coin;
            Text_playercoin.text = _coin.ToString("N0");

            InventoryUpdate(itemImage);
           
        }
        else
        {
            return;
        }
      
    }

    public void CoinAndImage(int _coin)
    {
       Coin += _coin;
        Text_playercoin.text = Coin.ToString("N0");

        if (Coin > 99999)
        { 
          Text_playercoin.text = Coin.ToString("99999+");
        }
    }


    //_________________________________________________________



    [Header("-Inventory-")]
    //_____________________Inventory___________________________


    [SerializeField]
    private InventorySo inventoryData;

    private void InventoryUpdate(Sprite itemImage)
    {
      
        Item item = itemImage.GetComponent<Item>();
       

        if (item != null)
        {
           
            int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
         
            item.Quantity = reminder;

        }
        else
        {
            Debug.Log("null");
        }
    }
    //____________________________________________________________



    [Header("-Camera-")]
    //_________________________Camera_____________________________


    public Camera Selectcamera;
    public Transform newParent;
    public GameObject SelectCharacterPanel;

    // 호출될 때마다 움직이기 시작
    public void MoveLeft()
    {
        // 현재 위치를 가져옵니다.
        Vector3 currentPosition = Selectcamera.transform.position;
      
        if (currentPosition.x <= -64)
            return;
        else
            // X 좌표를 -2만큼 이동하고 Y, Z 좌표는 고정합니다.
            currentPosition.x -= 2f;



        // 변경된 위치를 설정합니다.
        Selectcamera.transform.position = currentPosition;

    }


    // 호출될 때마다 움직이기 시작
    public void MoveRight()
    {
        // 현재 위치를 가져옵니다.
        Vector3 currentPosition = Selectcamera.transform.position;

        if (currentPosition.x >= -58)
            return;
        else
            // X 좌표를 -2만큼 이동하고 Y, Z 좌표는 고정합니다.
            currentPosition.x += 2f;

        // 변경된 위치를 설정합니다.
        Selectcamera.transform.position = currentPosition;


    }

    public void OnMoveButtonClicked()
    {
        // 모든 MoveObject 스크립트를 가진 오브젝트를 찾고
        MoveObject[] moveObjects = FindObjectsOfType<MoveObject>();
      

        foreach (MoveObject moveObject in moveObjects)
        {
            // 카메라가 오브젝트를 비추고 있는지 확인
            if (IsObjectVisible(moveObject.transform))
            {
                // 오브젝트를 새로운 부모의 자식으로 이동
                moveObject.MoveToNewParentButton(newParent);

                SelectCharacterPanel.gameObject.SetActive(false);
               
            }
        }
    }

    // 오브젝트가 카메라 뷰포트 내에 있는지 확인
    private bool IsObjectVisible(Transform objectTransform)
    {
        Vector3 viewportPosition = Selectcamera.WorldToViewportPoint(objectTransform.position);
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1 && viewportPosition.z > 0;
    }


    //____________________________________________________________________

    [Header("-changePlayer-")]
    //_____________________changePlayer_________________________

    public GameObject WomanPlayer;
    public GameObject ManPlayer;

    public void MomanButton()
    {
        if (WomanPlayer.active == false)
        {
            ManPlayer.SetActive(false);
            WomanPlayer.SetActive(true);
        }

    }

    public void ManButton()
    {
        if (ManPlayer.active == false)
        {
            WomanPlayer.SetActive(false);
            ManPlayer.SetActive(true);
        }

    }


    //_________________________________________________________

    [Header("-playerHp-")]
    //__________________playerHp_______________________________

    public Slider Player_HpSlidebar;
    public float PlayerMaxHp = 10000;

    private void PlayerHpData()
    {
        Player_HpSlidebar.minValue = 0f;
        Player_HpSlidebar.maxValue = PlayerMaxHp;
        Player_HpSlidebar.value = PlayerMaxHp;
     
    }


    public void PlayerSliderbar(float Ap)
    { 
         Player_HpSlidebar.value -= Ap;
       
    }


    //_________________________________________________________

    [Header("-MonsterHp-")]
    //__________________MonsterHp_______________________________

    public GameObject Monster_HpSlidebarPrefab; // HP 바 프리팹

    public float MonsterMaxHp = 100;

    public void MonsterHpData(GameObject monster)
    {
        GameObject hpBarInstance = Instantiate(Monster_HpSlidebarPrefab, monster.transform);
        Slider slider = hpBarInstance.GetComponentInChildren<Slider>();
        slider.minValue = 0f;
        slider.maxValue = MonsterMaxHp;
        slider.value = MonsterMaxHp;
        hpBarInstance.transform.localPosition = new Vector3(0, 1.0f, 0); // 적절한 높이로 설정
    }

    public void MonsterSliderbar(Slider slider, float Ap)
    {
        slider.value -= Ap;
    }

    //_________________________________________________________

    [Header("-Boss-")]
    //__________________MonsterHp_______________________________

    public GameObject BossBa;
    public Slider Boss_HpSlidebar;
    public float BossMaxHp = 10000;

    public void SowHpBa()
    {
        BossBa.gameObject.SetActive(true);
    }

    private void BossHpData()
    {
        Boss_HpSlidebar.minValue = 0f;
        Boss_HpSlidebar.maxValue = BossMaxHp;
        Boss_HpSlidebar.value = BossMaxHp;
    }

    public void BossSliderbar(float Ap)
    {
        Boss_HpSlidebar.value -= Ap;

    }

    //_________________________________________________________


  
    //__________________Active_______________________________

    public void OnClickButtonActive(GameObject targetObject)
    {
        // GameObject가 비활성화 상태이면 활성화하고, 활성화 상태이면 비활성화
        if (!targetObject.activeSelf)
        {
            targetObject.SetActive(true);
        }
        else
        {
            targetObject.SetActive(false);
        }
    }

    //_________________________________________________________


    [Header("-SettingButtonActive-")]
    //__________________SettingButtonActive_______________________________

    public GameObject SoundPanel;
    public GameObject Graphicspanel;
    public GameObject ControlPanel;


    public void OnClickSettingButton(GameObject butonPanle)
    {
        //활서화면 냅두고 
        if (butonPanle.activeSelf)
        {
            return;
        }
        else
        {
            //아니면 다 비활성화 
            SoundPanel.SetActive(false);
            Graphicspanel.SetActive(false);
            ControlPanel.SetActive(false);

            butonPanle.SetActive(true);
        }

    }

    //_________________________________________________________

    [Header("-EventButton-")]
    //__________________EventButton_______________________________

    public GameObject Event_Button;
 
    //버튼 활성화 
    public void EventButton()
    {
        Event_Button.SetActive(true);
        Invoke("SetActiveBox",5f);
    }

    public void SetActiveBox()
    {
        Event_Button.SetActive(false);
        EventManager.Instans.TreasurBoxSetActive();
    }

    //_________________________________________________________

    [Header("LevelUp")]
    //__________________LevelUp_______________________________

    public GameObject LevelUP_Text;
    public Text Level;

    public void PlayerlevelUp(int level)
    {
        Debug.Log("활성화 ");
        LevelUP_Text.SetActive(true);
        Level.text = level.ToString();
    }


    //_________________________________________________________

    public GameObject Endingpanel;

    public void EndingpaenlActive()
    {
        Endingpanel.SetActive(true);


    }

}

