using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGemUI : MonoBehaviour
{
    [SerializeField] GameObject GemUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GemUI.SetActive(true);
    }

}
