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

        // HP �� ���� �� �ʱ�ȭ
        UImanger.Instance.MonsterHpData(gameObject);
        HpSlider = GetComponentInChildren<Slider>();
    }

    IEnumerator StateMachine()
    {
        while (MonsterHP > 0)
        {
            yield return StartCoroutine(state.ToString());
           
        }

        // HP�� 0 ������ �� KILLED ���·� ��ȯ
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
                ChangeState(State.ATTACK); // ��ǥ�� �����ϸ� ���� ���·� ����
                yield break; // CHASE ���� ����
            }
            else if (nmAgent.remainingDistance < lostDistance)
            {
              
                Target = null;
                nmAgent.SetDestination(transform.position);
                ChangeState(State.IDLE); // ��ǥ�� �Ҿ������ ��� ���·� ����
                yield break; // CHASE ���� ����
            }

            yield return null; // ���� �����ӱ��� ���
        }
    }

    IEnumerator ATTACK()
    {
      
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (!curAnimStateInfo.IsName("Attack"))
        {
            anim.Play("Attack", 0, 0); // ���� �ִϸ��̼� ����
            PlayerManager.instance.PlayerUpdateHp(MonsterAp);

        }

        // ���� �ִϸ��̼��� ���� ������ ���
        while (curAnimStateInfo.normalizedTime < 1.0f)
        {
            yield return null;
            curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        }

        // ���� �ִϸ��̼��� ���� �Ŀ��� ���¸� ����
        yield return new WaitForSeconds(0.5f); // ���� ���� ����

        // �Ÿ��� �־����� ���� ���·� ����
        if (nmAgent.remainingDistance > nmAgent.stoppingDistance)
        {
           
            ChangeState(State.CHASE);
        }
        else
        {
            // �ٽ� ���� ���·� ���ư���
            ChangeState(State.ATTACK);
        }
    }

    IEnumerator KILLED()
    {
       
        var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (!curAnimStateInfo.IsName("Die"))
        {
            anim.Play("Die", 0, 0); // ���� �ִϸ��̼� ����
            Particle.SetActive(true);
        }

        // ���� �ִϸ��̼��� ���� ������ ���
        while (curAnimStateInfo.normalizedTime < 1.0f)
        {
            yield return null;
            curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);
          
        }
        
        yield return new WaitForSeconds(1.0f); 
        Destroy(gameObject); // ���� ����
         UImanger.Instance.CoinAndImage(500);
        DataManager.Instance.CompleteMission(6);
        Destroy(transform.gameObject);//Hp�� ����

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

        // ��ǥ�� �ٶ󺸵��� ȸ��
        Vector3 direction = (Target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        // ��ǥ�� ��� ����
        nmAgent.SetDestination(Target.position);

        if (MonsterHP == 0 && state != State.KILLED)
        {
            ChangeState(State.KILLED);
        }
        
    }

    //������ ������ ȣ�� 
    public void MonsterUpdateHp(float Ap)
    {
        MonsterHP -= Ap;
        if (HpSlider != null)
        {
            UImanger.Instance.MonsterSliderbar(HpSlider, Ap); // HP �� ������Ʈ
        }
    }

}



