using Cinemachine;
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
    [SerializeField] public PoolManager pool;
    [SerializeField] public Player player;
    [SerializeField] public Enemy enemy;
    [SerializeField] public LevelUp uilevelUp;
    [SerializeField] public Result uiResult;
    [SerializeField] public GameObject ClearEnemy;
    [SerializeField] public GameObject targetPrefab;
    [SerializeField] public Spawner Spawner;
    [SerializeField] public Animator transitionAnim;
    [SerializeField] public GameObject gate;
    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { };
    private bool isTest = false;
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
            if(isTest)
            {
                return;
            }
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
        Vector2 v2 = new Vector3(10003, 9982);
        player.transform.position = v2;
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
        isTest=true;
        Vector2 v2 = (Vector2)player.transform.position;
        Vector2 gateOffset = new Vector2(0, 7f);
        Vector2 gatePosition = v2 + gateOffset;
        gate.transform.position = gatePosition;
        gate.SetActive(true);
        //StartCoroutine(GameWinRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        isLive = false;
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        transitionAnim.SetTrigger("start");
        AudioManager.instance.sfx(4);
        AudioManager.instance.PauseMusic();
        Stop();
    }
    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(2.5f);
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
