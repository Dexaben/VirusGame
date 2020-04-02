using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Variables
    public bool spawner_enable = true;
    [Header("Spawn Objects")]
    public List<GameObject> VirusObjects = new List<GameObject>();
    public List<GameObject> notVirusObjects = new List<GameObject>();

    [Header("Instalize")]
    public GameManager gameManager;
    #endregion

    void Start()
    {
        #region Init
        try 
        {
            gameManager = this.gameObject.GetComponent<GameManager>();
        }
        catch
        {
            if (gameManager == null)
                gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
        #endregion
    }
    public void SpawnVirus(GameObject spawnVirusObject)
    {
        if(spawner_enable)
        {
            Vector2 colliderPos = gameManager.SpawnArea.GetComponent<BoxCollider2D>().transform.position;
            float randomPosX = Random.Range(colliderPos.x - gameManager.SpawnArea.GetComponent<BoxCollider2D>().size.x / 2, colliderPos.x + gameManager.SpawnArea.GetComponent<BoxCollider2D>().size.x / 2);
            float randomPosY = Random.Range(colliderPos.y - gameManager.SpawnArea.GetComponent<BoxCollider2D>().size.y / 2, colliderPos.y + gameManager.SpawnArea.GetComponent<BoxCollider2D>().size.y / 2);
            Instantiate(spawnVirusObject, new Vector3(randomPosX, randomPosY), Quaternion.identity, gameManager.GameCanvas.transform);
        }
    }
}
