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
    public GameObject GameEnd_Canvas;

    [SerializeField]
    [Range(0.0f, 100.0f)]
    public float Corona_MotherCorona;

    [Header("Audios")]
    public List<Audios> audios = new List<Audios>();
    public struct Audios
    {
        public string audioName;
        public AudioClip audioClip;
    }
    private void OnEnable() { Init(); }
    void Start() { Init(); }
    void Init() //Oyun başlatıldığında.
    {
        #region configure boxColliders
        SpawnArea.GetComponent<BoxCollider2D>().size = SpawnArea.GetComponent<RectTransform>().sizeDelta;
        DeathArea.GetComponent<BoxCollider2D>().size = DeathArea.GetComponent<RectTransform>().sizeDelta;
        #endregion
        gameTime = 0.0f;
        killedViruses = 0;
        spawnedViruses = 0;
        spawn_timer = 0f;
        spawn_velocity = min_spawn_velocity;
        GameStart();
        Debug.Log("Başladı");
    }
    private void Update()
    {
       
        if(timer_continue)
        {
            gameTime += Time.deltaTime;
            TimerText.text = gameTime.ToString("F1");
        }
        spawn_timer += Time.deltaTime;
        if(spawn_timer > spawn_velocity)
        {
            GameObject spawnVirus = spawner.VirusObjects[Random.Range(0, spawner.VirusObjects.Count)];
            float random = Random.Range(0,100);
            if (random <= Corona_MotherCorona)
                spawnVirus.GetComponent<VirusController>().virusType = VirusController.VirusType.corona;
            else
                spawnVirus.GetComponent<VirusController>().virusType = VirusController.VirusType.motherCorona;
            
            spawnVirus.GetComponent<VirusController>().gameManager = this.GetComponent<GameManager>();
            spawner.SpawnVirus(spawnVirus);
            spawn_timer = 0f;
        }

    }
    public void VirusKilled() //Virüs öldürüldüğünde çağırılacak fonksiyon.
    {
        if (spawn_velocity > 0.5f)
            spawn_velocity -= 0.2f;
        killedViruses++;
    }
    public void VirusAttacked(float health_decrease) //Virus ekranı geçtiğinde çağırılacak fonksiyon.
    {
        health -= health_decrease;
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
        spawner.spawner_enable = true;
        timer_continue = true;
        if (GameEnd_Canvas.activeInHierarchy)
            GameEnd_Canvas.SetActive(false);
    }
}
