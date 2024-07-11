using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
  
    public Text EndingText;
 
    public string[] dialogues;

    public int talkNum;
  
    private Coroutine typingCoroutine;

    private void OnEnable()
    {
        StartTalk(dialogues);
    }



    //Ÿ���� Text �ڷ�ƾ
    IEnumerator Typing(string talk)
    {
        EndingText.text = null;
        for (int i = 0; i < talk.Length; i++)
        {
            EndingText.text += talk[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1.0f);
        NextTalk();
    }

    //��ȭ ���� 
    public void StartTalk(string[] _talks)
    {
        dialogues = _talks;
      
        //ù ��� Ÿ����
        typingCoroutine = StartCoroutine(Typing(dialogues[talkNum]));
    }

    //���� ��� ���
    public void NextTalk()
    {
        EndingText.text = null;
        talkNum++;

        if (talkNum == dialogues.Length)
        {
            EndTalk();
      
            return;
        }
       
        //���� ��� Ÿ����
        typingCoroutine = StartCoroutine(Typing(dialogues[talkNum]));
    }


    //��ȭ ���� 
    private void EndTalk()
    {
        this.gameObject.SetActive(false);

    }




}


