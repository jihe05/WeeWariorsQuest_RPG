using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 미션 정보를 초기화하고 저장하는 클래스
public class MissionInfo
{
    public int id; // 미션 ID
    public int count; // 미션 진행도
    public int star; // 얻은 별 개수
    public int state; // 미션 상태

    // 생성자 : 미션 정보를 초기화
    public MissionInfo(int id, int count, int star, int state)
    {
        this.id = id;
        this.count = count;
        this.star = star;
        this.state = state;
    }
}
