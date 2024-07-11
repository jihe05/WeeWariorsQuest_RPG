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



    //타이핑 Text 코루틴
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

    //대화 시작 
    public void StartTalk(string[] _talks)
    {
        dialogues = _talks;
      
        //첫 대사 타이핑
        typingCoroutine = StartCoroutine(Typing(dialogues[talkNum]));
    }

    //다음 대사 출력
    public void NextTalk()
    {
        EndingText.text = null;
        talkNum++;

        if (talkNum == dialogues.Length)
        {
            EndTalk();
      
            return;
        }
       
        //다음 대사 타이핑
        typingCoroutine = StartCoroutine(Typing(dialogues[talkNum]));
    }


    //대화 종료 
    private void EndTalk()
    {
        this.gameObject.SetActive(false);

    }




}


