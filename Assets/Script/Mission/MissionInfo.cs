using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �̼� ������ �ʱ�ȭ�ϰ� �����ϴ� Ŭ����
public class MissionInfo
{
    public int id; // �̼� ID
    public int count; // �̼� ���൵
    public int star; // ���� �� ����
    public int state; // �̼� ����

    // ������ : �̼� ������ �ʱ�ȭ
    public MissionInfo(int id, int count, int star, int state)
    {
        this.id = id;
        this.count = count;
        this.star = star;
        this.state = state;
    }
}
