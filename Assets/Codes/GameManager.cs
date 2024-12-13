using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime;
    [Header("# GameObject")]
    public PoolManager pool;
    public Player player;
    public LevelUp uilevelUp;
    public Result uiResult;
    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = {};

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        uilevelUp.Select(1);
    }
    private void Update()
    {
        if (!isLive)
        {
            return;
        }
        gameTime += Time.deltaTime;
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }

        if(gameTime==maxGameTime && player.scanner.nearestTarget == null)
        {
            GameWin();
        }
    }


    public void GetExp()
    {
        exp++;
        if (exp == nextExp[Mathf.Min(level,nextExp.Length-1)]) {
            
            level++;
            exp = 0;
            uilevelUp.Show();
        }
    }
    public void Stop()
    {
        isLive=false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
    public void GameStart()
    {
        isLive = true;
        player.Health = player.maxHealth;
        uilevelUp.Select(1);
        Time.timeScale = 1;
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }
    public void GameWin()
    {
        StartCoroutine (GameWinRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        isLive=false;
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }
    IEnumerator GameWinRoutine()
    {
        yield return new WaitForSeconds(3f);
        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }
}
