using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    private GameObject contents;
    private bool showSkillBook;

    private void Start()
    {
        contents = GameObject.Find("Contents");
        showSkillBook = false;
        contents.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("input k");
            showSkillBook = !showSkillBook;

            if (showSkillBook)
            {
                OpenSkillBook();
            }
            else
            {
                CloseSkillBook();
            }
        }
    }

    public void OpenSkillBook()
    {
        Debug.Log("Open");
        showSkillBook = true;
        contents.SetActive(true);
    }

    public void CloseSkillBook()
    {
        Debug.Log("CLose");
        showSkillBook = false;
        contents.SetActive(false);
    }
}