using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    
    private static GameManager instance;
    private static int score;
    private static bool isGameOver = false;
    private static bool isGameWin = false;
    private static int difficultyLevel = 1;
    private bool firstKeySpawned = false;
    private bool secondKeySpawned = false;
    private bool thirdKeySpawned = false;

    //EVENTOS
    public UnityEvent onFirstKeyUnlocked;
    public UnityEvent onSecondKeyUnlocked;
    public UnityEvent onThirdKeyUnlocked;
    //EVENTOS
    public static int Score { get => score; set => score = value; }
    public static bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    
    public static GameManager Instance { get => instance; set => instance = value; }
    public static int DifficultyLevel { get => difficultyLevel; set => difficultyLevel = value; }
    public static bool IsGameWin { get => isGameWin; set => isGameWin = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        //SUSCRIPCION A EVENTOS
        PlayerBehaviour.OnDead += OnPLayerDeadHandler;
        EnemyBehaviour.OnDead += AddScore;
        BossBehaviour.OnDead += OnBossDeathHandler;
        
    }

    private void Update()
    {
        if (score == 5)
        {
            spawnFirstKey();
            
        }

        if (score == 7)
        {
            spawnSecondKey();

        }


        if (score == 10)
        {
            spawnThirdKey();

        }
    }
    private void OnPLayerDeadHandler()
    {
        isGameOver = true;

    }


    private void AddScore(int points)
    {
        score += points;
    }

    void spawnFirstKey()
    {
        
        if(!firstKeySpawned)
        {
            onFirstKeyUnlocked.Invoke();
            firstKeySpawned = true;
        }
        
    }

    void spawnSecondKey()
    {

        if (!secondKeySpawned)
        {
            onSecondKeyUnlocked.Invoke();
            secondKeySpawned = true;
        }

    }


    void spawnThirdKey()
    {
        if (!thirdKeySpawned)
        {
            onThirdKeyUnlocked.Invoke();
            thirdKeySpawned = true;
        }
    }

    void OnBossDeathHandler()
    {
        IsGameWin = true;
    }
}
