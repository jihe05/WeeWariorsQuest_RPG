using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerState
{
    void EnterState();
    void ExitState();
    void ExtcuteOnUpdate();

   
}


//�÷��̾��� ��� ���� 
public class IdleState : IPlayerState
{

    private readonly Move _playerMove;

    //������ : �÷��̾� �ν��Ͻ� �ʱ�ȭ 
    public IdleState(Move playerMove)
    {
        this._playerMove = playerMove;
    }

    public void EnterState()
    {
        _playerMove.animator_Player.SetBool("isMove" , false);
        _playerMove.animator_Player.SetFloat("MoveX", 0);
        _playerMove.animator_Player.SetFloat("MoveY", 0);
    }


    public void ExtcuteOnUpdate()
    {

        // _playerMove.handleJump();
        if (_playerMove == null)
        { return; }
        else
        _playerMove.PlayerMove(Vector3.zero);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            _playerMove.ChangeState(new WalkState(_playerMove));
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerMove.ChangeState(new JumpState(_playerMove));
        }
     
    }
                

    public void ExitState()
    {
        

    }

}

//�ȱ�
public class WalkState : IPlayerState
{

    private readonly Move _playerMove;
    public WalkState(Move playerMove)
    {
        this._playerMove = playerMove;
    }
    
      
    public void EnterState()
    {
        _playerMove.animator_Player.SetBool("isMove", true);
    
        
    }

    public void ExtcuteOnUpdate()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        { 
            _playerMove.animator_Player.SetFloat("MoveX", 0);
            _playerMove.animator_Player.SetFloat("MoveY", 3);
            direction += _playerMove.transform.forward;
           
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _playerMove.animator_Player.SetFloat("MoveX", 0);
            _playerMove.animator_Player.SetFloat("MoveY", -3);
            direction +=  -_playerMove.transform.forward;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _playerMove.animator_Player.SetFloat("MoveX", -3);
            _playerMove.animator_Player.SetFloat("MoveY", 0);
            direction += -_playerMove.transform.right;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _playerMove.animator_Player.SetFloat("MoveX", 3);
            _playerMove.animator_Player.SetFloat("MoveY", 0);
            direction += _playerMove.transform.right;
        }
        else
        {
           _playerMove.ChangeState(new IdleState(_playerMove));
        }

    
        // �̵� ���� ȣ��
        _playerMove.PlayerMove(direction);


    }


    public void ExitState()
    {
     
    }



}


//���� 
public class JumpState : IPlayerState
{

    private readonly Move _playerMove;
    

    public JumpState(Move playerMove)
    {
        this._playerMove = playerMove;
    }

    public void EnterState()
    {
        _playerMove.animator_Player.SetBool("Jump", true);
        
    }

    public void ExtcuteOnUpdate()
    {
        Vector3 direction = Vector3.zero;

        // ���� �� �̵� ó��
        _playerMove.PlayerMove(direction);

        if (!_playerMove.characterController.isGrounded)
        {
            return;
        }
        else
        {
            _playerMove.ChangeState(new IdleState(_playerMove));
        }

    }
    public void ExitState()
    {
        _playerMove.animator_Player.SetBool("Jump", false);

    }
}


//���� ���� 
public class AtKState : IPlayerState
{

    private readonly Move _playerMove;



    public AtKState(Move playerMove)
    {
        this._playerMove = playerMove;
    }
    public void EnterState()
    {
        _playerMove.animator_Player.SetTrigger("Attack");
    }


    public void ExtcuteOnUpdate()
    {
        _playerMove.PlayerMove(Vector3.zero);

        if (!Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                _playerMove.ChangeState(new WalkState(_playerMove));
            }
            else
            {
                _playerMove.ChangeState(new IdleState(_playerMove));

            }

        }



    }

    public void ExitState()
    {

    }




}


public class LevelUP : IPlayerState
{

    private readonly Move _playerMove;



    public LevelUP(Move playerMove)
    {
        this._playerMove = playerMove;
    }
    public void EnterState()
    {
        _playerMove.animator_Player.Play("LevelUp");
    }


    public void ExtcuteOnUpdate()
    {
        Debug.Log("�߷� ����");
        _playerMove.PlayerMove(Vector3.zero);
        _playerMove.gravity = 1;

        if (_playerMove.characterController.isGrounded)
        {
            _playerMove.ChangeState(new IdleState(_playerMove));
        }

    }

    public void ExitState()
    {
        Debug.Log("��");
      
      
    }




}
