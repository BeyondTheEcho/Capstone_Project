using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Content : MonoBehaviour
{
    public TextMeshProUGUI m_textComponent; //reference tmpro to text component
    const int TALKERNUM = 2;
    const int LINESNUM = 9;
    private string[] m_lines=new string[LINESNUM];
    public float m_textSpeed;

    public static bool m_conversationOver = false;
    private int m_index;

    public GameObject[] m_storyTeller;

    public Animator[] m_storyTellerMouth;

    private int m_talkerIndex=0;

    // Start is called before the first frame update
    void Start()
    {
        LanguageSettings.ChangeFont(LanguageSettings.m_FinalFont);
        m_conversationOver = false;
        m_textComponent.text = string.Empty;
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
        m_lines[0] = LanguageSettings.s_Instance.GetLocalizedString("Hi");
        m_lines[1] = LanguageSettings.s_Instance.GetLocalizedString("BRIEF");
        m_lines[2] = LanguageSettings.s_Instance.GetLocalizedString("BRIEF2");
        m_lines[3] = LanguageSettings.s_Instance.GetLocalizedString("IntroduceOther");
        m_lines[4] = LanguageSettings.s_Instance.GetLocalizedString("OTHER");
        m_lines[5] = LanguageSettings.s_Instance.GetLocalizedString("OTHER2");
        m_lines[6] = LanguageSettings.s_Instance.GetLocalizedString("OTHER3");
        m_lines[7] = LanguageSettings.s_Instance.GetLocalizedString("GoodLuck");
        m_lines[8] = LanguageSettings.s_Instance.GetLocalizedString("GoGoGo");
    }
    void StartDialogue()
    {
        m_index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in m_lines[m_index].ToCharArray())
        {
            m_textComponent.text += c;
            yield return new WaitForSeconds(m_textSpeed);
        }
    }

    void NextLine()
    {
        if (m_index < m_lines.Length - 1)
        {
            m_index++;
            m_textComponent.text = string.Empty;
            StartCoroutine(TypeLine());

        }
        else
        {
            m_conversationOver = true;
        }
    }

    public void SentenceBehaviour()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Add1"))
        {

            if (m_textComponent.text == m_lines[m_index]) //means this sentense has been run completely
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                m_textComponent.text = m_lines[m_index]; //instantly fill out the words in the sentense
            }
        }

        //These are to check which one is the talker
        if (m_index == 4) //introduce the manager
        {
            m_talkerIndex = 1;
        }

        if (m_index == 7) //introduce the manager
        {
            m_talkerIndex = 0;
        }

        if (m_index == 8) //introduce the manager
        {
            m_talkerIndex = 1;
        }

        if (m_conversationOver || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause1"))
        {
            SceneManager.LoadScene("Lobby");
        }
        //The talker starts talking
        StartTalker(m_talkerIndex);

        //Make the character in the scene who is not talking stop the mouth talking animation
        for (int i = 0; i < TALKERNUM; i++)
        {
            if (i != m_talkerIndex || m_textComponent.text == m_lines[m_index]) 
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

