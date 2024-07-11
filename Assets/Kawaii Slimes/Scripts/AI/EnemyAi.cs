
using UnityEngine;
using UnityEngine.AI;
public enum SlimeAnimationState 
{
    Idle,
    Walk,
    Jump,
    Attack,
    Damage
}
public class EnemyAi : MonoBehaviour
{

    public Face faces; //얼굴 텍스를 관리하는 스크렙터블 오브젝트 스크립트 
    public GameObject SmileBody; //슬라임 오브젝트
    public SlimeAnimationState currentState;//상태 이너미
   
    public Animator animator;//애니메이터
    public NavMeshAgent agent;//네미메쉬 
    public Transform[] waypoints;//위치배열
    public int damType;//패해 유형

    private int m_CurrentWaypointIndex; //현재 위치 

    private bool move; //이동여부
    private Material faceMaterial;//얼굴 재질
    private Vector3 originPos;//시작 위치

    //이동 유형 Enme
    public enum WalkType { Patroll ,ToOrigin }

    private WalkType walkType; //현재 이동 유형

    void Start()
    {
        originPos = transform.position;//시작위치
        faceMaterial = SmileBody.GetComponent<Renderer>().materials[1];// 얼굴재질
        walkType = WalkType.Patroll;//초기 이동 유형
    }

    //걷는 상태로 설정하고 이동위치를 바꿔주는 함수
    public void WalkToNextDestination()
    {
        //현재 상태 설정
        currentState = SlimeAnimationState.Walk;
        //다음 경유지 덱스계산 (현재위치 +1 % 위치 배열의 길이 )
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        //에이전트로 다음 경유지 설정
        agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        //표정 설정
        SetFace(faces.WalkFace);
    }

    //메서드 호출을 취소 
    public void CancelGoNextDestination() =>CancelInvoke(nameof(WalkToNextDestination));

    //얼굴 텍스쳐 변경 
    void SetFace(Texture tex)
    {
        //메인 텍스쳐 
        faceMaterial.SetTexture("_MainTex", tex);
    }


 void Update()
{
    switch (currentState)
    {
        case SlimeAnimationState.Idle:
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return; // 이미 Idle 애니메이션이면 반환
            StopAgent(); // 에이전트 정지
            SetFace(faces.Idleface); // 얼굴 표정 설정
            break;

        case SlimeAnimationState.Walk:
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) return; // 이미 Walk 애니메이션이면 반환

            agent.isStopped = false; // 에이전트 정지 해제
            agent.updateRotation = true; // 에이전트가 회전하도록 설정

            if (walkType == WalkType.ToOrigin)
            {
                agent.SetDestination(originPos); // 시작 위치로 이동
                SetFace(faces.WalkFace); // 얼굴 표정 설정
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    walkType = WalkType.Patroll; // 이동 유형을 Patroll로 변경
                    transform.rotation = Quaternion.identity; // 회전 초기화
                    currentState = SlimeAnimationState.Idle; // 상태를 Idle로 변경
                }
            }
            else
            {
                if (waypoints[0] == null) return;
                agent.SetDestination(waypoints[m_CurrentWaypointIndex].position); // 다음 경유지로 이동
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    currentState = SlimeAnimationState.Idle; // 상태를 Idle로 변경
                    Invoke(nameof(WalkToNextDestination), 2f); // 2초 후에 다음 목적지로 이동
                }
            }
            animator.SetFloat("Speed", agent.velocity.magnitude); // 에이전트 속도를 애니메이터에 설정
            break;

        case SlimeAnimationState.Jump:
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) return; // 이미 Jump 애니메이션이면 반환
            StopAgent(); // 에이전트 정지
            SetFace(faces.jumpFace); // 얼굴 표정 설정
            animator.SetTrigger("Jump"); // Jump 트리거 설정
            break;

        case SlimeAnimationState.Attack:
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return; // 이미 Attack 애니메이션이면 반환
            StopAgent(); // 에이전트 정지
            SetFace(faces.attackFace); // 얼굴 표정 설정
            animator.SetTrigger("Attack"); // Attack 트리거 설정
            break;

        case SlimeAnimationState.Damage:
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Damage0") || 
                animator.GetCurrentAnimatorStateInfo(0).IsName("Damage1") || 
                animator.GetCurrentAnimatorStateInfo(0).IsName("Damage2")) return; // 이미 Damage 애니메이션이면 반환

            StopAgent(); // 에이전트 정지
            animator.SetTrigger("Damage"); // Damage 트리거 설정
            animator.SetInteger("DamageType", damType); // DamageType 설정
            SetFace(faces.damageFace); // 얼굴 표정 설정
            break;
    }
}



    private void StopAgent()
    {
        agent.isStopped = true; // 에이전트 정지
        animator.SetFloat("Speed", 0); // 속도를 0으로 설정
        agent.updateRotation = false; // 에이전트 회전 업데이트 비활성화
    }

    public void AlertObservers(string message)
    {
        if (message.Equals("AnimationDamageEnded"))
        {
            float distanceOrg = Vector3.Distance(transform.position, originPos);
            if (distanceOrg > 1f)
            {
                walkType = WalkType.ToOrigin; // 이동 유형을 ToOrigin으로 설정
                currentState = SlimeAnimationState.Walk; // 상태를 Walk로 변경
            }
            else
            {
                currentState = SlimeAnimationState.Idle; // 상태를 Idle로 변경
            }
        }

        if (message.Equals("AnimationAttackEnded"))
        {
            currentState = SlimeAnimationState.Idle; // 상태를 Idle로 변경
        }

        if (message.Equals("AnimationJumpEnded"))
        {
            currentState = SlimeAnimationState.Idle; // 상태를 Idle로 변경
        }
    }



    void OnAnimatorMove()
    {
        Vector3 position = animator.rootPosition; // 애니메이터의 루트 포지션 가져오기
        position.y = agent.nextPosition.y; // y 좌표는 에이전트의 다음 위치로 설정
        transform.position = position; // 슬라임 위치 설정
        agent.nextPosition = transform.position; // 에이전트의 다음 위치 설정
    }

}
