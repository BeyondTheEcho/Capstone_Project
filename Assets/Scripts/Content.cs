using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Content : MonoBehaviour
{
    public TextMeshProUGUI textComponent; //reference tmpro to text component
    const int TALKERNUM = 2;
    const int LINESNUM = 9;
    private string[] lines=new string[LINESNUM];
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
        LinesContent();
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        SentenceBehaviour();
    }

    void LinesContent()
    {
        lines[0] = LanguageSettings.s_Instance.GetLocalizedString("PICK");
        lines[1] = LanguageSettings.s_Instance.GetLocalizedString("BRIEF");
        lines[2] = LanguageSettings.s_Instance.GetLocalizedString("BRIEF2");
        lines[3] = LanguageSettings.s_Instance.GetLocalizedString("IntroduceOther");
        lines[4] = LanguageSettings.s_Instance.GetLocalizedString("OTHER");
        lines[5] = LanguageSettings.s_Instance.GetLocalizedString("PICK");
        lines[6] = LanguageSettings.s_Instance.GetLocalizedString("BRIEF");
        lines[7] = LanguageSettings.s_Instance.GetLocalizedString("BRIEF2");
        lines[8] = LanguageSettings.s_Instance.GetLocalizedString("IntroduceOther");
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

        //These are to check which one is the talker
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


        //The talker starts talking
        StartTalker(m_talkerIndex);

        //Make the character in the scene who is not talking stop the mouth talking animation
        for (int i = 0; i < TALKERNUM; i++)
        {
            if (i != m_talkerIndex || textComponent.text == lines[index]) 
            { //if you are not the talker or the lines are finished
                StopTalker(i); 
            }
        }
    }



    //These 4 functions below are for talkers' and their mouths' animation
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

