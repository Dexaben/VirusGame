using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notVirusController : MonoBehaviour
{
    public enum notVirusType
    {
        pill
    };
    public notVirusType notvirus_Type;
    public float notVirusSpeed;
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
        switch (notvirus_Type)
        {
            case notVirusType.pill:
                notVirusSpeed = Random.Range(100f, 300f);
                animator.runtimeAnimatorController = animators[0];
                break;
        }
        this.gameObject.tag = "notVirus";
        isMove = true;
    }
    void Update()
    {
        if(isMove)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - Time.deltaTime * notVirusSpeed);
        }
    }
    public void VirusDeath()
    {
        audioSource.pitch = Random.Range(0.6f, 1.2f);
        isMove = false;
        StartCoroutine("PillKilled");
    }
    IEnumerator PillKilled()
    {
        audioSource.PlayOneShot(splat);
        animator.SetBool("death", true);
        yield return new WaitForSeconds(0.35f);
        gameManager.VirusAttacked(25f);
        Destroy(this.gameObject);
    }
}
