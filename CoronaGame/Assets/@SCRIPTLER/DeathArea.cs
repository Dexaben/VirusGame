using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    public GameManager gameManager;
    private void Start() { Init(); }
    private void OnEnable() { Init(); }
    private void Init()
    {
        if (gameManager == null)
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Virus")
        {
            Destroy(other.gameObject);
            gameManager.VirusAttacked(other.GetComponent<VirusController>().virusDamage);
        }
    }
}
