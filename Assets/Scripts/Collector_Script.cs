using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collector_Script : MonoBehaviour
{
    private int CherryCount = 0;
    [SerializeField]
    private Text CherryTextCount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        getObject(collision.gameObject);

        if (collision.gameObject.CompareTag("Cherry"))
        {
            UpdateText();
        }

    }

    private void getObject(GameObject gameObject)
    {
        Destroy(gameObject, 1.75f);
        gameObject.GetComponent<Animator>().SetBool("isPicked", true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void UpdateText()
    {
       
       CherryCount++;
       
       CherryTextCount.text = CherryCount.ToString();
    }
}
