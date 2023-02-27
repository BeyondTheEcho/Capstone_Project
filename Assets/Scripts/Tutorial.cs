using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject t_tutorialText;

    void Start()
    {
        t_tutorialText.SetActive(false);
    }

    void OnTriggerEnter2D()
    {
        t_tutorialText.SetActive(true);
    }
    void OnTriggerExit2D()
    {
        t_tutorialText.SetActive(false);
    }

}
