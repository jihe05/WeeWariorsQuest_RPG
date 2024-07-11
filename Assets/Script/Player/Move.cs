using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public float gravity = -23f;
    private int count =0;
   
    private float verticalVelocity;//수직속도

    public CharacterController characterController;

    public Animator animator_Player;

    Monstermove monstermove;

    public Camera camera;    

    private IPlayerState currentState;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        characterController = GetComponent<CharacterController>();
        monstermove = GetComponent<Monstermove>();
    }

    private void Start()
    {
        ChangeState(new IdleState(this));
    }

    private void Update()
    {

        currentState?.ExtcuteOnUpdate();

        // 마우스 클릭 시 공격 상태로 전환
        if (Input.GetMouseButtonDown(0) && !(currentState is AtKState))
        {
            ChangeState(new AtKState(this));
            PlayerAttackCount();
        }

    }

    private void PlayerAttackCount()
    {
        if (count <= 2)
            count++;
        else
            count = 0;
        
        animator_Player.SetInteger("AttackCount", count);

    }

    public void ChangeState(IPlayerState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();

    }



    public void PlayerMove(Vector3 direction)
    {
       
        Vector3 move = direction * moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            move *= 2;

        }
            if (characterController.isGrounded)
            {
                if (verticalVelocity < 0)
                {
                    verticalVelocity = 0f;
                }
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    verticalVelocity = jumpForce;

                    animator_Player.SetBool("Jump", true);

                }
                animator_Player.SetBool("Jump", false);
            }
            else
            {
            
                verticalVelocity += gravity * Time.deltaTime;
            }
        

        move.y = verticalVelocity;
        characterController.Move(move * Time.deltaTime);
    }

    public void OnAnimatorPlayer(GameObject player)
    {
        animator_Player = player.GetComponent<Animator>();

    }


    public void playerLevelUp()
    {
        Debug.Log("playerLevelUp");
        ChangeState(new LevelUP(this));

        Invoke("LevelUpEnd", 2f);

    }

    public void LevelUpEnd()
    {
        Debug.Log("LevelUpEnd");
        gravity = -23;
        ChangeState(new IdleState(this));

    }

    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetMouseButtonDown(0))
        {
           
            if (collision.collider.CompareTag("Monster"))
            {
                PlayerManager.instance.PlayerMonsterTrgger();
            }
            if (collision.collider.CompareTag("Boss"))
            {
                PlayerManager.instance.PlayerBossTrgger();
            }
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("BosEvent"))
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log(transform.position);
            characterController.enabled = false;
            SceneManager.LoadScene("Bosmap");
        }
        if (Input.GetMouseButtonDown(0))
        {
            //보스를 때렸을때
            if (other.CompareTag("Boss"))
            {
                PlayerManager.instance.PlayerBossTrgger();
            }
        }
        if(other.CompareTag("Guide"))
        {
            EventManager.Instans.TalkePanelActive();
            EventManager.Instans.ColiderFalse();
        }
        if (other.CompareTag("King"))
        {
            EventManager.Instans.TalkePanelDestroy();
        }
        if (other.CompareTag("BosAttack"))
        {
            Bossmove.Instance.PlayerAttack();
        }
       
        if (other.gameObject.CompareTag("EnventBox"))
        {
          
            UImanger.Instance.EventButton();
        }



    }

    // 씬이 로드된 후 호출
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Bosmap")
        {
            // 여기서 씬이 완전히 로드된 후 오브젝트의 위치와 회전을 설정
            Invoke("SetPlayerPositionAndRotation", 0.01f);
            SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 핸들러 제거
        }
    }

    private void SetPlayerPositionAndRotation()
    {
        transform.position = new Vector3(38f, -12f, 144.5f);
        transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = Color.black;
        characterController.enabled = true;


    }

}
