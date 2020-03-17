using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusController : MonoBehaviour
{
    public enum VirusType
    {
        corona,
        motherCorona
    };
    public VirusType virusType;
    public int virusDamage;
    public float virusSpeed;
    public int virusHealth;
    public Animator animator;
    private bool isMove;
   public GameManager gameManager;
    void Start()
    {
        switch(virusType)
        {
            case VirusType.corona:
                virusSpeed = Random.Range(100f, 300f);
                virusDamage = 10;
                virusHealth = 1;
                break;
            case VirusType.motherCorona:

                virusSpeed = Random.Range(50f, 70f);
                virusDamage = 50;
                virusHealth = 1;
                break;
        }
        this.gameObject.tag = "Virus";
        isMove = true;
        animator = this.GetComponent<Animator>();
        GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>($"VirusSprites/{virusType.ToString().ToLower()}");
    }
    void Update()
    {
        if(isMove)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - Time.deltaTime * virusSpeed);
        }
    }
    public void VirusDeath()
    {
        gameManager.VirusKilled();
        virusHealth--;
        if(virusHealth <= 0)
        {
            isMove = false;
            if (virusType == VirusType.motherCorona)
                MotherCoronaKilled();
            StartCoroutine("DestroyObject");
        }
    }
    IEnumerator DestroyObject()
    {
        animator.SetBool("death", true);
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }
    private void MotherCoronaKilled()
    {
        for(int i = 0;i<Random.Range(3,6);i++)
        {
            GameObject spawnVirus = gameManager.spawner.GetComponent<Spawner>().VirusObjects[0];
            spawnVirus.GetComponent<VirusController>().virusType = VirusType.corona;
            Instantiate(spawnVirus, this.transform.position+new Vector3(Random.Range(-50f,50f), Random.Range(-50f, 50f)), Quaternion.identity,gameManager.GameCanvas.transform); 
        }
    }
}
