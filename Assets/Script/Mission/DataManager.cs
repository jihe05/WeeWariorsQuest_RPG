using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance => instance;

    public MissionData[] missionDatas; // �̼� ������ �迭
    public MissionRewardData[] missionRewardDatas; // �̼� ���� ������ �迭

    public Dictionary<int, MissionData> dicMissionDatas; // �̼� ������ ��ųʸ�
    public Dictionary<int, MissionRewardData> dicMissionRewardDatas; // �̼� ���� ������ ��ųʸ�
    public List<MissionInfo> missionInfos; // �̼� ���� ���� ���
    public List<UIListItem> uiListItems; // UI ����Ʈ ������ ���

    public GameObject missionNews;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDictionaries();
            InitializeMissionInfos();
            uiListItems = new List<UIListItem>(); // ����Ʈ �ʱ�ȭ
            missionNews.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void CompleteMission(int missionId)
    {
        var missionData = DataManager.Instance.dicMissionDatas[missionId];
        UpdateMissionProgress(missionId, missionData.goal); // �̼� �Ϸ�
    }

    private void InitializeDictionaries()
    {
        dicMissionDatas = new Dictionary<int, MissionData>();
        dicMissionRewardDatas = new Dictionary<int, MissionRewardData>();

        foreach (var data in missionDatas)
        {
            dicMissionDatas.Add(data.id, data);
        }

        foreach (var reward in missionRewardDatas)
        {
            dicMissionRewardDatas.Add(reward.id, reward);
        }

       // Debug.LogFormat("Load completed! Missions: {0}, Rewards: {1}", dicMissionDatas.Count, dicMissionRewardDatas.Count);
    }

    private void InitializeMissionInfos()
    {
        missionInfos = new List<MissionInfo>();
        foreach (var data in missionDatas)
        {
            missionInfos.Add(new MissionInfo(data.id, 0, 0, 0));
        }
    }

    // �̼� ���� ���� ������Ʈ �޼���
    public void UpdateMissionProgress(int missionId, int progress)
    {
        var missionInfo = FindMissionInfoById(missionId);
        if (missionInfo != null)
        {
            missionInfo.count = progress;
            if (missionInfo.count >= dicMissionDatas[missionId].goal)
            {
                missionInfo.state = 1; // �̼� �Ϸ� ���·� ����
                missionNews.SetActive(true);

            }

            // UIListItem�� ã�Ƽ� ������Ʈ
            var listItem = FindUIListItemByMissionId(missionId);
            if (listItem != null)
            {
                listItem.UpdateProgress(progress);
            }
            else
            {
                Debug.Log("UIListItem not found for Mission " + missionId);
            }
        }
    }

  
    private MissionInfo FindMissionInfoById(int missionId)
    {
        return missionInfos.Find(m => m.id == missionId);
    }

    // UIListItem�� �̼� ID�� ã�� �޼���
    private UIListItem FindUIListItemByMissionId(int missionId)
    {
        return uiListItems.Find(item => item.info.id == missionId);
    }

    // UIListItem �߰� �޼���
    public void AddUIListItem(UIListItem listItem)
    {
        if (!uiListItems.Contains(listItem))
        {
            uiListItems.Add(listItem);
        }
    }


    


}
