using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;

    [SerializeField] private Text cherries_text;

    [SerializeField] private AudioSource collection_soundeffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collection_soundeffect.Play();
            Destroy(collision.gameObject);
            cherries++;
            cherries_text.text = "Cherries: " + cherries;
        }
    }
}
