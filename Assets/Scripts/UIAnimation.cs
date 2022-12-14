using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] frames;
    private int currentIndex = 0;
    private Image image;
    [SerializeField] private float temp = 0.5f;
   private IEnumerator Idle_Movement()
    {
        while (true)
        {
            if(currentIndex == frames.Length)
            {
                currentIndex = 0;
            }
            image.sprite = frames[currentIndex];
            currentIndex++;
            yield return new WaitForSeconds(temp);
        }
    }
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(Idle_Movement());
    }
}
