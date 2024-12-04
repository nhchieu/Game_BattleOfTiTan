using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager pool;
    public Player player;

    public float gameTime;
    public float maxGameTime=2*10f;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        gameTime += Time.deltaTime;
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
