using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Virus")
            collision.GetComponent<VirusController>().VirusDeath();
    }
}
