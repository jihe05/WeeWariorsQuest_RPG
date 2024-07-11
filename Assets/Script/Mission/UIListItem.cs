using UnityEngine;
using UnityEngine.UI;

// UI����Ʈ �������� �ʱ�ȭ�ϰ� ������Ʈ�ϴ� Ŭ����
public class UIListItem : MonoBehaviour
{
    public Image reward_image; // ���� �̹���
    public Text reward_amount; // ���� ����
    public Image imageName; // �̼Ǿ�����
    public Text textName; // �̼� ����
    public Slider missionProgressSlider;
    public Button completButton;
    public Image checkImage;
    public Image dontImage;

    public MissionInfo info; // �̼� ����


    // �ʱ�ȭ
    public void Init(MissionInfo info)
    {
       
        this.info = info;

        var data = DataManager.Instance.dicMissionDatas[this.info.id];
        this.textName.text = string.Format(data.mission_desc, data.goal);

        // ��������Ʈ ��θ� ������Ʈ�� ��η� ����
        var path = string.Format("Demo_Icon/{0}", data.sprite_name);

        this.imageName.sprite = Resources.Load<Sprite>(path);

        this.reward_amount.text = data.reward_amount.ToString();

        // �����̴� ��ư �ʱ�ȭ
        missionProgressSlider.maxValue = data.goal;
        missionProgressSlider.value = info.count;

        DataManager.Instance.AddUIListItem(this);

        UpdateUI();
    }

    // �̼� ���¸� ������Ʈ �ϴ� �޼���
    public void UpdateProgress(int progress)
    {
        Debug.Log("6"+progress);
        info.count = progress;
        missionProgressSlider.value = progress;
        UpdateUI();
    }

    // UI������Ʈ
    private void UpdateUI()
    {

        if (info.count >= missionProgressSlider.maxValue)
        {
            Debug.Log("1234");
            dontImage.gameObject.SetActive(false);
            completButton.gameObject.SetActive(true);
        }
        else
        {
            completButton.gameObject.SetActive(false);
        }
    }

    private void ClickButton()
    {
        // �̼� �Ϸ��� ���
        if (info.state == 1)
        {
            checkImage.gameObject.SetActive(true);
            completButton.gameObject.SetActive(false);
            PlayerManager.instance.PlayerLevelUpUpdate();
        }
        else
        {
            checkImage.gameObject.SetActive(false);
        }
    }

    // �̼� �Ϸ� ��ư
    public void OnCompleteButtonClick()
    {
        info.state = 1;
        ApplyMissionReward();
       DataManager.Instance.missionNews.SetActive(false);
        ClickButton();
      
    }

    public void ApplyMissionReward()
    {
        var missionData = DataManager.Instance.dicMissionDatas[info.id];
        UImanger.Instance.CoinAndImage(missionData.reward_amount);
        Debug.Log($"Reward applied for mission ID: {missionData.id}, Reward amount: {missionData.reward_amount}");
    }
}
