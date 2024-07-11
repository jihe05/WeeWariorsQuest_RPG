using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public Move move;

    public GameObject player;

    public Text PlayerName;

    public Monstermove monstermove;
    Bossmove Bossmove;

    public float Player_Hp = 10000;
    public float Player_Ap = 20;

    public int Player_Level = 1;
         

    private void Awake()
    {
        instance = this;
    
    }

    private void Start()
    {
      
    }

    public void PlayerUpdateHp(float Ap)
    {
        Player_Hp -= Ap;
        PlayerPrefs.SetFloat("Player_Hp", Player_Hp);
        UImanger.Instance.PlayerSliderbar(Ap);
    }

    public void PlayerMonsterTrgger()
    {
        monstermove.MonsterUpdateHp(Player_Ap);
    }

    public void PlayerBossTrgger()
    {
        Bossmove = FindObjectOfType<Bossmove>();
        Bossmove.BossUpdateHp(Player_Ap);
    }

    public void PlayerLevelUpUpdate()
    {
        Player_Level++;
        UImanger.Instance.PlayerlevelUp(Player_Level);
        move.playerLevelUp();
    }

    
}
