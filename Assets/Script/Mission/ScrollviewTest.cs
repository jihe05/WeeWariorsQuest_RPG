using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollviewTest : MonoBehaviour
{
    public GameObject listItemPrefab;
    public Transform contents;

    // Start �޼��� : �ʱ� ������ �ε� �� ����Ʈ ������ ����
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

    // Update �޼��� : �ʿ�� ������Ʈ ���� �߰�
    void Update()
    {

    }
}
