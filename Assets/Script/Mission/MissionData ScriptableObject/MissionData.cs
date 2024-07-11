using UnityEngine;

[CreateAssetMenu(fileName = "MissionData", menuName = "ScriptableObjects/MissionData", order = 1)]
public class MissionData : ScriptableObject
{
  
    public int id; // �̼� ID
    public string mission_desc; // �̼� ����
    public int goal; // �̼� ��ǥ
    public int reward_id; // ���� ID
    public int reward_amount; // ���� ����
    public int btn_state; // ��ư ����
    public string sprite_name; // ��������Ʈ �̸�
    
}



