using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Content : MonoBehaviour
{
    public TextMeshProUGUI textComponent; //reference tmpro to text component
    public string[] lines;
    public float textSpeed;

    public GameObject quitButton;
    public static bool conversationOver = false;
    private int index;
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
            gameObject.SetActive(false);
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

        if (conversationOver)
        {
            quitButton.SetActive(true);
        }
    }
}

