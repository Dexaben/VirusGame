using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusController : MonoBehaviour
{
    public enum VirusType
    {
        corona,
        motherCorona,
        bigCorona
    };
    public VirusType virusType;
    public int virusDamage;
    public float virusSpeed;
    public int virusHealth;
    public Animator animator;
    private bool isMove;
   public GameManager gameManager;
    public List<RuntimeAnimatorController> animators = new List<RuntimeAnimatorController>();
    public AudioSource audioSource;
    public AudioClip splat;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = this.GetComponent<Animator>();
        switch (virusType)
        {
            case VirusType.corona:
                virusSpeed = Random.Range(100f, 300f);
                virusDamage = 10;
                animator.runtimeAnimatorController = animators[0];
                virusHealth = 1;
                break;
            case VirusType.motherCorona:
                animator.runtimeAnimatorController = animators[1];
                virusSpeed = Random.Range(50f, 70f);
                virusDamage = 50;
                virusHealth = 2;
                break;
            case VirusType.bigCorona:
                animator.runtimeAnimatorController = animators[2];
                virusSpeed = Random.Range(10f, 30f);
                virusDamage = 80;
                virusHealth = 3;
                break;
        }
        this.gameObject.tag = "Virus";
        isMove = true;
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
        audioSource.pitch = Random.Range(0.6f, 1.2f);
        audioSource.PlayOneShot(splat);
        gameManager.VirusKilled();
        virusHealth--;
        if(virusHealth <= 0)
        {
            isMove = false;
            if (virusType == VirusType.motherCorona)
                MotherCoronaKilled();
            if (virusType == VirusType.bigCorona)
                BigCoronaKilled();
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
        StartCoroutine("MotherKilled");
    }
    IEnumerator MotherKilled()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < Random.Range(3, 6); i++)
        {
            GameObject spawnVirus = gameManager.spawner.GetComponent<Spawner>().VirusObjects[0];
            spawnVirus.GetComponent<VirusController>().virusType = VirusType.corona;
            Instantiate(spawnVirus, this.transform.position + new Vector3(Random.Range(-70f, 70f), Random.Range(-80f, 80f)), Quaternion.identity, gameManager.GameCanvas.transform);
        }
    }
    private void BigCoronaKilled()
    {
        StartCoroutine("BigKilled");
    }
    IEnumerator BigKilled()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < Random.Range(2, 3); i++)
        {
            GameObject spawnVirus = gameManager.spawner.GetComponent<Spawner>().VirusObjects[0];
            spawnVirus.GetComponent<VirusController>().virusType = VirusType.motherCorona;
            Instantiate(spawnVirus, this.transform.position + new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f)), Quaternion.identity, gameManager.GameCanvas.transform);
        }
    }
   
}
