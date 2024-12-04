using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawPoint;
    public SpawnData[] spawnData;
    [SerializeField]float time=0;
    int level;

    private void Awake()
    {
        spawPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        time+= Time.deltaTime;
        //Debug.Log(GameManager.instance.gameTime);
        //level =Mathf.Min(Mathf.FloorToInt( GameManager.instance.gameTime / 10f),spawnData.Length-1) ;
        level =Mathf.FloorToInt(GameManager.instance.gameTime / 10f);

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
