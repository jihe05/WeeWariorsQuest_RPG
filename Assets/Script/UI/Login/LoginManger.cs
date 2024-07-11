using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManger : MonoBehaviour
{
    [Header("Login")]
    public InputField Login_idInput;
    public InputField Login_numberInput;

    [Header("Sing_up")]
    public InputField Singup_idInput;
    public InputField Singup_numberInput;

    [Header("Button")]
    public GameObject LoginPanel;
    public GameObject SingupPanel;
    public GameObject MainPanel;

    public Text Login_errorText;
    public Text Singup_errorText;

    private Dictionary<string, string> Dic_userData = new Dictionary<string, string>();

    public void SignUp()
    {

        string id = Singup_idInput.text;
        string number = Singup_numberInput.text;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(number))
        {
            Singup_errorText.text = "���̵�� ��й�ȣ�� �Է����ּ���.";
            return;
        }
        if (Dic_userData.ContainsKey(id))
        {
            Singup_errorText.text = "�̹� �����ϴ� ���̵��Դϴ�.";
            return;
        }

        Dic_userData.Add(id, number);
        Singup_errorText.text = "ȭ�������� �Ϸ�Ǿ����ϴ�.";
      
    }

    public void Login()
    {
      
        string id = Login_idInput.text;
        string number = Login_numberInput.text;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(number))
        {
            Login_errorText.text = "���̵�� ��й�ȣ�� �Է����ּ���.";
            return;
        }

        if (Dic_userData.TryGetValue(id, out string storesNumber))
        {
            if (number == storesNumber)
            {
                Login_errorText.text = "�α��� ����";
                PlayerPrefs.SetString("Playerid", id);
                PlayerPrefs.SetString("PlayerNumber", storesNumber);
                PlayerManager.instance.PlayerName.text = id;
                Invoke("GameStart", 0.5f);
               
            }
            else
            {

                Login_errorText.text = "�߸��� ��ȣ�Դϴ�";
            }

        }
        else
        {

            Login_errorText.text = "�������� �ʴ� ���̵��Դϴ�.";
        }
    }


    public void ShowLoginButtonClike()
    {
        Login();
    }

    public void ShowSignupButtonClike()
    {
        SignUp();
    }

    public void SignupTextButton()
    {
       SingupPanel.gameObject.SetActive(true);
       LoginPanel.gameObject.SetActive(false);
        
    }

    public void SigninTextButton()
    {
        SingupPanel.gameObject.SetActive(false);
        LoginPanel.gameObject.SetActive(true);
    }

    public void GameStart()
    {
        MainPanel.gameObject.SetActive(false);

    }

}
