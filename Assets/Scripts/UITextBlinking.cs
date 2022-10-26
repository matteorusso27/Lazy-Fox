using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextBlinking : MonoBehaviour
{
    private Coroutine BlinkTextCoroutine;
    private Text text;
    private float timeToWait;
    private IEnumerator BlinkText()
    {
        while (true)
        {
           
            if(text.color == Color.clear)
            {
                text.color = Color.black;
                timeToWait = 1.2f;
            }
            else
            {
                text.color = Color.clear;
                timeToWait = 0.3f;
            }
            yield return new WaitForSeconds(timeToWait);
        }
    }

    void Start()
    {
        text = GetComponent<Text>();
        BlinkTextCoroutine = StartCoroutine(BlinkText());
    }
}
