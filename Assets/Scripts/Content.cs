using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Content : MonoBehaviour
{
    public TextMeshProUGUI textComponent; //reference tmpro to text component
    public string[] lines;
    public float textSpeed;

    public static bool conversationOver = false;
    private int index;

    public GameObject[] m_storyTeller;

    public Animator[] m_storyTellerMouth;

    private int m_talkerIndex=0;

    // Start is called before the first frame update
    void Start()
    {
        conversationOver = false;
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        SentenceBehaviour();
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());

        }
        else
        {
            conversationOver = true;
        }
    }

    public void SentenceBehaviour()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (textComponent.text == lines[index]) //means this sentense has been run completely
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index]; //instantly fill out the words in the sentense
            }
        }

        if (index == 4) //introduce the manager
        {
            m_talkerIndex = 1;
        }

        if (index == 7) //introduce the manager
        {
            m_talkerIndex = 0;
        }

        if (index == 8) //introduce the manager
        {
            m_talkerIndex = 1;
        }

        StartTalker(m_talkerIndex);
        for (int i = 0; i < 2; i++)
        {
            if (i != m_talkerIndex || textComponent.text == lines[index]) 
            { //if you are not the talker or the lines are finished
                StopTalker(i); 
            }
        }
    }



    public void StartTalker(int m_talkerIndex)
    {
        StartTalking(m_storyTellerMouth[m_talkerIndex]);
        m_storyTeller[m_talkerIndex].SetActive(true);
    }

    public void StopTalker(int m_talkerIndex)
    {
        StopTalking(m_storyTellerMouth[m_talkerIndex]);

    }

    public void StartTalking(Animator animator)
    {
        animator.SetBool("stillTalking", true);
    }
    public void StopTalking(Animator animator)
    {
        animator.SetBool("stillTalking", false);
    }
}

