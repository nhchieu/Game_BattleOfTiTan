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
    public float bgmMenuVolume;
    public float bgmBattleVolume;
    [Header("# GameObject")]
    public PoolManager pool;
    public Player player;
    public LevelUp uilevelUp;
    public Result uiResult;
    public Enemy enemy;
    
    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = {};

    private void Awake()
    {
        instance = this;
        AudioManager.instance.BgmOn(0, bgmMenuVolume); 
        
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
        AudioManager.instance.BgmOn(1, bgmBattleVolume);
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
        AudioManager.instance.sfx(4);
        AudioManager.instance.PauseMusic();
        Stop();
    }
    IEnumerator GameWinRoutine()
    {
        yield return new WaitForSeconds(3f);
        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        AudioManager.instance.sfx(5);
        AudioManager.instance.PauseMusic();
        Stop();
    }
    
    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
