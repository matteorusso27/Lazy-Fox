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
        CherryCount++;
        CherryTextCount.text = CherryCount.ToString();
    }

    private void getObject(GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().SetBool("isPicked", true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
