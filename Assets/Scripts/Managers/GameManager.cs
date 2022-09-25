using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private static GameManager instance;
    private static int score;
    private static bool isGameOver = false;
    private static int difficultyLevel = 1;

    public static int Score { get => score; set => score = value; }
    public static bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    
    public static GameManager Instance { get => instance; set => instance = value; }
    public static int DifficultyLevel { get => difficultyLevel; set => difficultyLevel = value; }

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
    
    
}
