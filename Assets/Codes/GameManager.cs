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
    public Enemy enemy;
    public LevelUp uilevelUp;
    public Result uiResult;
    public GameObject ClearEnemy;
    public GameObject targetPrefab;
    public Spawner Spawner;
    [SerializeField] Animator transitionAnim;
    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { };
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
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
        targetPrefab.transform.position = mousePos;
        
        if (!isLive)
        {
            return;
        }
        gameTime += Time.deltaTime;
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }

        if (gameTime == maxGameTime && player.scanner.nearestTarget == null)
        {
            GameWin();
        }
        //them 2 dieu kien de test game
        if (Input.GetMouseButtonDown(1)) {
            Time.timeScale = 2;
        }
        if (Input.GetMouseButtonDown(2)) {
            Time.timeScale = 1;
        }
    }
    public void GetExp()
    {
        exp++;
        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {

            level++;
            exp = 0;
            uilevelUp.Show();
         
            AudioManager.instance.sfx(2);
        }
    }
    public void Stop()
    {
        isLive = false;
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
    public void NextScreen()
    {
        StartCoroutine(LoadScene());
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
        StartCoroutine(GameWinRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        isLive = false;
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        AudioManager.instance.sfx(4);
        AudioManager.instance.PauseMusic();
        Stop();
    }
    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(4f);
        Vector3 v3 = new Vector3(10003, 9982, 0);
        player.transform.position = v3;
        transitionAnim.SetTrigger("start");
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
       
    }
}
