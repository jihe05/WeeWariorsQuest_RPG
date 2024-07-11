using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance => instance;

    public MissionData[] missionDatas; // 미션 데이터 배열
    public MissionRewardData[] missionRewardDatas; // 미션 보상 데이터 배열

    public Dictionary<int, MissionData> dicMissionDatas; // 미션 데이터 딕셔너리
    public Dictionary<int, MissionRewardData> dicMissionRewardDatas; // 미션 보상 데이터 딕셔너리
    public List<MissionInfo> missionInfos; // 미션 진행 상태 목록
    public List<UIListItem> uiListItems; // UI 리스트 아이템 목록

    public GameObject missionNews;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDictionaries();
            InitializeMissionInfos();
            uiListItems = new List<UIListItem>(); // 리스트 초기화
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
        UpdateMissionProgress(missionId, missionData.goal); // 미션 완료
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

    // 미션 진행 상태 업데이트 메서드
    public void UpdateMissionProgress(int missionId, int progress)
    {
        var missionInfo = FindMissionInfoById(missionId);
        if (missionInfo != null)
        {
            missionInfo.count = progress;
            if (missionInfo.count >= dicMissionDatas[missionId].goal)
            {
                missionInfo.state = 1; // 미션 완료 상태로 변경
                missionNews.SetActive(true);

            }

            // UIListItem을 찾아서 업데이트
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

    // UIListItem을 미션 ID로 찾는 메서드
    private UIListItem FindUIListItemByMissionId(int missionId)
    {
        return uiListItems.Find(item => item.info.id == missionId);
    }

    // UIListItem 추가 메서드
    public void AddUIListItem(UIListItem listItem)
    {
        if (!uiListItems.Contains(listItem))
        {
            uiListItems.Add(listItem);
        }
    }


    


}
