using UnityEngine;

[CreateAssetMenu(fileName = "MissionData", menuName = "ScriptableObjects/MissionData", order = 1)]
public class MissionData : ScriptableObject
{
  
    public int id; // 미션 ID
    public string mission_desc; // 미션 설명
    public int goal; // 미션 목표
    public int reward_id; // 보상 ID
    public int reward_amount; // 보상 수량
    public int btn_state; // 버튼 상태
    public string sprite_name; // 스프라이트 이름
    
}



