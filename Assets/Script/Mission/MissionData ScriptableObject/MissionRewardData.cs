using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionRewardData", menuName = "ScriptableObjects/MissionRewardData", order = 2)]
public class MissionRewardData : ScriptableObject
{
    public int id; // 보상 ID
    public string reward_name; // 보상 이름
}
