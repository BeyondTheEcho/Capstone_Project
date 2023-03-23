using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject s_tutorialText;

    void Start()
    {
        s_tutorialText.SetActive(false);
    }

    void OnTriggerEnter2D()
    {
        s_tutorialText.SetActive(true);
    }
    void OnTriggerExit2D()
    {
        s_tutorialText.SetActive(false);
    }

}
