using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Listener_Script: MonoBehaviour
{
    [SerializeField] bool isStart;
    private bool alreadyPressed;
    [SerializeField] Text text;

    private void Update()
    {
        if (!alreadyPressed)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return)) 
            {
                alreadyPressed = true;
                if (isStart)
                {
                    Invoke("Start_Game", 1f);
                    text.text = "Let's go";
                }
                else
                {
                    Invoke("Quit", 1f);
                    text.text = "Bye!";
                }
            }
        }
    }
    private void Start_Game()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
