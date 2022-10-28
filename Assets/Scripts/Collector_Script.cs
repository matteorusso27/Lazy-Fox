using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collector_Script : MonoBehaviour
{
    private int CherryCount = 0;
    [SerializeField]
    private Text CherryTextCount;
    float _hitTime = 0.1f;
    float _hitTimer = 0;
    bool _canPick = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gem")) // Se ho preso la Gemma
        {
            getObject(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Cherry"))
        {
            if (_canPick)
            {
                getObject(collision.gameObject);
                UpdateText();
            }
            
        }

    }

    private void getObject(GameObject gameObject)
    {
        Destroy(gameObject, 1.75f);
        gameObject.GetComponent<Animator>().SetBool("isPicked", true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        _hitTimer = 0;
    }

    private void UpdateText()
    {
       CherryCount++;
       CherryTextCount.text = CherryCount.ToString();
    }

    private void Update()
    {
        // Increment the hit timer
        _hitTimer += Time.deltaTime;


        if (_hitTimer > _hitTime)
            _canPick = true;
        else
        {
            _canPick = false;
        }
    }
}
