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
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawPoint[Random.Range(1, spawPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
        if (level >= 2)
        {
            GameObject enemy1 = GameManager.instance.pool.Get(0);
            enemy1.transform.position = spawPoint[Random.Range(1, spawPoint.Length)].position;
            enemy1.GetComponent<Enemy>().Init(spawnData[level-2]);
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
