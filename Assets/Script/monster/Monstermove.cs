using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



enum State
{
    IDLE,
    CHASE,
    ATTACK,
    KILLED
}
public class Monstermove : MonoBehaviour
{
    public Transform Target;
    NavMeshAgent nmAgent;
    Animator anim;
    public GameObject Particle;
    private Slider HpSlider;
   
    public float MonsterHP = 100;
    public float MonsterAp = 100;
    public float lostDistance = 0;

   
    State state;

    private void Awake()
    {
        nmAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        state = State.IDLE;
        StartCoroutine(StateMachine());

        // HP 바 생성 및 초기화
        UImanger.Instance.MonsterHpData(gameObject);
        HpSlider = GetComponentInChildren<Slider>();
    }

    IEnumerator StateMachine()
    {
        while (MonsterHP > 0)
        {
            yield return StartCoroutine(state.ToString());
           
        }

        // HP가 0 이하일 때 KILLED 상태로 전환
        if (MonsterHP <= 0)
        {
            ChangeState(State.KILLED);
            yield return StartCoroutine(state.ToString());
        }
    }

    IEnumerator IDLE()
    {
       
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (!curAnimStateInfo.IsName("Idle"))
            anim.Play("Idle", 0, 0);

        int dir = Random.Range(0f, 1f) > 0.5f ? 1 : -1;
        float lookSpeed = Random.Range(25f, 40f);

        for (float i = 0; i < curAnimStateInfo.length; i += Time.deltaTime)
        {
            transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y + (dir) * Time.deltaTime * lookSpeed, 0f);
            yield return null;
        }
    }

    IEnumerator CHASE()
    {
       
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (!curAnimStateInfo.IsName("Walk"))
        {
           
            anim.Play("Walk", 0, 0);
            yield return null;
        }

        while (state == State.CHASE)
        {
            nmAgent.SetDestination(Target.position);

            if (nmAgent.remainingDistance <= nmAgent.stoppingDistance)
            {
                ChangeState(State.ATTACK); // 목표에 도달하면 공격 상태로 변경
                yield break; // CHASE 상태 종료
            }
            else if (nmAgent.remainingDistance < lostDistance)
            {
              
                Target = null;
                nmAgent.SetDestination(transform.position);
                ChangeState(State.IDLE); // 목표를 잃어버리면 대기 상태로 변경
                yield break; // CHASE 상태 종료
            }

            yield return null; // 다음 프레임까지 대기
        }
    }

    IEnumerator ATTACK()
    {
      
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (!curAnimStateInfo.IsName("Attack"))
        {
            anim.Play("Attack", 0, 0); // 공격 애니메이션 실행
            PlayerManager.instance.PlayerUpdateHp(MonsterAp);

        }

        // 공격 애니메이션이 끝날 때까지 대기
        while (curAnimStateInfo.normalizedTime < 1.0f)
        {
            yield return null;
            curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        }

        // 공격 애니메이션이 끝난 후에만 상태를 변경
        yield return new WaitForSeconds(0.5f); // 공격 간격 조절

        // 거리가 멀어지면 추적 상태로 변경
        if (nmAgent.remainingDistance > nmAgent.stoppingDistance)
        {
           
            ChangeState(State.CHASE);
        }
        else
        {
            // 다시 공격 상태로 돌아가기
            ChangeState(State.ATTACK);
        }
    }

    IEnumerator KILLED()
    {
       
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (!curAnimStateInfo.IsName("Die"))
        {
            anim.Play("Die", 0, 0); // 죽음 애니메이션 실행
            Particle.SetActive(true);
        }

        // 죽음 애니메이션이 끝날 때까지 대기
        while (curAnimStateInfo.normalizedTime < 1.0f)
        {
            yield return null;
            curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);
          
        }
        
        yield return new WaitForSeconds(1.0f); 
        Destroy(gameObject); // 몬스터 삭제
         UImanger.Instance.CoinAndImage(500);
        DataManager.Instance.CompleteMission(6);
        Destroy(transform.gameObject);//Hp바 삭제

    }


    void ChangeState(State newState)
    {
        state = newState;
    }

    public void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        else
        {
            Target = other.transform;
            nmAgent.SetDestination(Target.position);
            ChangeState(State.CHASE);
        }
    }

    private void Update()
    {
        if (Target == null)
            return;

        // 목표를 바라보도록 회전
        Vector3 direction = (Target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        // 목표를 계속 추적
        nmAgent.SetDestination(Target.position);

        if (MonsterHP == 0 && state != State.KILLED)
        {
            ChangeState(State.KILLED);
        }
        
    }

    //데미지 받을때 호출 
    public void MonsterUpdateHp(float Ap)
    {
        MonsterHP -= Ap;
        if (HpSlider != null)
        {
            UImanger.Instance.MonsterSliderbar(HpSlider, Ap); // HP 바 업데이트
        }
    }

}



