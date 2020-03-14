using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Variables")]
    private float gameTime = 0.0f;
    private int killedViruses = 0;
    private int spawnedViruses = 0;

    [Header("Scripts")]
    public InputController inputController;
    public Spawner spawner;

    [Header("GameObjects")]
    public GameObject GameCanvas;
    public GameObject SpawnArea;
    public GameObject DeathArea;


    void Start()
    {
        SpawnArea.GetComponent<BoxCollider2D>().size = SpawnArea.GetComponent<RectTransform>().sizeDelta;
        DeathArea.GetComponent<BoxCollider2D>().size = DeathArea.GetComponent<RectTransform>().sizeDelta;
    }
}
