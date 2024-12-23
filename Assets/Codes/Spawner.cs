using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawPoint;
    public SpawnData[] spawnData;
    [SerializeField]float time=0;
    public int level;

    private void Awake()
    {
        spawPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        time += Time.deltaTime;
        level =Mathf.FloorToInt(GameManager.instance.gameTime / 30f);

        if (time > spawnData[level].spawnTime )
        {
            time = 0;
            spawn();
        }

       
    }
    void spawn()
    {
        if (level < 3)
        {
            GameObject enemy = GameManager.instance.pool.Get(0);
            enemy.transform.position = spawPoint[Random.Range(1, spawPoint.Length)].position;
            enemy.GetComponent<Enemy>().Init(spawnData[level]);
        }
        else
        {
            
            GameObject enemy1 = GameManager.instance.pool.Get(0);
            enemy1.transform.position = spawPoint[Random.Range(1, spawPoint.Length)].position;
            Vector3 newScale = enemy1.transform.localScale * 4f; // tang scale
            enemy1.transform.localScale = newScale;
            enemy1.GetComponent<Enemy>().Init(spawnData[level]);
            spawnData[level].spawnTime = 10000;
        }

    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;

}
