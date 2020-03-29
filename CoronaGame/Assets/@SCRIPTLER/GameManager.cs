using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Variables")]
    private float health = 100f;
    private float gameTime = 0.0f;
    private int killedViruses = 0;
    private int spawnedViruses = 0;
    private float spawn_velocity = 3f;
    private float spawn_timer = 0f;
    private float max_spawn_velocity = 0.5f;
    private float min_spawn_velocity = 3f;
    private bool timer_continue = true;

    [Header("Scripts")]
    public InputController inputController;
    public Spawner spawner;

    [Header("GameObjects")]
    public GameObject GameCanvas;
    public GameObject SpawnArea;
    public GameObject DeathArea;

    [Header("UI")]
    public UnityEngine.UI.Text TimerText;
    public UnityEngine.UI.Text ScoreText;
    public GameObject GameEnd_Canvas;
    public UnityEngine.UI.Slider healthSlider;

    [SerializeField]
    [Range(0.0f, 100.0f)]
    public float Corona_Init;
    [SerializeField]
    [Range(0.0f, 100.0f)]
    public float MotherCorona_Init;
    [SerializeField]
    [Range(0.0f, 100.0f)]
    public float BigCorona_Init;

    [Header("Audios")]
    public List<AudioClip> audios = new List<AudioClip>();
    public AudioSource audioSource;
    void Start() { Init(); }
    void Init() //Oyun başlatıldığında.
    {
        #region configure boxColliders
        SpawnArea.GetComponent<BoxCollider2D>().size = SpawnArea.GetComponent<RectTransform>().sizeDelta;
        DeathArea.GetComponent<BoxCollider2D>().size = DeathArea.GetComponent<RectTransform>().sizeDelta;
        #endregion
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audios[0];
        audioSource.Play();
        gameTime = 0.0f;
        killedViruses = 0;
        spawnedViruses = 0;
        spawn_timer = 0f;
        spawn_velocity = min_spawn_velocity;
        GameStart();
    }
    private void Update()
    {
       
        if(timer_continue)
        {
            gameTime += Time.deltaTime;
            TimerText.text = "Timer: "+gameTime.ToString("F0")+"sn";
        }
        spawn_timer += Time.deltaTime;
        if(spawn_timer > spawn_velocity)
        {
            GameObject spawnVirus = spawner.VirusObjects[Random.Range(0, spawner.VirusObjects.Count)];
            float toplam = (Corona_Init / 100) + (MotherCorona_Init / 100) + (BigCorona_Init / 100);
            Corona_Init = Mathf.RoundToInt(Corona_Init / toplam);
            MotherCorona_Init = Mathf.RoundToInt(MotherCorona_Init / toplam);
            BigCorona_Init = Mathf.RoundToInt(BigCorona_Init / toplam);
            int random = Random.Range(0,101);
            if(random  <= Corona_Init)
            spawnVirus.GetComponent<VirusController>().virusType = VirusController.VirusType.corona;
            else if(random > Corona_Init && random <= Corona_Init+MotherCorona_Init)
            spawnVirus.GetComponent<VirusController>().virusType = VirusController.VirusType.motherCorona;
            else
            spawnVirus.GetComponent<VirusController>().virusType = VirusController.VirusType.bigCorona;
            spawnVirus.GetComponent<VirusController>().gameManager = this.GetComponent<GameManager>();
            spawner.SpawnVirus(spawnVirus);
            spawn_timer = 0f;
        }

    }
    public void VirusKilled() //Virüs öldürüldüğünde çağırılacak fonksiyon.
    {
        if (spawn_velocity > 0.9f)
            spawn_velocity -= 0.1f;
        killedViruses++;
        ScoreText.text = "Score: " + killedViruses;
    }
    public void VirusAttacked(float health_decrease) //Virus ekranı geçtiğinde çağırılacak fonksiyon.
    {
        if(spawn_velocity<3f)
        spawn_velocity += 0.8f;
        health -= health_decrease;
        healthSlider.value = health;
        if (health <= 0)
            GameEnd();
    }
    public void GameEnd() //Oyun bittiğinde.
    {
        spawner.spawner_enable = false;
        timer_continue = false;
        GameEnd_Canvas.SetActive(true);
    }
    public void GameStart() //Oyun başlama durumuna geçtiğinde.
    {
        health = 100;
        healthSlider.value = health;
        ScoreText.text = "Score: " + killedViruses;
        spawner.spawner_enable = true;
        timer_continue = true;
        if (GameEnd_Canvas.activeInHierarchy)
            GameEnd_Canvas.SetActive(false);
    }
}
