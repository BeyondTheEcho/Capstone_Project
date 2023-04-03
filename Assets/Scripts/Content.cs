using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;

public class Content : MonoBehaviour
{
    public TextMeshProUGUI m_TextComponent; //reference tmpro to text component
    const int TALKERNUM = 2;
    const int LINESNUM = 9;
    private string[] m_Lines = new string[LINESNUM];
    public float m_TextSpeed;

    public static bool m_ConversationOver = false;
    private int m_Index;

    public GameObject[] m_StoryTeller;

    public Animator[] m_StoryTellerMouth;

    private int m_TalkerIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        LanguageSettings.ChangeFont(LanguageSettings.m_FinalFont);
        m_ConversationOver = false;
        m_TextComponent.text = string.Empty;
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
        m_Lines[0] = LanguageSettings.s_Instance.GetLocalizedString("Hi");
        m_Lines[1] = LanguageSettings.s_Instance.GetLocalizedString("BRIEF");
        m_Lines[2] = LanguageSettings.s_Instance.GetLocalizedString("BRIEF2");
        m_Lines[3] = LanguageSettings.s_Instance.GetLocalizedString("IntroduceOther");
        m_Lines[4] = LanguageSettings.s_Instance.GetLocalizedString("OTHER");
        m_Lines[5] = LanguageSettings.s_Instance.GetLocalizedString("OTHER2");
        m_Lines[6] = LanguageSettings.s_Instance.GetLocalizedString("OTHER3");
        m_Lines[7] = LanguageSettings.s_Instance.GetLocalizedString("GoodLuck");
        m_Lines[8] = LanguageSettings.s_Instance.GetLocalizedString("GoGoGo");
    }
    void StartDialogue()
    {
        m_Index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in m_Lines[m_Index].ToCharArray())
        {
            m_TextComponent.text += c;
            yield return new WaitForSeconds(m_TextSpeed);
        }
    }

    void NextLine()
    {
        if (m_Index < m_Lines.Length - 1)
        {
            m_Index++;
            m_TextComponent.text = string.Empty;
            StartCoroutine(TypeLine());

        }
        else
        {
            m_ConversationOver = true;
        }
    }

    public void SentenceBehaviour()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Add1"))
        {

            if (m_TextComponent.text == m_Lines[m_Index]) //means this sentense has been run completely
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                m_TextComponent.text = m_Lines[m_Index]; //instantly fill out the words in the sentense
            }
        }

        //These are to check which one is the talker
        if (m_Index == 4) //introduce the manager
        {
            m_TalkerIndex = 1;
        }

        if (m_Index == 7) //introduce the manager
        {
            m_TalkerIndex = 0;
        }

        if (m_Index == 8) //introduce the manager
        {
            m_TalkerIndex = 1;
        }

        if (m_ConversationOver || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause1"))
        {
            SceneManager.LoadScene("Lobby");
        }
        //The talker starts talking
        StartTalker(m_TalkerIndex);

        //Make the character in the scene who is not talking stop the mouth talking animation
        for (int i = 0; i < TALKERNUM; i++)
        {
            if (i != m_TalkerIndex || m_TextComponent.text == m_Lines[m_Index])
            { //if you are not the talker or the lines are finished
                StopTalker(i);
            }
        }
    }



    //These 4 functions below are for talkers' and their mouths' animation
    public void StartTalker(int m_talkerIndex)
    {
        m_StoryTeller[m_talkerIndex].SetActive(true);

        StartTalking(m_StoryTellerMouth[m_talkerIndex]);
    }

    public void StopTalker(int m_talkerIndex)
    {
        if (m_StoryTellerMouth[m_talkerIndex].isActiveAndEnabled)
        {
            StopTalking(m_StoryTellerMouth[m_talkerIndex]);
        }

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

