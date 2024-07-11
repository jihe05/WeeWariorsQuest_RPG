using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollviewTest : MonoBehaviour
{
    public GameObject listItemPrefab;
    public Transform contents;

    // Start 메서드 : 초기 데이터 로드 및 리스트 아이템 생성
    private void Start()
    {
        var dataManager = DataManager.Instance;

        foreach (var pair in dataManager.dicMissionDatas)
        {
            var go = Instantiate(this.listItemPrefab, contents);
            var listItem = go.GetComponent<UIListItem>();
            var data = pair.Value;
            var info = new MissionInfo(data.id, 0, 0, 0);
            listItem.Init(info);
           // Debug.Log("UIListItem created for Mission ID: " + data.id);
        }


    }

    // Update 메서드 : 필요시 업데이트 로직 추가
    void Update()
    {

    }
}
