using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionRewardData", menuName = "ScriptableObjects/MissionRewardData", order = 2)]
public class MissionRewardData : ScriptableObject
{
    public int id; // ���� ID
    public string reward_name; // ���� �̸�
}
