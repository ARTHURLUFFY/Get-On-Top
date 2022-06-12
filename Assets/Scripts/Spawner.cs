using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject blockPrefab;
    public float minrespawnTime;
    public float maxrespawnTime;
    private Vector2 screenBounds;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(blockWave());
    }

   private void spawnBlocks()
    {
        GameObject a = Instantiate(blockPrefab) as GameObject;
        a.transform.position = new Vector2(Random.Range(-screenBounds.x + 0.54f, screenBounds.x - 0.54f), player.position.y + 10);
        //a.transform.position = new Vector2(Random.Range(-screenBounds.x + 0.54f, screenBounds.x - 0.54f), screenBounds.y * 1.5f);
    }

    IEnumerator blockWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minrespawnTime,maxrespawnTime));
            spawnBlocks();
        }
    }

}
